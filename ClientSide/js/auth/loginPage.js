/* loginPage.js */
import { BASE, getCurrentUser, saveToken, decodeJWT } from '../auth.js';
import { toast } from '../ui.js';

if (getCurrentUser()) window.location.href = '../index.html';

document.querySelectorAll('.toggle-pw').forEach(btn => {
  btn.addEventListener('click', () => {
    const inp = document.getElementById(btn.dataset.target);
    inp.type = inp.type === 'password' ? 'text' : 'password';
    btn.textContent = inp.type === 'password' ? '👁' : '🙈';
  });
});

document.getElementById('login-form').addEventListener('submit', async (e) => {
  e.preventDefault();

  const email    = document.getElementById('login-email').value.trim();
  const password = document.getElementById('login-password').value;
  const errEl    = document.getElementById('login-error');
  const btn      = document.getElementById('btn-login');
  const btnText  = document.getElementById('login-btn-text');

  errEl.style.display = 'none';
  document.getElementById('login-email-err').textContent = '';
  document.getElementById('login-pw-err').textContent = '';

  let valid = true;
  if (!email)    { document.getElementById('login-email-err').textContent = 'Email is required'; valid = false; }
  if (!password) { document.getElementById('login-pw-err').textContent = 'Password is required'; valid = false; }
  if (!valid) return;

  btn.disabled = true;
  btnText.textContent = 'Logging in…';

  try {
    const r = await fetch(`${BASE}/api/Authentication/login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, password }),
      credentials: 'include',
    });

    const text = await r.text();
    let data;
    try { data = JSON.parse(text); } catch { data = null; }

    if (r.ok) {
      /*
       * The server sets the 'jwt' cookie on its own domain (localhost:7060).
       * Since our frontend is on a different port, document.cookie can't read it.
       * So we also look for the token in the response body — check every
       * common field name the API might use.
       */
      const token =
        data?.token        ||
        data?.Token        ||
        data?.accessToken  ||
        data?.AccessToken  ||
        data?.jwt          ||
        data?.JWT          ||
        // Also check Authorization response header (if server exposes it)
        (() => {
          const h = r.headers.get('Authorization') || r.headers.get('authorization') || '';
          return h.startsWith('Bearer ') ? h.slice(7).trim() : null;
        })() ||
        null;

      if (token && token.includes('.') && decodeJWT(token)) {
        // We got the token — save it to localStorage so all pages can read it
        saveToken(token);
      }
      // Even if we didn't get the token in the body, login was successful
      // and the cookie was set — it'll work if frontend and API are same origin

      toast('Welcome back!', 'success');
      setTimeout(() => { window.location.href = '../index.html'; }, 700);

    } else {
      const msg = data?.message || data?.Message || data?.title
        || (r.status === 401 ? 'Invalid email or password.' : `Error ${r.status}`);
      errEl.textContent   = msg;
      errEl.style.display = 'block';
    }

  } catch (err) {
    errEl.textContent = err.message.toLowerCase().includes('fetch')
      ? 'Cannot reach the server. Is the API running on https://localhost:7060?'
      : err.message;
    errEl.style.display = 'block';
  } finally {
    btn.disabled        = false;
    btnText.textContent = 'Log In';
  }
});
