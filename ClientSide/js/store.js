/* ══════════════════════════════════════
   store.js — main store page logic
══════════════════════════════════════ */
import { getCurrentUser, isAdmin, isLoggedIn, clearToken } from './auth.js';
import {
  BASE, fetchProducts, fetchProduct, fetchProductImages,
  uploadProductImage, deleteProductImage,
  createProduct, updateProduct, deleteProduct,
  fetchWishlist, addToWishlist, removeFromWishlist,
  fetchCartItems, addToCart, removeFromCart,
  esc, extractImages, extractImageKey
} from './api.js';
import { toast, openLightbox, closeLightbox, buildCarousel, initCarousel, formatPrice, setLoading, buildAdminOverlay } from './ui.js';
import { CATEGORIES, getParentCategories, getChildren, getCategoryById, populateCategorySelects, CATEGORY_ICONS } from './categories.js';
import { t, tCat, getLang } from './i18n.js';
import { applyI18n, initLangToggle } from './applyI18n.js';

/* ══ STATE ══ */
let currentPage   = 1;
// wishlistItems: array of wishlist response objects from API
// { wishListItemId, userId, productName, price, imageUrls, ... }
// We also track productIds locally for quick isInWishlist() lookups
let wishlistItems= [];
let cartItems     = [];
let totalPages    = 1;
let currentFilter = {};
let adminCardsMap = {};   // productId → { imgs, carouselEl }
let uploadTargetId = null;
let uploadFile     = null;

/* ══ INIT ══ */
populateCategorySelects();
initWishlist();
initCart();
initFooterLinks();
initLangToggle((newLang) => {
  // Re-render dynamic content on language switch
  initCategoryNav();
  initCategoryTiles();
  loadProducts();
  const titleEl = document.getElementById('shop-title');
  if (titleEl && !currentFilter.categoryId) titleEl.textContent = t('shop.allProducts');
});
initHeader();
initCategoryNav();
initCategoryTiles();
initFilters();
initUploadModal();
initLightbox();
loadProducts();

/* ══════════════════════════════════════
   HEADER / AUTH
══════════════════════════════════════ */
function initHeader() {
  const user = getCurrentUser();
  console.log('[EkkoShop] getCurrentUser →', user);

  if (user) {
    // Hide guest buttons, show user avatar
    document.getElementById('auth-guest').style.display = 'none';
    document.getElementById('auth-user').style.display  = 'flex';

    // Show wishlist button for logged-in users and wire it
    const wishBtn = document.getElementById('btn-wishlist');
    if (wishBtn) {
      wishBtn.style.display = 'flex';
      wishBtn.addEventListener('click', openWishlist);
    }

    // Load wishlist from server
    loadWishlistFromServer();

    // Load cart from server
    loadCartFromServer();

    const initials = `${user.firstName?.[0] || ''}${user.lastName?.[0] || ''}`.toUpperCase() || '?';
    document.getElementById('user-initials').textContent      = initials;
    document.getElementById('user-fullname').textContent      = `${user.firstName || ''} ${user.lastName || ''}`.trim();
    document.getElementById('user-email-display').textContent = user.email || '';

    const roleBadge = document.getElementById('user-role-badge');
    roleBadge.textContent = user.role || 'Client';
    if (user.role === 'Admin') roleBadge.classList.add('role-admin');

    // Admin panel button
    if (isAdmin()) {
      console.log('[EkkoShop] Admin detected — showing admin UI');
      document.getElementById('btn-admin-panel').style.display = 'flex';
      document.getElementById('btn-admin-panel').addEventListener('click', () => {
        closeUserDropdown();
        openAdminDrawer();
      });
    }

    // Toggle dropdown
    document.getElementById('user-avatar-btn').addEventListener('click', (e) => {
      e.stopPropagation();
      document.getElementById('user-dropdown').classList.toggle('open');
    });
    document.addEventListener('click', closeUserDropdown);

    document.getElementById('btn-logout').addEventListener('click', () => {
      clearToken();
      toast(t('toast.loggedOut'), 'info');
      setTimeout(() => location.reload(), 500);
    });
  } else {
    // Not logged in — make sure guest buttons are visible
    document.getElementById('auth-guest').style.display = 'flex';
    document.getElementById('auth-user').style.display  = 'none';
  }

  // Cart
  document.getElementById('btn-cart').addEventListener('click', openCart);

  // Header search
  document.getElementById('header-search').addEventListener('keydown', (e) => {
    if (e.key === 'Enter') {
      currentFilter.search = e.target.value.trim();
      currentPage = 1;
      loadProducts();
      document.getElementById('shop-section').scrollIntoView({ behavior: 'smooth' });
    }
  });
}

function closeUserDropdown() {
  document.getElementById('user-dropdown')?.classList.remove('open');
}

/* ══════════════════════════════════════
   CART
══════════════════════════════════════ */
function openCart() {
  document.getElementById('cart-overlay').style.display = 'flex';
  document.body.style.overflow = 'hidden';
  renderCartPanel();
  const user = getCurrentUser();
  if (user) loadCartFromServer();
}

document.getElementById('cart-close').addEventListener('click', () => {
  document.getElementById('cart-overlay').style.display = 'none';
  document.body.style.overflow = '';
});
document.getElementById('cart-overlay').addEventListener('click', e => {
  if (e.target.id === 'cart-overlay') {
    document.getElementById('cart-overlay').style.display = 'none';
    document.body.style.overflow = '';
  }
});

async function loadCartFromServer() {
  const user = getCurrentUser();
  if (!user) return;
  const loadingEl = document.getElementById('cart-loading');
  if (loadingEl) loadingEl.style.display = 'flex';
  try {
    const items = await fetchCartItems({ userId: user.id, pageSize: 100 });
    cartItems = Array.isArray(items) ? items : [];
  } catch { cartItems = []; }
  finally {
    if (loadingEl) loadingEl.style.display = 'none';
    renderCartPanel();
    updateCartBadge();
    refreshAllCartBtns();
  }
}

function isInCart(productId) {
  return cartItems.some(c => Number(c.productId) === Number(productId));
}

function updateCartBadge() {
  const badge = document.getElementById('cart-badge');
  if (!badge) return;
  const n = cartItems.length;
  badge.textContent    = n;
  badge.style.display  = n > 0 ? 'flex' : 'none';
}

async function handleAddToCart(productId, btn) {
  const user = getCurrentUser();
  if (!user) {
    toast(t('cart.loginRequired'), 'info');
    setTimeout(() => { window.location.href = 'login.html'; }, 900);
    return;
  }
  productId = Number(productId);
  if (!productId) return;
  if (isInCart(productId)) { openCart(); return; }

  if (btn) { btn.disabled = true; btn.textContent = '…'; }

  // Optimistic
  cartItems.push({ productId, productName: '', price: 0, amount: 1, imageUrls: [] });
  updateCartBadge(); refreshAllCartBtns();

  try {
    const r = await addToCart(user.id, productId, 1);
    if (r.ok) {
      toast(t('cart.added'), 'success');
      await loadCartFromServer();
    } else {
      cartItems = cartItems.filter(c => Number(c.productId) !== productId);
      updateCartBadge(); refreshAllCartBtns();
      toast(t('cart.addFailed'), 'error');
    }
  } catch {
    cartItems = cartItems.filter(c => Number(c.productId) !== productId);
    updateCartBadge(); refreshAllCartBtns();
    toast(t('cart.addFailed'), 'error');
  } finally {
    if (btn) {
      btn.disabled = false;
      btn.textContent = isInCart(productId)
        ? '✓ ' + t('product.viewCart')
        : t('product.addToCart');
    }
  }
}

async function handleRemoveFromCart(productId) {
  const user = getCurrentUser();
  if (!user) return;
  productId = Number(productId);
  const prev = [...cartItems];
  cartItems  = cartItems.filter(c => Number(c.productId) !== productId);
  updateCartBadge(); renderCartPanel(); refreshAllCartBtns();
  try {
    const r = await removeFromCart(user.id, productId);
    if (r.ok) { toast(t('cart.removed'), 'info'); }
    else { cartItems = prev; updateCartBadge(); renderCartPanel(); refreshAllCartBtns(); toast(t('cart.removeFailed'), 'error'); }
  } catch {
    cartItems = prev; updateCartBadge(); renderCartPanel(); refreshAllCartBtns(); toast(t('cart.removeFailed'), 'error');
  }
}

function renderCartPanel() {
  const user     = getCurrentUser();
  const itemsEl  = document.getElementById('cart-items');
  const emptyEl  = document.getElementById('cart-empty');
  const guestEl  = document.getElementById('cart-guest');
  const footerEl = document.getElementById('cart-footer');
  const countEl  = document.getElementById('cart-count');
  const totalEl  = document.getElementById('cart-total');
  if (!itemsEl) return;

  if (!user) {
    if (guestEl)  guestEl.style.display  = 'flex';
    if (emptyEl)  emptyEl.style.display  = 'none';
    if (footerEl) footerEl.style.display = 'none';
    itemsEl.innerHTML = '';
    return;
  }
  if (guestEl) guestEl.style.display = 'none';

  if (!cartItems.length) {
    if (emptyEl)  emptyEl.style.display  = 'flex';
    if (footerEl) footerEl.style.display = 'none';
    itemsEl.innerHTML = '';
    if (countEl) countEl.textContent = '';
    return;
  }

  if (emptyEl)  emptyEl.style.display  = 'none';
  if (footerEl) footerEl.style.display = 'flex';

  const total = cartItems.reduce((s, c) =>
    s + (parseFloat(c.price)||0) * (parseInt(c.amount ?? c.quantity ?? 1)), 0);
  if (countEl) countEl.textContent = `${cartItems.length} ${t('cart.count')}`;
  if (totalEl) totalEl.textContent = formatPrice(total);

  itemsEl.innerHTML = cartItems.map(c => {
    const pid  = Number(c.productId);
    const img  = c.imageUrls?.[0] || '';
    const name = c.productName || c.name || '—';
    const price = parseFloat(c.price) || 0;
    const qty   = parseInt(c.amount ?? c.quantity ?? 1);
    return `<div class="cart-item" data-cid="${pid}">
      <div class="ci-img">
        ${img ? `<img src="${img}" alt="" loading="lazy" onerror="this.style.display='none'"/>` : '📦'}
      </div>
      <div class="ci-info">
        <span class="ci-name">${esc(name)}</span>
        <span class="ci-price">${formatPrice(price)}</span>
        <span class="ci-qty-label">Qty: ${qty}</span>
      </div>
      <button class="ci-remove" data-cid="${pid}" title="Remove">✕</button>
    </div>`;
  }).join('');

  itemsEl.querySelectorAll('.ci-remove').forEach(btn => {
    btn.addEventListener('click', () => handleRemoveFromCart(Number(btn.dataset.cid)));
  });
}

function refreshAllCartBtns() {
  document.querySelectorAll('.btn-cart[data-cart]').forEach(btn => {
    const pid    = Number(btn.dataset.cart);
    const inCart = isInCart(pid);
    btn.classList.toggle('in-cart', inCart);
    btn.textContent = inCart ? '✓ ' + t('product.viewCart') : t('product.addToCart');
  });
}

function initCart() {
  updateCartBadge();

  document.getElementById('btn-clear-cart')?.addEventListener('click', async () => {
    const user = getCurrentUser();
    if (!user || !cartItems.length) return;
    if (!confirm('Clear your entire cart?')) return;
    const prev = [...cartItems];
    const ids  = prev.map(c => Number(c.productId));
    cartItems  = [];
    updateCartBadge(); renderCartPanel(); refreshAllCartBtns();
    try {
      await Promise.all(ids.map(pid => removeFromCart(user.id, pid)));
      toast(t('cart.cleared'), 'info');
    } catch {
      cartItems = prev; updateCartBadge(); renderCartPanel(); refreshAllCartBtns();
      toast(t('cart.removeFailed'), 'error');
    }
  });
}

/* ══════════════════════════════════════
   CATEGORY NAV (pill bar)
══════════════════════════════════════ */
function initCategoryNav() {
  const nav  = document.getElementById('category-nav').querySelector('.cat-nav-inner');
  const sub  = document.getElementById('cat-subnav-inner');
  nav.innerHTML = '';

  // ── All pill ──
  const allBtn = document.createElement('button');
  allBtn.className = 'cat-pill active';
  allBtn.dataset.cat = '';
  allBtn.dataset.level = 'parent';
  allBtn.textContent = t('cat.all');
  nav.appendChild(allBtn);

  // ── Parent pills ──
  getParentCategories().forEach(cat => {
    const btn = document.createElement('button');
    btn.className = 'cat-pill';
    btn.dataset.cat   = cat.id;
    btn.dataset.level = 'parent';
    btn.textContent   = `${CATEGORY_ICONS[cat.id] || ''} ${tCat(cat.name)}`.trim();
    nav.appendChild(btn);
  });

  // ── Sub-nav row ── (hidden until parent selected)
  if (sub) sub.innerHTML = '';
  document.getElementById('cat-subnav').style.display = 'none';

  function showSubnav(parentId) {
    if (!sub) return;
    const children = getChildren(parseInt(parentId));
    if (!children.length) {
      document.getElementById('cat-subnav').style.display = 'none';
      return;
    }
    sub.innerHTML = '';
    // "All [parent]" option
    const allChild = document.createElement('button');
    allChild.className = 'cat-pill cat-child-pill';
    allChild.dataset.cat = '';
    allChild.dataset.level = 'child';
    allChild.textContent = `${t('cat.all')} →`;
    sub.appendChild(allChild);

    children.forEach(c => {
      const btn = document.createElement('button');
      btn.className   = 'cat-pill cat-child-pill';
      btn.dataset.cat = c.id;
      btn.dataset.level = 'child';
      btn.textContent = tCat(c.name);
      sub.appendChild(btn);
    });
    document.getElementById('cat-subnav').style.display = 'block';
  }

  // ── Parent nav click ──
  nav.addEventListener('click', (e) => {
    const pill = e.target.closest('.cat-pill');
    if (!pill) return;
    nav.querySelectorAll('.cat-pill').forEach(p => p.classList.remove('active'));
    pill.classList.add('active');

    const catId = pill.dataset.cat;
    if (!catId) {
      // All — clear filter, hide subnav
      currentFilter.categoryId = '';
      document.getElementById('cat-subnav').style.display = 'none';
      document.getElementById('f-category').value = '';
      currentPage = 1;
      loadProducts();
      document.getElementById('shop-section').scrollIntoView({ behavior: 'smooth' });
      return;
    }
    // Parent selected — show subnav, don't filter yet
    showSubnav(catId);
    document.getElementById('shop-section').scrollIntoView({ behavior: 'smooth' });
  });

  // ── Child nav click ──
  if (sub) {
    sub.addEventListener('click', (e) => {
      const pill = e.target.closest('.cat-pill');
      if (!pill) return;
      sub.querySelectorAll('.cat-pill').forEach(p => p.classList.remove('active'));
      pill.classList.add('active');
      const childId = pill.dataset.cat;
      currentFilter.categoryId = childId;
      document.getElementById('f-category').value = childId;
      currentPage = 1;
      loadProducts();
      document.getElementById('shop-section').scrollIntoView({ behavior: 'smooth' });
    });
  }
}

/* ══════════════════════════════════════
   CATEGORY TILES
══════════════════════════════════════ */
function initCategoryTiles() {
  const grid = document.getElementById('category-tiles');
  const parents = getParentCategories();
  grid.innerHTML = parents.map(cat => {
    const children = getChildren(cat.id);
    const icon = CATEGORY_ICONS[cat.id] || '📦';
    return `<div class="cat-tile" data-cat="${cat.id}">
      <div class="cat-tile-icon">${icon}</div>
      <div class="cat-tile-info">
        <h3>${tCat(cat.name)}</h3>
        <span>${children.length ? `${children.length} ${t('cat.subcategories')}` : t('cat.allItems')}</span>
      </div>
    </div>`;
  }).join('');

  grid.addEventListener('click', (e) => {
    const tile = e.target.closest('.cat-tile');
    if (!tile) return;
    const id = tile.dataset.cat;
    // Activate parent pill and show child subnav (don't filter by parent directly)
    document.querySelectorAll('#category-nav .cat-pill').forEach(p => {
      p.classList.toggle('active', p.dataset.cat == id);
    });
    // Trigger subnav via the parent pill click
    const parentPill = document.querySelector(`#category-nav .cat-pill[data-cat="${id}"]`);
    if (parentPill) parentPill.click();
    document.getElementById('shop-section').scrollIntoView({ behavior: 'smooth' });
  });
}

/* ══════════════════════════════════════
   FILTERS
══════════════════════════════════════ */
function initFilters() {
  document.getElementById('btn-filter').addEventListener('click', () => {
    currentFilter = {
      categoryId: document.getElementById('f-category').value,
      available:  document.getElementById('f-available').value,
      minPrice:   document.getElementById('f-minprice').value,
      maxPrice:   document.getElementById('f-maxprice').value,
      search:     document.getElementById('header-search').value.trim(),
    };
    currentPage = 1;
    loadProducts();
  });

  document.getElementById('btn-clear-filter').addEventListener('click', clearAllFilters);
  document.getElementById('btn-empty-clear').addEventListener('click', clearAllFilters);

  // Enter on price inputs
  ['f-minprice','f-maxprice'].forEach(id => {
    document.getElementById(id).addEventListener('keydown', e => {
      if (e.key === 'Enter') document.getElementById('btn-filter').click();
    });
  });

  // Sync category select change
  document.getElementById('f-category').addEventListener('change', (e) => {
    document.querySelectorAll('.cat-pill').forEach(p => {
      p.classList.toggle('active', p.dataset.cat == e.target.value);
    });
    if (!e.target.value) document.querySelector('.cat-pill[data-cat=""]')?.classList.add('active');
  });
}

function clearAllFilters() {
  currentFilter = {};
  document.getElementById('f-category').value  = '';
  document.getElementById('f-available').value = '';
  document.getElementById('f-minprice').value  = '';
  document.getElementById('f-maxprice').value  = '';
  document.getElementById('header-search').value = '';
  document.querySelectorAll('.cat-pill').forEach((p, i) => p.classList.toggle('active', i === 0));
  currentPage = 1;
  loadProducts();
}

/* ══════════════════════════════════════
   LOAD PRODUCTS
══════════════════════════════════════ */
async function loadProducts() {
  const grid     = document.getElementById('product-grid');
  const loading  = document.getElementById('products-loading');
  const empty    = document.getElementById('products-empty');
  const countEl  = document.getElementById('product-count');
  const titleEl  = document.getElementById('shop-title');

  grid.style.opacity   = '0';
  loading.style.display = 'flex';
  empty.style.display   = 'none';
  adminCardsMap = {};

  try {
    const data = await fetchProducts({
      page:       currentPage,
      pageSize:   20,
      search:     currentFilter.search     || '',
      categoryId: currentFilter.categoryId || '',
      minPrice:   currentFilter.minPrice   || '',
      maxPrice:   currentFilter.maxPrice   || '',
      available:  currentFilter.available  ?? '',
    });

    totalPages = data.totalPages || 1;
    const items = data.items || [];

    // Title
    const catId = currentFilter.categoryId;
    const cat   = catId ? getCategoryById(catId) : null;
    titleEl.textContent = cat ? tCat(cat.name) : t('shop.allProducts');
    countEl.textContent = items.length
      ? `${data.totalCount} ${t('shop.productsPlural')}`
      : '';

    if (!items.length) {
      grid.innerHTML = '';
      empty.style.display = 'flex';
    } else {
      grid.innerHTML = items.map((p, i) => buildProductCard(p, i)).join('');
      // Init carousels
      items.forEach((p, i) => {
        const imgs = extractImages(p);
        adminCardsMap[p.productId] = { imgs: [...imgs], idx: 0, ownerId: null };
        const carEl = grid.querySelector(`[data-key="c${i}"]`);
        if (carEl) initCarousel(carEl, imgs);
      });
      attachCardEvents(items);
    }

    renderPagination();
    grid.style.opacity = '1';
  } catch (err) {
    toast(`${t('toast.loadFailed')}: ${err.message}`, 'error');
    grid.innerHTML = `<div class="load-error">${t('products.error')}</div>`;
    grid.style.opacity = '1';
  } finally {
    loading.style.display = 'none';
  }
}

/* ══════════════════════════════════════
   PRODUCT CARD HTML
══════════════════════════════════════ */
function buildProductCard(p, i) {
  const imgs    = extractImages(p);
  const inStock = (p.amount ?? 0) > 0;
  const catName = p.categoryName || '';
  const admin   = isAdmin();

  const carousel = buildCarousel(imgs, `c${i}`);
  const adminOvl = admin ? buildAdminOverlay(imgs.length > 0) : '';

  return `<article class="product-card" data-id="${p.productId}" data-card-idx="${i}">
    <div class="card-media">
      ${carousel}
      ${adminOvl}
      ${inStock
        ? `<span class="stock-chip stock-in">${t('product.inStock')}</span>`
        : `<span class="stock-chip stock-out">${t('product.outOfStock')}</span>`}
    </div>
    <div class="card-body">
      ${catName ? `<span class="card-category">${esc(catName)}</span>` : ''}
      <h3 class="card-name">${esc(p.name || '—')}</h3>
      <div class="card-footer">
        <span class="card-price">${formatPrice(p.price)}</span>
        <div class="card-actions">
          <button class="btn-wish${isInWishlist(p.productId) ? ' wished' : ''}" data-wish="${p.productId}" title="Wishlist">♡</button>
          <button class="btn btn-ghost btn-sm card-view-btn" data-id="${p.productId}">${t('product.view')}</button>
        </div>
      </div>
      <button class="btn btn-sm btn-full btn-cart ${isInCart(p.productId) ? 'btn-ghost in-cart' : 'btn-primary'}"
              data-cart="${p.productId}" style="margin-top:8px">
        ${isInCart(p.productId) ? '✓ ' + t('product.viewCart') : t('product.addToCart')}
      </button>
    </div>
  </article>`;
}

/* ══════════════════════════════════════
   CARD EVENT DELEGATION
══════════════════════════════════════ */
// Track whether grid listener is already attached
let _gridListenerAttached = false;

function attachCardEvents(items) {
  const grid = document.getElementById('product-grid');
  if (_gridListenerAttached) return;
  _gridListenerAttached = true;

  grid.addEventListener('click', async (e) => {
    const card = e.target.closest('.product-card');
    if (!card) return;
    const pid     = parseInt(card.dataset.id);
    const cardIdx = parseInt(card.dataset.cardIdx);

    // 1. Admin: add image button — highest priority
    if (e.target.closest('.aov-add')) {
      e.stopPropagation();
      openUploadModal(pid);
      return;
    }

    // 2. Admin: delete image button
    if (e.target.closest('.aov-del')) {
      e.stopPropagation();
      const state = adminCardsMap[pid];
      if (!state || !state.imgs.length) return;
      const imageUrl = state.imgs[state.idx || 0];
      const key = extractImageKey(imageUrl);
      if (!confirm(`${t('admin.confirmDelImg')}\n${key}`)) return;
      try {
        const r = await deleteProductImage(pid, imageUrl);
        if (r.ok) {
          toast(t('admin.imageDeleted'));
          state.imgs.splice(state.idx || 0, 1);
          state.idx = Math.min(state.idx || 0, state.imgs.length - 1);
          rebuildCardCarousel(card, cardIdx, pid, state.imgs);
        } else {
          const t = await r.text();
          let msg = t; try { msg = JSON.parse(t).message || t; } catch {}
          toast(`${t('admin.deleteFailed')}: ${msg}`, 'error');
        }
      } catch (err) { toast(err.message, 'error'); }
      return;
    }

    // 3. Wishlist toggle
    if (e.target.closest('.btn-wish')) {
      e.stopPropagation();
      const btn = e.target.closest('.btn-wish');
      const wid = parseInt(btn.dataset.wish);
      toggleWishlist(wid, card);
      return;
    }

    // 4. Add to cart button
    if (e.target.closest('.btn-cart')) {
      e.stopPropagation();
      const btn = e.target.closest('.btn-cart');
      const cid = parseInt(btn.dataset.cart);
      if (isInCart(cid)) {
        openCart();
      } else {
        handleAddToCart(cid, btn);
      }
      return;
    }

    // 5. Carousel prev/next/dots
    if (e.target.closest('.carr') || e.target.closest('.cdot')) return;

    // 6. Anything else → open product modal
    openProductModal(pid);
  });
}

function rebuildCardCarousel(cardEl, cardIdx, pid, newImgs) {
  const mediaEl = cardEl.querySelector('.card-media');

  // Replace carousel
  const oldCar = mediaEl.querySelector('.product-carousel');
  const newHtml = buildCarousel(newImgs, `c${cardIdx}`);
  const tmp = document.createElement('div');
  tmp.innerHTML = newHtml;
  const newCarEl = tmp.firstElementChild;
  if (oldCar) oldCar.replaceWith(newCarEl);
  else mediaEl.prepend(newCarEl);
  initCarousel(newCarEl, newImgs);

  // Replace admin overlay
  const oldOvl = mediaEl.querySelector('.admin-img-overlay');
  const newOvlTmp = document.createElement('div');
  newOvlTmp.innerHTML = buildAdminOverlay(newImgs.length > 0);
  if (oldOvl) oldOvl.replaceWith(newOvlTmp.firstElementChild);
  else mediaEl.appendChild(newOvlTmp.firstElementChild);

  adminCardsMap[pid] = { imgs: [...newImgs], idx: 0 };
}

/* ══════════════════════════════════════
   PRODUCT DETAIL MODAL
══════════════════════════════════════ */
async function openProductModal(productId) {
  const modal  = document.getElementById('product-modal');
  const body   = document.getElementById('product-modal-box');
  const content = document.getElementById('product-modal-body');

  content.innerHTML = `<div class="modal-loading"><div class="loader-ring"></div></div>`;
  modal.classList.add('open');
  document.body.style.overflow = 'hidden';

  try {
    const [product, freshImgs] = await Promise.all([
      fetchProduct(productId),
      fetchProductImages(productId),
    ]);
    if (!product) throw new Error('Product not found');

    const imgs    = freshImgs.length ? freshImgs : extractImages(product);
    const inStock = (product.amount ?? 0) > 0;
    const admin   = isAdmin();
    const cat     = getCategoryById(product.categoryId);
    // Store the product's real ownerId now that we have the full product object
    const productOwnerId = product.userId ?? product.UserId ?? product.user_id ?? null;
    if (adminCardsMap[productId]) {
      adminCardsMap[productId].ownerId = productOwnerId;
    } else {
      adminCardsMap[productId] = { imgs: [...imgs], idx: 0, ownerId: productOwnerId };
    }
    console.log(`[EkkoShop] modal product #${productId} ownerId=${productOwnerId}`, product);
    const catDisplayName = cat ? tCat(cat.name) : '';

    const carousel = buildCarousel(imgs, `modal-${productId}`);
    const adminOvl = admin ? buildAdminOverlay(imgs.length > 0) : '';

    content.innerHTML = `
      <div class="modal-product-layout">
        <div class="modal-product-media">
          <div class="modal-carousel-wrap">
            ${carousel}
            ${adminOvl}
          </div>
          ${admin ? `
          <div class="admin-actions-bar">
            <span class="admin-label">⚙ Admin</span>
            <button class="btn btn-ghost btn-xs" id="modal-upload-btn">${t('admin.addImageBtn')}</button>
            <button class="btn btn-danger btn-xs" id="modal-del-product-btn">${t('admin.deleteProduct')}</button>
          </div>` : ''}
        </div>
        <div class="modal-product-info">
          ${cat ? `<span class="card-category">${esc(cat.name)}</span>` : ''}
          <h2 class="modal-product-name">${esc(product.name || '—')}</h2>
          <div class="modal-price-row">
            <span class="modal-price">${formatPrice(product.price)}</span>
            <span class="stock-chip ${inStock ? 'stock-in' : 'stock-out'}">${inStock ? `✓ ${product.amount} ${t('filter.inStock')}` : t('product.outOfStock')}</span>
          </div>
          ${product.description ? `<p class="modal-desc">${esc(product.description)}</p>` : ''}
          <div class="modal-meta">
            <div class="meta-row"><span>${t('product.id')}</span><span>#${product.productId}</span></div>
            ${cat ? `<div class="meta-row"><span>${t('product.category')}</span><span>${catDisplayName}</span></div>` : ''}
            ${product.creationDate ? `<div class="meta-row"><span>${t('product.added')}</span><span>${new Date(product.creationDate).toLocaleDateString()}</span></div>` : ''}
          </div>
          <div class="modal-cta">
            <button class="btn btn-lg btn-cart ${isInCart(product.productId) ? 'btn-ghost in-cart' : 'btn-primary'}"
                    data-cart="${product.productId}" id="modal-cart-btn">
              ${isInCart(product.productId) ? '✓ ' + t('product.viewCart') : t('product.addToCart')}
            </button>
            <button class="btn btn-ghost btn-lg">${t('product.wishlist')}</button>
          </div>
          ${admin ? `
          <div class="admin-edit-form" id="admin-edit-form">
            <div class="admin-edit-header">
              <span>${t('admin.editProduct')}</span>
              <button class="btn btn-ghost btn-xs" id="toggle-edit-btn">${t('admin.show')}</button>
            </div>
            <div class="admin-edit-fields" id="admin-edit-fields" style="display:none">
              <div class="edit-row">
                <input type="text" id="edit-name" value="${esc(product.name||'')}" placeholder="Name"/>
                <input type="number" id="edit-price" value="${product.price||''}" placeholder="Price" step="0.01"/>
              </div>
              <div class="edit-row">
                <input type="number" id="edit-amount" value="${product.amount||''}" placeholder="Amount"/>
                <input type="text" id="edit-desc" value="${esc(product.description||'')}" placeholder="${t('drawer.description')}"/>
              </div>
              <button class="btn btn-warning btn-sm" id="btn-save-edit">${t('admin.saveChanges')}</button>
              <div id="edit-result" style="margin-top:8px;font-size:.75rem"></div>
            </div>
          </div>` : ''}
        </div>
      </div>`;

    // Init carousel
    const carEl = content.querySelector(`[data-key="modal-${productId}"]`);
    if (carEl) initCarousel(carEl, imgs);

    // Admin modal events
    if (admin) {
      let modalImgs = [...imgs];

      content.getElementById?.('modal-upload-btn') || content.querySelector('#modal-upload-btn')
        ?.addEventListener('click', () => openUploadModal(productId, () => {
          // Refresh images after upload
          fetchProductImages(productId).then(newImgs => {
            modalImgs = newImgs;
            const wrap = content.querySelector('.modal-carousel-wrap');
            if (wrap) {
              const newHtml = buildCarousel(newImgs, `modal-${productId}`);
              const tmp = document.createElement('div'); tmp.innerHTML = newHtml;
              const oldCar = wrap.querySelector('.product-carousel,.card-img-placeholder');
              if (oldCar) oldCar.replaceWith(tmp.firstElementChild);
              initCarousel(wrap.querySelector('.product-carousel'), newImgs);
            }
          });
        }));

      content.querySelector('.aov-add')?.addEventListener('click', () =>
        openUploadModal(productId));

      content.querySelector('.aov-del')?.addEventListener('click', async () => {
        if (!modalImgs.length) return;
        const url = modalImgs[0]; // simplified: deletes first
        const key = extractImageKey(url);
        if (!confirm(`Delete image?\n${key}`)) return;
        const r = await deleteProductImage(productId, url);
        if (r.ok) { toast('Image deleted'); modalImgs.shift(); }
        else toast('Delete failed', 'error');
      });

      content.querySelector('#modal-del-product-btn')?.addEventListener('click', async () => {
        if (!confirm(`${t('admin.confirmDelProd')} #${productId}?`)) return;
        const user = getCurrentUser();
        const ownerId = adminCardsMap[productId]?.ownerId || user?.id;
        const r = await deleteProduct(productId, ownerId);
        if (r.ok) {
          toast(t('toast.productDeleted'));
          modal.classList.remove('open');
          document.body.style.overflow = '';
          loadProducts();
        } else { toast('Delete failed', 'error'); }
      });

      // Toggle edit form
      content.querySelector('#toggle-edit-btn')?.addEventListener('click', () => {
        const fields = content.querySelector('#admin-edit-fields');
        const btn    = content.querySelector('#toggle-edit-btn');
        const show   = fields.style.display === 'none';
        fields.style.display = show ? 'grid' : 'none';
        btn.textContent = show ? t('admin.hide') : t('admin.show');
      });

      content.querySelector('#btn-save-edit')?.addEventListener('click', async () => {
        const user = getCurrentUser();
        const dto  = {
          productId: productId,
          userId:    parseInt(adminCardsMap[productId]?.ownerId || user?.id),
          name:      content.querySelector('#edit-name').value.trim() || undefined,
          price:     parseFloat(content.querySelector('#edit-price').value) || undefined,
          amount:    parseInt(content.querySelector('#edit-amount').value) || undefined,
          description: content.querySelector('#edit-desc').value.trim() || undefined,
          modifiedDate: new Date().toISOString(),
        };
        const r = await updateProduct(dto);
        const res = content.querySelector('#edit-result');
        if (r.ok || r.status === 204) {
          res.textContent = t('admin.saved');
          res.style.color = 'var(--success)';
          toast(t('admin.updated'));
          loadProducts();
        } else {
          const t = await r.text();
          res.textContent = `Failed: ${t}`;
          res.style.color = 'var(--danger)';
        }
      });
    }

    // Wire modal cart button
    content.querySelector('#modal-cart-btn')?.addEventListener('click', async (e) => {
      const btn = e.currentTarget;
      const pid = parseInt(btn.dataset.cart);
      if (isInCart(pid)) {
        document.getElementById('product-modal').classList.remove('open');
        document.body.style.overflow = '';
        openCart();
      } else {
        await handleAddToCart(pid, btn);
        if (isInCart(pid)) {
          btn.className = 'btn btn-ghost btn-lg in-cart btn-cart';
          btn.textContent = '✓ ' + t('product.viewCart');
        }
      }
    });

  } catch (err) {
    content.innerHTML = `<div class="modal-error">Failed to load product: ${err.message}</div>`;
  }
}

document.getElementById('product-modal-close').addEventListener('click', () => {
  document.getElementById('product-modal').classList.remove('open');
  document.body.style.overflow = '';
});
document.getElementById('product-modal').addEventListener('click', (e) => {
  if (e.target === document.getElementById('product-modal')) {
    document.getElementById('product-modal').classList.remove('open');
    document.body.style.overflow = '';
  }
});

/* ══════════════════════════════════════
   ADMIN DRAWER
══════════════════════════════════════ */
function openAdminDrawer() {
  document.getElementById('admin-drawer').classList.add('open');
  document.body.style.overflow = 'hidden';
  renderAdminDrawer();
}
function closeAdminDrawer() {
  document.getElementById('admin-drawer').classList.remove('open');
  document.body.style.overflow = '';
}
document.getElementById('drawer-close').addEventListener('click', closeAdminDrawer);
document.getElementById('drawer-overlay').addEventListener('click', closeAdminDrawer);

function renderAdminDrawer() {
  const body = document.getElementById('drawer-body');
  body.innerHTML = `
    <div class="drawer-section">
      <h4>${t('drawer.createProduct')}</h4>
      <div class="drawer-form">
        <input type="text"   id="ap-name"  placeholder="${t('drawer.name')}" class="drawer-input"/>
        <input type="number" id="ap-price" placeholder="${t('drawer.price')}" step="0.01" class="drawer-input"/>
        <input type="number" id="ap-amt"   placeholder="${t('drawer.amount')}" class="drawer-input"/>
        <textarea id="ap-desc" placeholder="${t('drawer.description')}" class="drawer-input" rows="3"></textarea>
        <select id="ap-cat" data-category-select="— Select category —" class="drawer-input"></select>
        <div class="drawer-img-zone" id="ap-img-zone">
          <input type="file" id="ap-img-input" accept="image/*" style="position:absolute;inset:0;opacity:0;cursor:pointer"/>
          <span>${t('drawer.attachImage')}</span>
          <span id="ap-img-name" class="dz-hint"></span>
        </div>
        <div id="ap-result"></div>
        <button class="btn btn-primary" id="ap-submit">${t('drawer.create')}</button>
      </div>
    </div>
    <div class="drawer-divider"></div>
    <div class="drawer-section">
      <h4>${t('drawer.quickDelete')}</h4>
      <div class="drawer-form">
        <input type="number" id="ap-del-id" placeholder="${t('drawer.productId')}" class="drawer-input"/>
        <div id="ap-del-result"></div>
        <button class="btn btn-danger" id="ap-del-submit">${t('drawer.deleteBtn')}</button>
      </div>
    </div>`;

  populateCategorySelects();

  document.getElementById('ap-img-input').addEventListener('change', function () {
    document.getElementById('ap-img-name').textContent = this.files[0]?.name || '';
  });

  document.getElementById('ap-submit').addEventListener('click', async () => {
    const name  = document.getElementById('ap-name').value.trim();
    const price = document.getElementById('ap-price').value;
    const amt   = document.getElementById('ap-amt').value;
    const desc  = document.getElementById('ap-desc').value.trim();
    const cat   = document.getElementById('ap-cat').value;
    const file  = document.getElementById('ap-img-input').files[0];
    const res   = document.getElementById('ap-result');
    const user  = getCurrentUser();

    if (!name) { res.textContent = t('drawer.nameRequired'); res.style.color = 'var(--danger)'; return; }

    const btn = document.getElementById('ap-submit');
    btn.disabled = true; btn.textContent = t('drawer.creating');

    try {
      const r = await createProduct({
        name, userId: parseInt(user?.id),
        price: price ? parseFloat(price) : undefined,
        amount: amt ? parseInt(amt) : undefined,
        description: desc || undefined,
        categoryId: cat ? parseInt(cat) : undefined,
      });
      if (!r.ok) throw new Error(`HTTP ${r.status}`);
      const newId = await r.json();

      if (file) {
        await uploadProductImage(newId, file, user?.id);
      }

      res.textContent = `${t('drawer.created')} ${newId}`;
      res.style.color = 'var(--success)';
      toast(`${t('drawer.created')} #${newId}`, 'success');
      loadProducts();
      // Reset
      ['ap-name','ap-price','ap-amt','ap-desc','ap-cat'].forEach(id => {
        const el = document.getElementById(id); if (el) el.value = '';
      });
      document.getElementById('ap-img-name').textContent = '';
    } catch (err) {
      res.textContent = `✕ ${err.message}`;
      res.style.color = 'var(--danger)';
    } finally {
      btn.disabled = false; btn.textContent = t('drawer.create');
    }
  });

  document.getElementById('ap-del-submit').addEventListener('click', async () => {
    const id   = document.getElementById('ap-del-id').value;
    const res  = document.getElementById('ap-del-result');
    const user = getCurrentUser();
    if (!id) { res.textContent = t('drawer.enterIdFirst'); return; }
    if (!confirm(`Delete product #${id}?`)) return;
    const r = await deleteProduct(id, adminCardsMap[parseInt(id)]?.ownerId || user?.id);
    if (r.ok || r.status === 204) {
      res.textContent = `✓ Product #${id} deleted`;
      res.style.color = 'var(--success)';
      toast(`Product #${id} deleted`);
      loadProducts();
    } else {
      const t = await r.text();
      res.textContent = `✕ Failed: ${t}`;
      res.style.color = 'var(--danger)';
    }
  });
}

/* ══════════════════════════════════════
   UPLOAD MODAL
══════════════════════════════════════ */
function initUploadModal() {
  document.getElementById('upload-modal-close').addEventListener('click', closeUploadModal);
  document.getElementById('btn-upload-cancel').addEventListener('click', closeUploadModal);
  document.getElementById('upload-modal').addEventListener('click', (e) => {
    if (e.target === document.getElementById('upload-modal')) closeUploadModal();
  });

  document.getElementById('upload-file-input').addEventListener('change', () => {
    const f = document.getElementById('upload-file-input').files[0];
    if (f) applyUploadFile(f);
  });

  const dz = document.getElementById('drop-zone');
  dz.addEventListener('dragover',  e => { e.preventDefault(); dz.classList.add('dz-over'); });
  dz.addEventListener('dragleave', () => dz.classList.remove('dz-over'));
  dz.addEventListener('drop', e => {
    e.preventDefault(); dz.classList.remove('dz-over');
    const f = e.dataTransfer.files[0];
    if (f?.type.startsWith('image/')) applyUploadFile(f);
    else toast(t('upload.dropOnly'), 'error');
  });

  document.getElementById('btn-do-upload').addEventListener('click', doUpload);
}

function openUploadModal(productId, onSuccessCallback) {
  uploadTargetId = productId;
  uploadFile     = null;
  document.getElementById('upload-file-input').value = '';
  document.getElementById('upload-preview').src = '';
  document.getElementById('upload-preview').style.display = 'none';
  document.getElementById('upload-filename').textContent = '';
  document.getElementById('upload-filename').style.display = 'none';
  document.getElementById('upload-bar-wrap').style.display = 'none';
  document.getElementById('upload-bar').style.width = '0%';
  document.getElementById('btn-do-upload').disabled = true;
  document.getElementById('upload-result').innerHTML = '';
  document.getElementById('upload-pid').textContent = productId;
  document.getElementById('upload-modal')._onSuccess = onSuccessCallback;
  document.getElementById('upload-modal').classList.add('open');
  document.body.style.overflow = 'hidden';
}

function closeUploadModal() {
  document.getElementById('upload-modal').classList.remove('open');
  document.body.style.overflow = '';
}

function applyUploadFile(f) {
  uploadFile = f;
  document.getElementById('upload-filename').textContent = `${f.name} (${(f.size/1024).toFixed(1)} KB)`;
  document.getElementById('upload-filename').style.display = 'block';
  const reader = new FileReader();
  reader.onload = e => {
    document.getElementById('upload-preview').src = e.target.result;
    document.getElementById('upload-preview').style.display = 'block';
  };
  reader.readAsDataURL(f);
  document.getElementById('btn-do-upload').disabled = false;
}

async function doUpload() {
  if (!uploadFile || !uploadTargetId) return;
  const btn   = document.getElementById('btn-do-upload');
  const bar   = document.getElementById('upload-bar');
  const barWr = document.getElementById('upload-bar-wrap');
  const resEl = document.getElementById('upload-result');

  btn.disabled = true; btn.textContent = t('upload.uploading');
  barWr.style.display = 'block';
  bar.style.width = '50%';

  try {
    const user = getCurrentUser();
    // Use the product's original owner ID — backend checks product.UserId matches
    const ownerIdForUpload = user?.id;
    console.log(`[EkkoShop] upload → productId=${uploadTargetId} userId=${ownerIdForUpload}`);
    const r = await uploadProductImage(uploadTargetId, uploadFile, ownerIdForUpload);
    bar.style.width = '100%';
    const text = await r.text();
    if (r.ok) {
      resEl.innerHTML = `<span style="color:var(--success)">${t('upload.success')}</span>`;
      toast(t('upload.toasted'));
      // Notify
      const cb = document.getElementById('upload-modal')._onSuccess;
      if (cb) cb();
      // Refresh card on grid
      const card = document.querySelector(`.product-card[data-id="${uploadTargetId}"]`);
      if (card) {
        const newImgs = await fetchProductImages(uploadTargetId);
        const idx = parseInt(card.dataset.cardIdx);
        rebuildCardCarousel(card, idx, uploadTargetId, newImgs);
      }
      setTimeout(closeUploadModal, 1200);
    } else {
      let msg = text; try { msg = JSON.parse(text).message || text; } catch {}
      resEl.innerHTML = `<span style="color:var(--danger)">✕ ${msg}</span>`;
      toast(`${t('upload.failed')}: ${msg}`, 'error');
    }
  } catch (err) {
    resEl.innerHTML = `<span style="color:var(--danger)">✕ ${err.message}</span>`;
    toast(err.message, 'error');
  } finally {
    btn.disabled = false; btn.textContent = t('upload.btn');
  }
}

/* ══════════════════════════════════════
   WISHLIST (CLEAN VERSION)
══════════════════════════════════════ */

/** Load wishlist from server */
async function loadWishlistFromServer() {
  const user = getCurrentUser();
  if (!user) return;

  try {
    const items = await fetchWishlist({ userId: user.id, pageSize: 100 });

    // Normalize data
    wishlistItems = items.map(item => ({
      ...item,
      productId: Number(item.productId)
    }));

    updateWishlistBadge();
    refreshAllHearts();
  } catch (err) {
    console.warn('[Wishlist] Failed to load:', err);
  }
}

/** Check if product is in wishlist */
function isInWishlist(productId) {
  return wishlistItems.some(w => w.productId === productId);
}

/** Toggle wishlist (add/remove) */
async function toggleWishlist(productId, cardEl) {
  const user = getCurrentUser();
  if (!user) {
    toast('Please log in to use your wishlist', 'info');
    setTimeout(() => window.location.href = 'login.html', 900);
    return;
  }

  if (!productId || isNaN(productId)) {
    console.warn('[Wishlist] Invalid productId:', productId);
    return;
  }

  const alreadyWished = isInWishlist(productId);

  const updateHearts = (wished) => {
    document.querySelectorAll(`.btn-wish[data-wish="${productId}"]`).forEach(btn => {
      btn.classList.toggle('wished', wished);
      btn.textContent = wished ? '♥' : '♡';
    });
  };

  if (alreadyWished) {
    // REMOVE
    wishlistItems = wishlistItems.filter(w => w.productId !== productId);
    updateHearts(false);
    updateWishlistBadge();
    renderWishlistPanel();

    try {
      const r = await removeFromWishlist(user.id, productId);
      if (!r.ok) throw new Error();
      toast(t('wishlist.removed'), 'info');
    } catch {
      toast('Failed to remove', 'error');
      await loadWishlistFromServer();
    }

  } else {
    // ADD (optimistic)
    const name  = cardEl?.querySelector('.card-name')?.textContent || '';
    const price = cardEl?.querySelector('.card-price')?.textContent || '';
    const img   = cardEl?.querySelector('.car-slide img')?.src || '';

    wishlistItems.push({
      productId,
      productName: name,
      price: parseFloat(price.replace('₾', '')) || 0,
      imageUrls: img ? [img] : []
    });

    updateHearts(true);
    updateWishlistBadge();
    renderWishlistPanel();

    try {
      const r = await addToWishlist(user.id, productId);
      if (!r.ok) throw new Error();

      toast(t('wishlist.added'), 'success');
      await loadWishlistFromServer();
    } catch {
      toast('Failed to add', 'error');
      await loadWishlistFromServer();
    }
  }
}

/** Refresh heart icons */
function refreshAllHearts() {
  document.querySelectorAll('.btn-wish').forEach(btn => {
    const pid = Number(btn.dataset.wish);
    const wished = isInWishlist(pid);

    btn.classList.toggle('wished', wished);
    btn.textContent = wished ? '♥' : '♡';
  });
}

/** Update badge */
function updateWishlistBadge() {
  const badge = document.getElementById('wishlist-badge');
  if (!badge) return;

  const count = wishlistItems.length;
  badge.textContent = count;
  badge.style.display = count > 0 ? 'flex' : 'none';
}

/** Open wishlist */
function openWishlist() {
  renderWishlistPanel();
  document.getElementById('wishlist-overlay').style.display = 'flex';
  document.body.style.overflow = 'hidden';
}

/** Render wishlist UI */
function renderWishlistPanel() {
  const itemsEl = document.getElementById('wishlist-items');
  const emptyEl = document.getElementById('wishlist-empty');
  const countEl = document.getElementById('wishlist-count');

  if (!itemsEl) return;

  if (countEl) {
    countEl.textContent = wishlistItems.length
      ? `${wishlistItems.length} ${t('wishlist.count')}`
      : '';
  }

  if (!wishlistItems.length) {
    emptyEl.style.display = 'flex';
    itemsEl.innerHTML = '';
    return;
  }

  emptyEl.style.display = 'none';

  itemsEl.innerHTML = wishlistItems.map(w => {
    const pid = Number(w.productId);
    if (!pid) return '';

    const img  = w.imageUrls?.[0] || '';
    const name = w.productName || '—';
    const price = w.price != null ? formatPrice(w.price) : '—';

    return `
      <div class="wishlist-item" data-wid="${pid}">
        <div class="wi-img">
          ${img ? `<img src="${img}" alt="">` : '<span>📷</span>'}
        </div>
        <div class="wi-info">
          <span class="wi-name">${esc(name)}</span>
          <span class="wi-price">${price}</span>
        </div>
        <div class="wi-actions">
          <button class="btn btn-primary btn-xs wi-cart" data-wid="${pid}">
            ${t('wishlist.addToCart')}
          </button>
          <button class="btn btn-ghost btn-xs wi-remove" data-wid="${pid}">
            ${t('wishlist.remove')}
          </button>
        </div>
      </div>
    `;
  }).join('');

  // REMOVE buttons
  itemsEl.querySelectorAll('.wi-remove').forEach(btn => {
    btn.addEventListener('click', async () => {
      const pid = Number(btn.dataset.wid);
      await toggleWishlist(pid, null);
    });
  });

  // CART buttons
  itemsEl.querySelectorAll('.wi-cart').forEach(btn => {
    btn.addEventListener('click', async () => {
      const pid = Number(btn.dataset.wid);
      document.getElementById('wishlist-overlay').style.display = 'none';
      document.body.style.overflow = '';
      await handleAddToCart(pid, null);
      openCart();
    });
  });
}

/** Init wishlist */
function initWishlist() {
  updateWishlistBadge();

  document.getElementById('wishlist-close')?.addEventListener('click', () => {
    document.getElementById('wishlist-overlay').style.display = 'none';
    document.body.style.overflow = '';
  });

  document.getElementById('wishlist-overlay')?.addEventListener('click', (e) => {
    if (e.target.id === 'wishlist-overlay') {
      document.getElementById('wishlist-overlay').style.display = 'none';
      document.body.style.overflow = '';
    }
  });
}

/* ══════════════════════════════════════
   FOOTER CATEGORY LINKS
══════════════════════════════════════ */
function initFooterLinks() {
  document.querySelectorAll('.footer-cat-link').forEach(a => {
    a.addEventListener('click', (e) => {
      e.preventDefault();
      const catId = a.dataset.cat;
      currentFilter.categoryId = catId;
      currentPage = 1;
      document.getElementById('f-category').value = catId;
      document.querySelectorAll('.cat-pill').forEach(p => {
        p.classList.toggle('active', p.dataset.cat == catId);
      });
      loadProducts();
      document.getElementById('shop-section').scrollIntoView({ behavior: 'smooth' });
    });
  });
}

/* ══════════════════════════════════════
   PAGINATION
══════════════════════════════════════ */
function renderPagination() {
  const el = document.getElementById('pagination');
  if (totalPages <= 1) { el.innerHTML = ''; return; }

  let html = '';
  if (currentPage > 1) html += `<button class="page-btn" data-p="${currentPage - 1}">${t('page.prev')}</button>`;
  for (let i = 1; i <= totalPages; i++) {
    if (totalPages > 7 && Math.abs(i - currentPage) > 2 && i !== 1 && i !== totalPages) {
      if (i === 2 || i === totalPages - 1) html += `<span class="page-ellipsis">…</span>`;
      continue;
    }
    html += `<button class="page-btn${i === currentPage ? ' active' : ''}" data-p="${i}">${i}</button>`;
  }
  if (currentPage < totalPages) html += `<button class="page-btn" data-p="${currentPage + 1}">${t('page.next')}</button>`;
  el.innerHTML = html;
  el.addEventListener('click', (e) => {
    const btn = e.target.closest('.page-btn');
    if (!btn) return;
    currentPage = parseInt(btn.dataset.p);
    loadProducts();
    document.getElementById('shop-section').scrollIntoView({ behavior: 'smooth' });
  });
}

/* ══════════════════════════════════════
   LIGHTBOX
══════════════════════════════════════ */
function initLightbox() {
  document.getElementById('lightbox')?.addEventListener('click', (e) => {
    if (e.target === document.getElementById('lightbox')) closeLightbox();
  });
}
