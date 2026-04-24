/* ══════════════════════════════════════════
   SHARED COMPONENTS — Navbar, Drawer, Footer
   Injected into all pages except index.html
   ══════════════════════════════════════════ */

function getNavbarHTML() {
  return `
  <nav class="navbar" id="navbar">
    <div class="container navbar__inner">
      <a href="/index.html" class="navbar__logo" data-i18n="logo">ماركت اولاد رزق</a>
      <ul class="navbar__links" id="navLinks">
        <li><a href="/index.html" data-i18n="nav_home">الرئيسية</a></li>
        <li><a href="/categories.html" data-i18n="nav_categories">الأقسام</a></li>
        <li><a href="/offers.html" data-i18n="nav_offers">العروض</a></li>
        <li><a href="/about.html" data-i18n="nav_about">من نحن</a></li>
        <li><a href="/contact.html" data-i18n="nav_contact">تواصل معنا</a></li>
      </ul>
      <div class="navbar__actions">
        <button class="icon-btn" aria-label="Search" id="searchBtn">🔍</button>
        <a href="/cart.html" class="icon-btn cart-btn" aria-label="Cart" id="cartBtn">🛒<span class="cart-badge" id="cartBadge">0</span></a>
        <button class="dark-toggle" id="darkToggle" aria-label="Night Mode">🌙</button>
        <button class="lang-toggle" id="langToggle" data-i18n="lang_toggle">EN</button>
        <button class="hamburger" id="hamburger" aria-label="Menu">
          <span></span><span></span><span></span>
        </button>
      </div>
    </div>
  </nav>
  <div class="mobile-drawer" id="mobileDrawer">
    <div class="mobile-drawer__overlay" id="drawerOverlay"></div>
    <div class="mobile-drawer__panel">
      <button class="mobile-drawer__close" id="drawerClose">✕</button>
      <ul class="mobile-drawer__links">
        <li><a href="/index.html" data-i18n="nav_home">الرئيسية</a></li>
        <li><a href="/categories.html" data-i18n="nav_categories">الأقسام</a></li>
        <li><a href="/offers.html" data-i18n="nav_offers">العروض</a></li>
        <li><a href="/about.html" data-i18n="nav_about">من نحن</a></li>
        <li><a href="/contact.html" data-i18n="nav_contact">تواصل معنا</a></li>
      </ul>
    </div>
  </div>`;
}

function getFooterHTML() {
  return `
  <footer class="footer">
    <div class="container footer__grid">
      <div class="footer__col">
        <h3 class="footer__logo" data-i18n="logo">ماركت اولاد رزق</h3>
        <p data-i18n="footer_tagline">ماركت متميز يقدم أكبر تشكيلة من المنتجات المصرية بأفضل الأسعار</p>
      </div>
      <div class="footer__col">
        <h4 data-i18n="footer_links">روابط سريعة</h4>
        <ul>
          <li><a href="/index.html" data-i18n="nav_home">الرئيسية</a></li>
          <li><a href="/categories.html" data-i18n="nav_categories">الأقسام</a></li>
          <li><a href="/offers.html" data-i18n="nav_offers">العروض</a></li>
          <li><a href="/about.html" data-i18n="nav_about">من نحن</a></li>
        </ul>
      </div>
      <div class="footer__col">
        <h4 data-i18n="footer_contact">تواصل معنا</h4>
        <ul>
          <li>📍 <span data-i18n="footer_address">قرية نوي مركز شبين القناطر القليوبية</span></li>
          <li>📞 <span dir="ltr">+20 2 1234 5678</span></li>
          <li>✉️ info@awladrizk.eg</li>
        </ul>
      </div>
      <div class="footer__col">
        <h4 data-i18n="footer_social">تابعنا</h4>
        <div class="footer__socials">
          <a href="#" class="social-icon hover-lift" aria-label="Facebook">f</a>
          <a href="#" class="social-icon hover-lift" aria-label="Instagram">📷</a>
          <a href="#" class="social-icon hover-lift" aria-label="Twitter">𝕏</a>
          <a href="#" class="social-icon hover-lift" aria-label="WhatsApp">💬</a>
        </div>
      </div>
    </div>
    <div class="footer__bottom container">
      <p data-i18n="footer_copy">© ٢٠٢٦ ماركت اولاد رزق. جميع الحقوق محفوظة.</p>
      <div class="footer__payments">
        <span class="pay-icon">VISA</span>
        <span class="pay-icon">MC</span>
        <span class="pay-icon">فوري</span>
        <span class="pay-icon">ميزة</span>
      </div>
    </div>
  </footer>`;
}

/* Highlight current page nav link */
function highlightActiveNav() {
  const page = location.pathname.split('/').pop() || 'index.html';
  document.querySelectorAll('.navbar__links a, .mobile-drawer__links a').forEach(a => {
    if (a.getAttribute('href') === page) a.classList.add('active-link');
  });
}

/* Inject shared components into page */
function injectSharedComponents() {
  const navPlaceholder = document.getElementById('nav-placeholder');
  const footerPlaceholder = document.getElementById('footer-placeholder');
  if (navPlaceholder) navPlaceholder.innerHTML = getNavbarHTML();
  if (footerPlaceholder) footerPlaceholder.innerHTML = getFooterHTML();
  highlightActiveNav();
}

document.addEventListener('DOMContentLoaded', injectSharedComponents);
