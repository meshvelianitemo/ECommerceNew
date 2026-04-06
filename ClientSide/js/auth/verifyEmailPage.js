/* verifyEmailPage.js */
import { BASE } from '../auth.js';
import { toast } from '../ui.js';

// Pre-fill email if coming from register
const savedEmail = sessionStorage.getItem('ekko_verify_email') || '';
const emailInput = document.getElementById('verify-email');
if (savedEmail) {
  emailInput.value = savedEmail;
  document.getElementById('verify-sub').textContent =
    `Enter the code we sent to ${savedEmail}`;
}

document.getElementById('verify-form').addEventListener('submit', async (e) => {
  e.preventDefault();
  const email            = emailInput.value.trim();
  const verificationCode = document.getElementById('verify-code').value.trim();
  const errEl            = document.getElementById('verify-error');
  const successEl        = document.getElementById('verify-success');
  const btn              = document.getElementById('btn-verify');
  const btnText          = document.getElementById('verify-btn-text');

  errEl.style.display     = 'none';
  successEl.style.display = 'none';
  document.getElementById('verify-email-err').textContent = '';
  document.getElementById('verify-code-err').textContent  = '';

  let valid = true;
  if (!email)            { document.getElementById('verify-email-err').textContent = 'Email is required'; valid = false; }
  if (!verificationCode) { document.getElementById('verify-code-err').textContent  = 'Code is required'; valid = false; }
  if (!valid) return;

  btn.disabled = true;
  btnText.textContent = 'Verifying…';

  try {
    const r = await fetch(`${BASE}/api/Authentication/VerifyEmail`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, verificationCode }),
    });

    const text = await r.text();
    let data; try { data = JSON.parse(text); } catch { data = null; }

    if (r.ok) {
      sessionStorage.removeItem('ekko_verify_email');
      successEl.textContent   = '✓ Email verified! Redirecting to login…';
      successEl.style.display = 'block';
      toast('Email verified successfully!', 'success');
      setTimeout(() => { window.location.href = '../login.html'; }, 1500);
    } else {
      const msg = data?.message || data?.Message || data?.title || `Verification failed (${r.status})`;
      errEl.textContent   = msg;
      errEl.style.display = 'block';
    }
  } catch (err) {
    errEl.textContent   = 'Cannot reach the server.';
    errEl.style.display = 'block';
  } finally {
    btn.disabled        = false;
    btnText.textContent = 'Verify Email';
  }
});

// Resend — just calls Register again? No, server should have a resend endpoint.
// For now we hint the user to re-register or wait.
document.getElementById('btn-resend').addEventListener('click', async () => {
  const email = emailInput.value.trim();
  if (!email) { toast('Enter your email first', 'error'); return; }
  toast('Resend not yet implemented by the API — check your spam folder.', 'info');
});
