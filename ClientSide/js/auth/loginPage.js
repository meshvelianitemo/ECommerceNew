/* loginPage.js */
import { BASE, getCurrentUser, saveToken, decodeJWT } from '../auth.js';
import { parseResult } from '../api.js'; 
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

    const result = await parseResult(r);

if (result.ok) {
  const token =
    result.data?.token       ||
    result.data?.Token       ||
    result.data?.accessToken ||
    result.data?.AccessToken ||
    result.data?.jwt         ||
    result.data?.JWT         || null;

  if (token && token.includes('.') && decodeJWT(token)) {
    saveToken(token);
  }

  toast('Welcome back!', 'success');
  setTimeout(() => { window.location.href = '../index.html'; }, 700);

} else {
  errEl.textContent   = result.message || 'Invalid email or password.';
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
