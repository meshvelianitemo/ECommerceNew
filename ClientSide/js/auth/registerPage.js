/* registerPage.js */
import { BASE, getCurrentUser } from '../auth.js';
import { toast } from '../ui.js';

if (getCurrentUser()) window.location.href = '../index.html';

document.querySelectorAll('.toggle-pw').forEach(btn => {
  btn.addEventListener('click', () => {
    const inp = document.getElementById(btn.dataset.target);
    inp.type = inp.type === 'password' ? 'text' : 'password';
    btn.textContent = inp.type === 'password' ? '👁' : '🙈';
  });
});

document.getElementById('reg-password').addEventListener('input', function () {
  const v = this.value;
  let score = 0;
  if (v.length >= 8)           score++;
  if (/[A-Z]/.test(v))        score++;
  if (/[0-9]/.test(v))        score++;
  if (/[^A-Za-z0-9]/.test(v)) score++;
  const fill   = document.getElementById('pw-fill');
  const label  = document.getElementById('pw-label');
  const labels = ['', 'Weak', 'Fair', 'Good', 'Strong'];
  const colors = ['', '#dc2626', '#d97706', '#2563eb', '#059669'];
  fill.style.width      = `${(score / 4) * 100}%`;
  fill.style.background = colors[score] || '#e2e8f0';
  label.textContent     = labels[score] || '';
  label.style.color     = colors[score] || '';
});

document.getElementById('register-form').addEventListener('submit', async (e) => {
  e.preventDefault();
  const errEl    = document.getElementById('reg-error');
  const btn      = document.getElementById('btn-register');
  const btnText  = document.getElementById('reg-btn-text');

  errEl.style.display = 'none';
  ['reg-first-err','reg-last-err','reg-email-err','reg-pw-err','reg-confirm-err'].forEach(id => {
    document.getElementById(id).textContent = '';
  });

  const firstName       = document.getElementById('reg-first').value.trim();
  const lastName        = document.getElementById('reg-last').value.trim();
  const email           = document.getElementById('reg-email').value.trim();
  const password        = document.getElementById('reg-password').value;
  const confirmPassword = document.getElementById('reg-confirm').value;
  const terms           = document.getElementById('reg-terms').checked;

  let valid = true;
  if (!firstName)                    { document.getElementById('reg-first-err').textContent = 'Required'; valid = false; }
  if (!lastName)                     { document.getElementById('reg-last-err').textContent = 'Required'; valid = false; }
  if (!email || !email.includes('@')){ document.getElementById('reg-email-err').textContent = 'Valid email required'; valid = false; }
  if (password.length < 8)           { document.getElementById('reg-pw-err').textContent = 'Minimum 8 characters'; valid = false; }
  if (password !== confirmPassword)  { document.getElementById('reg-confirm-err').textContent = 'Passwords do not match'; valid = false; }
  if (!terms) { errEl.textContent = 'You must accept the Terms of Service.'; errEl.style.display = 'block'; valid = false; }
  if (!valid) return;

  btn.disabled = true;
  btnText.textContent = 'Creating account…';

  try {
    const r = await fetch(`${BASE}/api/Authentication/Register`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ firstName, lastName, email, password, confirmPassword }),
    });

    const text = await r.text();
    let data; try { data = JSON.parse(text); } catch { data = null; }

    if (r.ok) {
      toast('Account created! Check your email for the verification code.', 'success');
      // Pass email to verify page so user doesn't retype it
      sessionStorage.setItem('ekko_verify_email', email);
      setTimeout(() => { window.location.href = '../verify-email.html'; }, 900);
    } else {
      let msg = data?.message || data?.Message || data?.title || `Registration failed (${r.status}).`;
      if (data?.errors) msg = Object.values(data.errors).flat().join(' ');
      errEl.textContent = msg;
      errEl.style.display = 'block';
    }
  } catch (err) {
    errEl.textContent = err.message.toLowerCase().includes('fetch')
      ? 'Cannot reach the server. Is the API running on https://localhost:7060?'
      : err.message;
    errEl.style.display = 'block';
  } finally {
    btn.disabled = false;
    btnText.textContent = 'Create Account';
  }
});
