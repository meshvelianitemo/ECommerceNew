/* ══════════════════════════════════════
   ui.js — shared UI helpers
══════════════════════════════════════ */
import { t } from './i18n.js';

/* ── Toast ── */
export function toast(msg, type = 'success', duration = 3200) {
  const container = document.getElementById('toast-container');
  if (!container) return;
  const el = document.createElement('div');
  el.className = `toast toast-${type}`;
  const icons = { success: '✓', error: '✕', info: 'ℹ' };
  el.innerHTML = `<span class="toast-icon">${icons[type] || '•'}</span><span>${msg}</span>`;
  container.appendChild(el);
  setTimeout(() => {
    el.style.opacity = '0';
    el.style.transform = 'translateX(16px)';
    setTimeout(() => el.remove(), 300);
  }, duration);
}

/* ── Lightbox ── */
export function openLightbox(src) {
  const lb  = document.getElementById('lightbox');
  const img = document.getElementById('lightbox-img');
  if (!lb || !img) return;
  img.src = src;
  lb.classList.add('open');
  document.body.style.overflow = 'hidden';
}
export function closeLightbox() {
  const lb = document.getElementById('lightbox');
  if (lb) lb.classList.remove('open');
  document.body.style.overflow = '';
}

/* ── Spinner ── */
export function setLoading(btnEl, on, origHTML) {
  if (!btnEl) return;
  btnEl.disabled = on;
  if (on) {
    btnEl.dataset.orig = origHTML || btnEl.innerHTML;
    btnEl.innerHTML = `<span class="spin">↻</span>`;
  } else {
    btnEl.innerHTML = btnEl.dataset.orig || origHTML || btnEl.innerHTML;
  }
}

/* ── Format price ── */
export function formatPrice(p) {
  return `₾${parseFloat(p || 0).toFixed(2)}`;
}

/* ══════════════════════════════════════
   CAROUSEL
   Structure:
     .product-carousel[data-key]
       .car-track-wrap          ← overflow:hidden
         .car-track             ← flex, translateX animates
           .car-slide × N
       .carr.prev               ← z-index above overlay
       .carr.next
       .car-dots                ← z-index above overlay
       .img-badge
       .admin-img-overlay       ← center strip only, pointer-events on hover
══════════════════════════════════════ */

export function buildCarousel(imgs, cardKey) {
  if (!imgs.length) {
    return `<div class="product-carousel no-img" data-key="${cardKey}">
      <div class="car-track-wrap">
        <div class="car-no-img">📷</div>
      </div>
    </div>`;
  }

  const slides = imgs.map(url =>
    `<div class="car-slide">
      <img src="${url}" alt="" loading="lazy" draggable="false"
           onerror="this.closest('.car-slide').style.visibility='hidden'"/>
    </div>`
  ).join('');

  const multi = imgs.length > 1;

  const arrows = multi ? `
    <button class="carr carr-prev" data-dir="-1" title="Previous">‹</button>
    <button class="carr carr-next" data-dir="1"  title="Next">›</button>` : '';

  const dots = multi ? `
    <div class="car-dots">
      ${imgs.map((_, i) => `<button class="cdot${i === 0 ? ' active' : ''}" data-i="${i}"></button>`).join('')}
    </div>` : '';

  const badge = multi ? `<span class="img-badge">1/${imgs.length}</span>` : '';

  return `<div class="product-carousel" data-key="${cardKey}" data-count="${imgs.length}">
    <div class="car-track-wrap">
      <div class="car-track">${slides}</div>
    </div>
    ${arrows}
    ${dots}
    ${badge}
  </div>`;
}

export function initCarousel(container, imgs) {
  if (!container) return;

  // Single image — just wire lightbox
  if (imgs.length <= 1) {
    const img = container.querySelector('.car-slide img');
    if (img && imgs[0]) {
      img.style.cursor = 'zoom-in';
      img.addEventListener('click', e => { e.stopPropagation(); openLightbox(imgs[0]); });
    }
    return;
  }

  const track  = container.querySelector('.car-track');
  const wrap   = container.querySelector('.car-track-wrap');
  const dots   = container.querySelectorAll('.cdot');
  const badge  = container.querySelector('.img-badge');
  let idx = 0;

  function goTo(n) {
    idx = ((n % imgs.length) + imgs.length) % imgs.length;
    // Use the wrap's pixel width — immune to track's own width being N*100%
    const slideW = wrap.offsetWidth;
    track.style.transform = `translateX(-${idx * slideW}px)`;
    dots.forEach((d, i) => d.classList.toggle('active', i === idx));
    if (badge) badge.textContent = `${idx + 1}/${imgs.length}`;
  }

  // Arrow buttons
  container.querySelectorAll('.carr').forEach(btn => {
    btn.addEventListener('click', e => {
      e.stopPropagation();
      e.preventDefault();
      goTo(idx + parseInt(btn.dataset.dir));
    });
  });

  // Dot buttons
  dots.forEach(dot => {
    dot.addEventListener('click', e => {
      e.stopPropagation();
      e.preventDefault();
      goTo(parseInt(dot.dataset.i));
    });
  });

  // Click image → lightbox
  container.querySelectorAll('.car-slide img').forEach((img, i) => {
    img.style.cursor = 'zoom-in';
    img.addEventListener('click', e => {
      e.stopPropagation();
      openLightbox(imgs[i]);
    });
  });

  // Recalculate on resize (viewport changes alter wrap.offsetWidth)
  const resizeObserver = new ResizeObserver(() => {
    const slideW = wrap.offsetWidth;
    track.style.transition = 'none';
    track.style.transform = `translateX(-${idx * slideW}px)`;
    requestAnimationFrame(() => {
      track.style.transition = '';
    });
  });
  resizeObserver.observe(wrap);

  // Touch/swipe support
  let startX = 0;
  wrap.addEventListener('touchstart', e => { startX = e.touches[0].clientX; }, { passive: true });
  wrap.addEventListener('touchend',   e => {
    const diff = startX - e.changedTouches[0].clientX;
    if (Math.abs(diff) > 40) goTo(idx + (diff > 0 ? 1 : -1));
  });
}

/* ── Admin image overlay ── */
export function buildAdminOverlay(hasImages) {
  return `<div class="admin-img-overlay">
    <div class="admin-overlay-btns">
      <button class="aov-btn aov-add" title="Upload image">${t('admin.addImage')}</button>
      ${hasImages ? `<button class="aov-btn aov-del" title="Delete current image">${t('admin.delImage')}</button>` : ''}
    </div>
  </div>`;
}
