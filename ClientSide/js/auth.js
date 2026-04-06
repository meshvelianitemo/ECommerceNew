/* ══════════════════════════════════════
   auth.js
   Reads JWT from cookie named 'jwt' (set by server).
   Falls back to localStorage key 'ekko_jwt'.
══════════════════════════════════════ */

export const BASE = 'https://localhost:7060';
const LS_KEY = 'ekko_jwt';

/** Decode JWT payload — handles base64url + padding edge cases */
export function decodeJWT(token) {
  try {
    const part = token.split('.')[1];
    // base64url → base64: replace chars and add padding
    const b64 = part.replace(/-/g, '+').replace(/_/g, '/');
    const padded = b64 + '==='.slice((b64.length + 3) % 4);
    return JSON.parse(atob(padded));
  } catch { return null; }
}

/** Read cookie by name — handles = signs inside value */
export function getToken() {
  const name = 'jwt';
  const cookies = document.cookie.split(';');
  for (const c of cookies) {
    const idx = c.indexOf('=');
    if (idx < 0) continue;
    const key = c.slice(0, idx).trim();
    const val = c.slice(idx + 1).trim();
    if (key === name) return decodeURIComponent(val);
  }
  return localStorage.getItem(LS_KEY) || null;
}

/** Save token to localStorage (called after login when token is in body) */
export function saveToken(token) {
  if (token) localStorage.setItem(LS_KEY, token);
}

/** Log out — clear both storage and cookie */
export function clearToken() {
  localStorage.removeItem(LS_KEY);
  document.cookie = 'jwt=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/';
}

/** Decode current token and return user object, or null */
export function getCurrentUser() {
  const token = getToken();
  if (!token) return null;
  const p = decodeJWT(token);
  if (!p) return null;
  if (p.exp && Date.now() / 1000 > p.exp) { clearToken(); return null; }
  return {
    id:        p['UserId'] || p['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
    email:     p['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
    firstName: p['FirstName'],
    lastName:  p['LastName'],
    role:      p['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
    token,
  };
}

export function isAdmin()    { return getCurrentUser()?.role === 'Admin'; }
export function isLoggedIn() { return getCurrentUser() !== null; }

export function authFetch(url, options = {}) {
  const token = getToken();
  const headers = { ...(options.headers || {}) };
  if (token) headers['Authorization'] = `Bearer ${token}`;
  return fetch(url, { ...options, headers, credentials: 'include' });
}
