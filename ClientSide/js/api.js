/* ══════════════════════════════════════
   api.js — shared API helpers
══════════════════════════════════════ */
export { BASE, authFetch } from './auth.js';
import { BASE, authFetch } from './auth.js';

export function esc(s) {
  return String(s)
    .replace(/&/g,'&amp;').replace(/</g,'&lt;')
    .replace(/>/g,'&gt;').replace(/"/g,'&quot;');
}

export function getImages(raw) {
  if (!raw) return [];
  if (Array.isArray(raw)) return raw.filter(u => u && String(u).trim());
  if (typeof raw === 'string' && raw.trim()) return [raw.trim()];
  return [];
}

export function extractImages(obj) {
  return getImages(obj?.imageUrl ?? obj?.imageUrls ?? obj?.ImageUrl ?? obj?.ImageUrls ?? null);
}

export function extractImageKey(url) {
  if (!url) return '';
  const match = url.match(/ProductImages\/([^?#]+)/);
  if (match) return `ProductImages/${decodeURIComponent(match[1])}`;
  return decodeURIComponent(url.split('?')[0]);
}

/** Fetch all products with filters */
export async function fetchProducts({ page = 1, pageSize = 20, search = '', categoryId = '', minPrice = '', maxPrice = '', available = '' } = {}) {
  const params = new URLSearchParams();
  if (search)     params.set('Search', search);
  if (categoryId) params.set('CategoryId', categoryId);
  if (minPrice)   params.set('MinPrice', minPrice);
  if (maxPrice)   params.set('MaxPrice', maxPrice);
  if (available !== '') params.set('Available', available);
  params.set('Page', page);
  params.set('PageSize', pageSize);
  const r = await fetch(`${BASE}/api/Products/All?${params}`);
  if (r.status === 204) return { items: [], totalCount: 0, page: 1, totalPages: 0 };
  return r.json();
}

/** Fetch single product */
export async function fetchProduct(id) {
  const r = await fetch(`${BASE}/api/Products/${id}`);
  if (!r.ok) return null;
  return r.json();
}

/** Fetch fresh image URLs */
export async function fetchProductImages(productId) {
  try {
    const r = await authFetch(`${BASE}/api/Products/GetImageUrls/${productId}`);
    // 500 = server throws when no images — treat as empty
    if (!r.ok || r.status === 204 || r.status === 500) return [];
    const d = await r.json();
    return getImages(d.imageUrls ?? d.ImageUrls ?? d.imageUrl ?? d.ImageUrl);
  } catch { return []; }
}

/** Upload product image (admin) */
export async function uploadProductImage(productId, file, userId) {
  const fd = new FormData();
  fd.append('ProductId', productId);   // form field
  fd.append('UserId', userId);          // form field
  fd.append('Image', file);
  // ProductId also as query param (Swagger shows it there)
  const url = `${BASE}/api/Products/UploadImage?ProductId=${productId}`;
  return authFetch(url, { method: 'POST', body: fd });
}

/** Delete product image (admin) */
export async function deleteProductImage(productId, imageUrl) {
  const imageKey = extractImageKey(imageUrl);
  return authFetch(`${BASE}/api/Products/DeleteImage`, {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ productId: parseInt(productId), imageKey }),
  });
}

/** Create product (admin) */
export async function createProduct(dto) {
  return authFetch(`${BASE}/api/Products/Create`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(dto),
  });
}

/** Update product (admin) */
export async function updateProduct(dto) {
  return authFetch(`${BASE}/api/Products/Update`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(dto),
  });
}

/** Delete product (admin) */
export async function deleteProduct(productId, userId) {
  return authFetch(`${BASE}/api/Products/Delete`, {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ productId: parseInt(productId), userId: parseInt(userId) }),
  });
}

/* ══════════════════════════════════════
   WISHLIST API
══════════════════════════════════════ */

/** Fetch wishlist for a user (paginated) */
export async function fetchWishlist({ userId, page = 1, pageSize = 50, available = '' } = {}) {
  const params = new URLSearchParams();
  params.set('UserId', userId);
  params.set('Page', page);
  params.set('PageSize', pageSize);
  if (available !== '') params.set('Available', available);

  const token = (await import('./auth.js')).getToken();
  console.log('[Wishlist] GET userId=', userId, '| token present=', !!token, '| token prefix=', token?.slice(0,30));

  const r = await authFetch(`${BASE}/api/Products/Wishlist?${params}`);
  console.log('[Wishlist] GET status=', r.status);
  if (!r.ok || r.status === 204) return [];
  const data = await r.json();
  return Array.isArray(data) ? data : (data.items ?? [data]);
}

/** Add product to wishlist */
export async function addToWishlist(userId, productId) {
  console.log('[Wishlist] ADD userId=', userId, 'productId=', productId);
  const r = await authFetch(`${BASE}/api/Products/Wishlist/Add`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ userId: parseInt(userId), productId: parseInt(productId) }),
  });
  console.log('[Wishlist] ADD status=', r.status);
  return r;
}

/** Remove product from wishlist */
export async function removeFromWishlist(userId, productId) {
  console.log('[Wishlist] REMOVE userId=', userId, 'productId=', productId);
  const r = await authFetch(`${BASE}/api/Products/Wishlist/Remove`, {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ userId: parseInt(userId), productId: parseInt(productId) }),
  });
  console.log('[Wishlist] REMOVE status=', r.status);
  return r;
}

/* ══════════════════════════════════════
   CART API
══════════════════════════════════════ */

/** Fetch cart items for a user */
export async function fetchCartItems({ userId, page = 1, pageSize = 100 } = {}) {
  const params = new URLSearchParams();
  params.set('UserId', userId);
  params.set('Page', page);
  params.set('PageSize', pageSize);
  const r = await authFetch(`${BASE}/api/Products/Cart/Items?${params}`);
  if (!r.ok || r.status === 204) return [];
  const data = await r.json();
  return Array.isArray(data) ? data : (data.items ?? [data]);
}

/** Add product to cart */
export async function addToCart(userId, productId, quantity = 1) {
  return authFetch(`${BASE}/api/Products/Cart/Add`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      userId: parseInt(userId),
      productId: parseInt(productId),
      quantity: parseInt(quantity),
    }),
  });
}

/** Remove product from cart */
export async function removeFromCart(userId, productId) {
  return authFetch(`${BASE}/api/Products/Cart/Remove`, {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      userId: parseInt(userId),
      productId: parseInt(productId),
    }),
  });
}
