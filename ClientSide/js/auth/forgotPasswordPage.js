/* forgotPasswordPage.js — 3-step password recovery */
import { BASE } from '../auth.js';
import { toast } from '../ui.js';

/* ── State ── */
let recoveryEmail = '';   // persisted across all 3 steps
let currentStep   = 1;

/* ── Step transition ── */
function goToStep(n) {
  currentStep = n;
  const slider = document.getElementById('recovery-slider');
  // Slide: each step is 100% wide, translate left by (n-1) * 100%
  slider.style.transform = `translateX(-${(n - 1) * 100}%)`;

  // Update left-panel step indicators
  [1, 2, 3].forEach(i => {
    const el = document.getElementById(`rstep-${i}`);
    el.classList.remove('active', 'done');
    if (i < n)  el.classList.add('done');
    if (i === n) el.classList.add('active');
  });

  // Update hint text
  const hints = {
    1: 'Enter the email address associated with your EkkoShop account.',
    2: `We sent a recovery code to ${recoveryEmail}. Check your inbox (and spam folder).`,
    3: 'Almost there — choose a new strong password for your account.',
  };
  document.getElementById('recovery-hint').textContent = hints[n];

  // Update step 2 sub text with email
  if (n === 2) {
    document.getElementById('step2-sub').textContent =
      `We sent a code to ${recoveryEmail}`;
  }
}

/* ── Password strength ── */
document.getElementById('new-password').addEventListener('input', function () {
  const v = this.value;
  let score = 0;
  if (v.length >= 8)           score++;
  if (/[A-Z]/.test(v))        score++;
  if (/[0-9]/.test(v))        score++;
  if (/[^A-Za-z0-9]/.test(v)) score++;
  const fill   = document.getElementById('new-pw-fill');
  const label  = document.getElementById('new-pw-label');
  const labels = ['', 'Weak', 'Fair', 'Good', 'Strong'];
  const colors = ['', '#dc2626', '#d97706', '#2563eb', '#059669'];
  fill.style.width      = `${(score / 4) * 100}%`;
  fill.style.background = colors[score] || '#e2e8f0';
  label.textContent     = labels[score] || '';
  label.style.color     = colors[score] || '';
});

/* ── Toggle password visibility ── */
document.querySelectorAll('.toggle-pw').forEach(btn => {
  btn.addEventListener('click', () => {
    const inp = document.getElementById(btn.dataset.target);
    inp.type = inp.type === 'password' ? 'text' : 'password';
    btn.textContent = inp.type === 'password' ? '👁' : '🙈';
  });
});

/* ══════════════════════════════════════
   STEP 1 — Send recovery code
══════════════════════════════════════ */
document.getElementById('form-step1').addEventListener('submit', async (e) => {
  e.preventDefault();
  const email  = document.getElementById('rec-email').value.trim();
  const errEl  = document.getElementById('step1-error');
  const btn    = document.getElementById('btn-step1');
  const btnTxt = document.getElementById('btn-step1-text');

  errEl.style.display = 'none';
  document.getElementById('rec-email-err').textContent = '';

  if (!email || !email.includes('@')) {
    document.getElementById('rec-email-err').textContent = 'Valid email required';
    return;
  }

  btn.disabled = true;
  btnTxt.textContent = 'Sending…';

  try {
    const r = await fetch(`${BASE}/api/Authentication/SendPasswordRecovery`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email }),
    });

    const text = await r.text();
    let data; try { data = JSON.parse(text); } catch { data = null; }

    if (r.ok) {
      recoveryEmail = email;   // store for all subsequent steps
      toast('Recovery code sent!', 'success');
      goToStep(2);
    } else {
      const msg = data?.message || data?.Message || data?.title || `Error ${r.status}`;
      errEl.textContent   = msg;
      errEl.style.display = 'block';
    }
  } catch {
    errEl.textContent   = 'Cannot reach the server.';
    errEl.style.display = 'block';
  } finally {
    btn.disabled    = false;
    btnTxt.textContent = 'Send Recovery Code';
  }
});

/* ══════════════════════════════════════
   STEP 2 — Verify recovery code
══════════════════════════════════════ */
document.getElementById('form-step2').addEventListener('submit', async (e) => {
  e.preventDefault();
  const recoveryCode = document.getElementById('rec-code').value.trim();
  const errEl        = document.getElementById('step2-error');
  const btn          = document.getElementById('btn-step2');
  const btnTxt       = document.getElementById('btn-step2-text');

  errEl.style.display = 'none';
  document.getElementById('rec-code-err').textContent = '';

  if (!recoveryCode) {
    document.getElementById('rec-code-err').textContent = 'Code is required';
    return;
  }

  btn.disabled = true;
  btnTxt.textContent = 'Verifying…';

  try {
    const r = await fetch(`${BASE}/api/Authentication/VerifyPasswordRecoveryCode`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email: recoveryEmail, recoveryCode }),
    });

    const text = await r.text();
    let data; try { data = JSON.parse(text); } catch { data = null; }

    if (r.ok) {
      toast('Code verified!', 'success');
      goToStep(3);
    } else {
      const msg = data?.message || data?.Message || data?.title || 'Invalid or expired code.';
      errEl.textContent   = msg;
      errEl.style.display = 'block';
    }
  } catch {
    errEl.textContent   = 'Cannot reach the server.';
    errEl.style.display = 'block';
  } finally {
    btn.disabled    = false;
    btnTxt.textContent = 'Verify Code';
  }
});

/* Resend recovery code */
document.getElementById('btn-resend-rec').addEventListener('click', async () => {
  if (!recoveryEmail) { toast('Go back and enter your email first', 'error'); return; }
  const btn = document.getElementById('btn-resend-rec');
  btn.disabled = true;
  btn.textContent = '↺ Sending…';
  try {
    const r = await fetch(`${BASE}/api/Authentication/SendPasswordRecovery`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email: recoveryEmail }),
    });
    if (r.ok) toast('New code sent!', 'success');
    else toast('Failed to resend code', 'error');
  } catch {
    toast('Cannot reach the server.', 'error');
  } finally {
    btn.disabled = false;
    btn.textContent = '↺ Resend code';
  }
});

/* Back to step 1 */
document.getElementById('back-to-step1').addEventListener('click', (e) => {
  e.preventDefault();
  goToStep(1);
});

/* ══════════════════════════════════════
   STEP 3 — Reset password
══════════════════════════════════════ */
document.getElementById('form-step3').addEventListener('submit', async (e) => {
  e.preventDefault();
  const newPassword     = document.getElementById('new-password').value;
  const confirmPassword = document.getElementById('new-confirm').value;
  const errEl           = document.getElementById('step3-error');
  const successEl       = document.getElementById('step3-success');
  const btn             = document.getElementById('btn-step3');
  const btnTxt          = document.getElementById('btn-step3-text');

  errEl.style.display     = 'none';
  successEl.style.display = 'none';
  document.getElementById('new-pw-err').textContent      = '';
  document.getElementById('new-confirm-err').textContent = '';

  let valid = true;
  if (newPassword.length < 8)           { document.getElementById('new-pw-err').textContent = 'Minimum 8 characters'; valid = false; }
  if (newPassword !== confirmPassword)  { document.getElementById('new-confirm-err').textContent = 'Passwords do not match'; valid = false; }
  if (!valid) return;

  btn.disabled = true;
  btnTxt.textContent = 'Resetting…';

  try {
    const r = await fetch(`${BASE}/api/Authentication/ResetPassword`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        email: recoveryEmail,   // ← never asked user to retype
        newPassword,
        confirmPassword,
      }),
    });

    const text = await r.text();
    let data; try { data = JSON.parse(text); } catch { data = null; }

    if (r.ok) {
      successEl.textContent   = '✓ Password reset! Redirecting to login…';
      successEl.style.display = 'block';
      toast('Password reset successfully!', 'success');
      setTimeout(() => { window.location.href = '../login.html'; }, 1800);
    } else {
      const msg = data?.message || data?.Message || data?.title || `Error ${r.status}`;
      errEl.textContent   = msg;
      errEl.style.display = 'block';
    }
  } catch {
    errEl.textContent   = 'Cannot reach the server.';
    errEl.style.display = 'block';
  } finally {
    btn.disabled    = false;
    btnTxt.textContent = 'Reset Password';
  }
});
