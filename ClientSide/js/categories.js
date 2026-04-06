/* ══════════════════════════════════════
   categories.js — matches DB seed exactly
══════════════════════════════════════ */
import { tCat, t } from './i18n.js';

export const CATEGORIES = [
  // Parents
  { id: 1,   name: 'Gaming Consoles & Accessories', parentId: null },
  { id: 2,   name: 'PC Hardware & Components',      parentId: null },
  { id: 3,   name: 'PC & Gaming Peripherals',       parentId: null },
  { id: 4,   name: 'Gaming PCs & Laptops',          parentId: null },
  { id: 5,   name: 'Services',                      parentId: null },
  // Gaming Consoles & Accessories
  { id: 101, name: 'Consoles',                      parentId: 1 },
  { id: 102, name: 'Gamepads & Controllers',        parentId: 1 },
  { id: 103, name: 'Controller Accessories',        parentId: 1 },
  { id: 104, name: 'Gift Cards / Subscriptions',    parentId: 1 },
  // PC Hardware & Components
  { id: 201, name: 'Processors (CPU)',              parentId: 2 },
  { id: 202, name: 'Graphics Cards (GPU)',          parentId: 2 },
  { id: 203, name: 'Motherboards',                  parentId: 2 },
  { id: 204, name: 'Memory (RAM)',                  parentId: 2 },
  { id: 205, name: 'Storage (SSD / HDD / NVMe)',   parentId: 2 },
  { id: 206, name: 'Power Supplies (PSU)',          parentId: 2 },
  { id: 207, name: 'PC Cases',                      parentId: 2 },
  { id: 208, name: 'Cooling Solutions',             parentId: 2 },
  { id: 209, name: 'Mining / Crypto Gear',          parentId: 2 },
  // PC & Gaming Peripherals
  { id: 301, name: 'Monitors',                      parentId: 3 },
  { id: 302, name: 'Keyboards',                     parentId: 3 },
  { id: 303, name: 'Mice',                          parentId: 3 },
  { id: 304, name: 'Headsets',                      parentId: 3 },
  { id: 305, name: 'Speakers',                      parentId: 3 },
  { id: 306, name: 'Other Accessories',             parentId: 3 },
  // Gaming PCs & Laptops
  { id: 401, name: 'Desktop PCs',                   parentId: 4 },
  { id: 402, name: 'Gaming Laptops',                parentId: 4 },
  { id: 403, name: 'Laptop Accessories',            parentId: 4 },
  // Services
  { id: 501, name: 'PC / Laptop Assembly',          parentId: 5 },
  { id: 502, name: 'PC / Laptop Repair',            parentId: 5 },
  { id: 503, name: 'Upgrades & Custom Builds',      parentId: 5 },
  { id: 504, name: 'Data Recovery',                 parentId: 5 },
  { id: 505, name: 'Consultation & Troubleshooting',parentId: 5 },
];

export const CATEGORY_ICONS = {
  1: '🎮', 2: '🖥', 3: '⌨️', 4: '💻', 5: '🔧',
};

export function getParentCategories() {
  return CATEGORIES.filter(c => c.parentId === null);
}

export function getChildren(parentId) {
  return CATEGORIES.filter(c => c.parentId === parentId);
}

export function getCategoryById(id) {
  return CATEGORIES.find(c => c.id === parseInt(id)) || null;
}

export function buildCategoryOptions(placeholder) {
  placeholder = placeholder || t('filter.allCategories');
  const parents = getParentCategories();
  let html = `<option value="">${placeholder}</option>`;
  for (const p of parents) {
    const children = getChildren(p.id);
    const pName = tCat(p.name);
    if (children.length) {
      html += `<optgroup label="${pName}">`;
      html += `<option value="${p.id}">${pName} (All)</option>`;
      for (const c of children) {
        html += `<option value="${c.id}">  ${tCat(c.name)}</option>`;
      }
      html += `</optgroup>`;
    } else {
      html += `<option value="${p.id}">${pName}</option>`;
    }
  }
  return html;
}

export function populateCategorySelects() {
  document.querySelectorAll('[data-category-select]').forEach(sel => {
    sel.innerHTML = buildCategoryOptions();
  });
}
