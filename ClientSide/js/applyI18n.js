/* ══════════════════════════════════════
   applyI18n.js
   Call applyI18n() after any language change.
   Reads data-i18n, data-i18n-placeholder, data-i18n-html attributes.
   Also exports initLangToggle() to wire the toggle button.
══════════════════════════════════════ */
import { t, getLang, setLang } from './i18n.js';

/** Apply all translations in the DOM */
export function applyI18n() {
  const lang = getLang();

  // data-i18n="key" → element.textContent
  document.querySelectorAll('[data-i18n]').forEach(el => {
    const key = el.dataset.i18n;
    const val = t(key);
    if (val !== key) el.textContent = val;
  });

  // data-i18n-placeholder="key" → input.placeholder
  document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
    const key = el.dataset.i18nPlaceholder;
    const val = t(key);
    if (val !== key) el.placeholder = val;
  });

  // data-i18n-html="key" → element.innerHTML (for rich strings)
  document.querySelectorAll('[data-i18n-html]').forEach(el => {
    const key = el.dataset.i18nHtml;
    const val = t(key);
    if (val !== key) el.innerHTML = val;
  });

  // Update <html lang> attr
  document.documentElement.lang = lang === 'ka' ? 'ka' : 'en';
}

/** Wire the lang toggle button (must exist in DOM) */
export function initLangToggle(onChangeCallback) {
  const btn   = document.getElementById('btn-lang');
  const flag  = document.getElementById('lang-flag');
  const label = document.getElementById('lang-label');
  if (!btn) return;

  function updateToggle() {
    const lang = getLang();
    if (lang === 'ka') {
      flag.textContent  = '🇬🇪';
      label.textContent = 'ქარ';
      btn.title = 'Switch to English';
    } else {
      flag.textContent  = '🇬🇧';
      label.textContent = 'ENG';
      btn.title = 'ქართულზე გადართვა';
    }
  }

  btn.addEventListener('click', () => {
    const newLang = getLang() === 'ka' ? 'en' : 'ka';
    setLang(newLang);
    updateToggle();
    applyI18n();
    if (onChangeCallback) onChangeCallback(newLang);
  });

  updateToggle();
  applyI18n();
}
