/* ══════════════════════════════════════════
   i18n SYSTEM + SHARED FUNCTIONALITY
   ══════════════════════════════════════════ */
const TRANSLATIONS = {
  ar: {
    logo: "ماركت اولاد رزق", nav_home: "الرئيسية", nav_categories: "الأقسام",
    nav_offers: "العروض", nav_about: "من نحن", nav_contact: "تواصل معنا", lang_toggle: "EN",
    hero_badge: "🚚 توصيل مجاني للطلبات فوق ٢٠٠ جنيه",
    hero_title: "كل اللي بيتك محتاجه في مكان واحد",
    hero_sub: "أكبر تشكيلة من المنتجات المصرية بأفضل الأسعار، توصلك لباب بيتك",
    hero_cta1: "تسوق الآن", hero_cta2: "عروض اليوم",
    cat_title: "تسوق حسب القسم",
    cat_chips: "شيبسي ومقرمشات", cat_juice: "عصائر ومشروبات", cat_coffee: "قهوة وشاي",
    cat_dairy: "ألبان وأجبان", cat_biscuits: "بسكويت وحلويات", cat_canned: "معلبات",
    cat_clean: "منظفات", cat_pasta: "معكرونة وأرز",
    offer1: "🔥 خصم 30% على شيبسي الأحجام الكبيرة",
    offer2: "🧃 عصير جهينة: اشترِ 3 واحصل على 1 مجاناً",
    offer3: "☕ عرض نسكافيه: الكيس الكبير بـ ٨٥ جنيه بدلاً من ١٢٠",
    offer4: "🚚 توصيل مجاني للطلبات فوق 200 جنيه",
    prod_title: "منتجات مميزة", sale: "خصم", add_cart: "أضف للسلة",
    p1_name: "شيبسي كبيرة - بالجبنة", p1_price: "٢٥ جنيه", p1_old: "٣٥ جنيه",
    p2_name: "عصير جهينة كلاسيك - مانجو", p2_price: "١٨ جنيه / لتر",
    p3_name: "نسكافيه ٣ في ١ - ٣٠ كيس", p3_price: "٨٥ جنيه", p3_old: "١٢٠ جنيه",
    p4_name: "جبنة دومتي أبيض مثلثات", p4_price: "٤٢ جنيه",
    p5_name: "شاي العروسة - ٢٠٠ كيس", p5_price: "٩٥ جنيه", p5_old: "١٢٥ جنيه",
    p6_name: "مولتو كرواسون - ١٢ قطعة", p6_price: "٦٠ جنيه",
    p7_name: "شيبسي بالكاتشب", p7_price: "١٥ جنيه",
    p8_name: "عصير بيتي - جوافة", p8_price: "١٢ جنيه",
    p9_name: "فول كاليفورنيا جاردن", p9_price: "٢٨ جنيه", p9_old: "٣٨ جنيه",
    p10_name: "تونة جنة", p10_price: "٣٥ جنيه",
    p11_name: "مكرونة ريجينا - ٥٠٠ جرام", p11_price: "٢٢ جنيه",
    p12_name: "برسيل غسيل - ٤ كيلو", p12_price: "١٥٥ جنيه", p12_old: "١٩٠ جنيه",
    why_title: "لماذا ماركت اولاد رزق؟",
    feat1_title: "جودة عالية", feat1_desc: "نوفر لك أفضل المنتجات المصرية من مصادر موثوقة",
    feat2_title: "توصيل سريع", feat2_desc: "توصيل خلال ساعة واحدة لجميع المناطق",
    feat3_title: "تشكيلة واسعة", feat3_desc: "كل اللي بيتك محتاجه من منتجات مصرية أصلية",
    feat4_title: "أسعار منافسة", feat4_desc: "أفضل الأسعار مع عروض يومية حصرية",
    test_title: "آراء عملائنا",
    t1_quote: "\"أفضل ماركت في المنطقة، الأسعار ممتازة والتوصيل سريع جداً\"", t1_name: "أمينة محمد",
    t2_quote: "\"التشكيلة كبيرة ومفيش حاجة ناقصة، بقالي سنة بتعامل معاهم\"", t2_name: "خالد عبدالله",
    t3_quote: "\"العروض اليومية توفّر كتير، وخدمة العملاء ممتازة ومتعاونين جداً\"", t3_name: "سارة أحمد",
    news_title: "اشترك في نشرتنا البريدية",
    news_sub: "احصل على أحدث العروض والخصومات مباشرة في بريدك الإلكتروني",
    news_email: "البريد الإلكتروني", news_btn: "اشترك الآن",
    footer_tagline: "ماركت متميز يقدم أكبر تشكيلة من المنتجات المصرية بأفضل الأسعار",
    footer_links: "روابط سريعة", footer_contact: "تواصل معنا", footer_social: "تابعنا",
    footer_address: "قرية نوي مركز شبين القناطر القليوبية",
    footer_copy: "© ٢٠٢٦ ماركت اولاد رزق. جميع الحقوق محفوظة.",
    // Categories page
    cat_page_title: "جميع الأقسام", cat_page_sub: "تصفح منتجاتنا حسب القسم",
    filter_all: "الكل", search_placeholder: "ابحث عن منتج...",
    // Offers page
    offers_page_title: "العروض والخصومات", offers_page_sub: "وفّر أكتر مع عروضنا اليومية",
    offer_valid: "العرض ساري حتى نهاية الأسبوع", offer_terms: "الشروط والأحكام",
    offer_terms_text: "العروض سارية حتى نفاد الكمية. لا يمكن الجمع بين أكثر من عرض. التوصيل المجاني للطلبات فوق ٢٠٠ جنيه فقط.",
    offer_d1: "خصم 30% على كل منتجات شيبسي", offer_d2: "اشترِ 3 جهينة واحصل على 1 مجاناً",
    offer_d3: "نسكافيه ٣ في ١ بـ ٨٥ جنيه بدلاً من ١٢٠", offer_d4: "خصم 20% على كل المنظفات",
    offer_d5: "شاي العروسة ٢٠٠ كيس بـ ٩٥ جنيه فقط", offer_d6: "فول كاليفورنيا بخصم ٢٥%",
    // About page
    about_page_title: "من نحن", about_page_sub: "تعرف على قصة ماركت اولاد رزق",
    about_story_title: "قصتنا", about_story_p1: "بدأ ماركت اولاد رزق كمحل صغير في قرية نوي بشبين القناطر، بحلم بسيط: نوفر لأهل المنطقة كل اللي بيتهم محتاجه بأسعار كويسة وجودة عالية.",
    about_story_p2: "النهاردة، بقينا واحد من أكبر الماركتات في المنطقة، بتشكيلة واسعة من المنتجات المصرية الأصلية وخدمة توصيل سريعة.",
    about_mission_title: "مهمتنا", about_mission: "نوفر لكل بيت مصري منتجات أصلية بأسعار عادلة مع أفضل تجربة تسوق.",
    about_vision_title: "رؤيتنا", about_vision: "نكون الماركت الأول في القليوبية والمنطقة كلها.",
    about_values_title: "قيمنا",
    about_v1: "الأمانة", about_v1d: "نتعامل بصدق وشفافية مع كل عملائنا",
    about_v2: "الجودة", about_v2d: "نختار أفضل المنتجات من أفضل المصادر",
    about_v3: "خدمة العميل", about_v3d: "رضا العميل هو أولويتنا الأولى",
    about_v4: "التطوير", about_v4d: "نطوّر خدماتنا باستمرار لنكون الأفضل",
    about_stats_title: "أرقامنا بتتكلم",
    stat1: "+٥٠٠٠", stat1_label: "عميل سعيد", stat2: "+١٠٠٠", stat2_label: "منتج متوفر",
    stat3: "+٣", stat3_label: "سنوات خبرة", stat4: "٢٤/٧", stat4_label: "خدمة عملاء",
    // Contact page
    contact_page_title: "تواصل معنا", contact_page_sub: "نحب نسمع منك! تواصل معانا في أي وقت",
    contact_name: "الاسم الكامل", contact_email: "البريد الإلكتروني",
    contact_phone: "رقم الموبايل", contact_message: "الرسالة", contact_send: "إرسال الرسالة",
    contact_info_title: "معلومات التواصل",
    contact_hours: "ساعات العمل", contact_hours_val: "يومياً من ٨ صباحاً حتى ١٢ مساءً",
    contact_faq_title: "أسئلة شائعة",
    faq1_q: "ما هي مناطق التوصيل؟", faq1_a: "نوصل لجميع مناطق شبين القناطر والمناطق المجاورة.",
    faq2_q: "كم مدة التوصيل؟", faq2_a: "التوصيل خلال ساعة واحدة من تأكيد الطلب.",
    faq3_q: "هل يوجد حد أدنى للطلب؟", faq3_a: "الحد الأدنى للطلب ٥٠ جنيه. التوصيل مجاني للطلبات فوق ٢٠٠ جنيه.",
    // Cart page
    cart_title: "سلة التسوق", cart_empty: "سلتك فارغة!", cart_empty_sub: "ابدأ التسوق وأضف منتجات لسلتك",
    cart_continue: "تابع التسوق", cart_product: "المنتج", cart_price: "السعر",
    cart_qty: "الكمية", cart_total: "الإجمالي", cart_remove: "حذف",
    cart_subtotal: "المجموع الفرعي", cart_delivery: "التوصيل", cart_free: "مجاني",
    cart_grand_total: "الإجمالي الكلي", cart_checkout: "إتمام الشراء",
    cart_delivery_fee: "١٥ جنيه",
    // Checkout page
    chk_title: "إتمام الشراء", chk_order_summary: "ملخص الطلب",
    chk_payment_method: "طريقة الدفع", chk_visa: "بطاقة بنكية (Visa/Mastercard)",
    chk_fawry: "فوري", chk_wallet: "محفظة إلكترونية",
    chk_card_number: "رقم البطاقة", chk_card_name: "الاسم على البطاقة",
    chk_expiry: "تاريخ الانتهاء (MM/YY)", chk_cvv: "CVV",
    chk_fawry_ref: "رقم المرجع", chk_fawry_msg: "سيتم إنشاء كود فوري للدفع في أقرب منفذ",
    chk_wallet_phone: "رقم الموبايل المربوط بالمحفظة",
    chk_wallet_provider: "اختر المحفظة",
    chk_pay_now: "ادفع الآن", chk_secure: "🔒 دفع آمن ومشفّر",
    chk_success_title: "تم الطلب بنجاح! 🎉",
    chk_success_msg: "شكراً لطلبك. سيتم التوصيل خلال ساعة واحدة.",
    chk_back_home: "العودة للرئيسية",
    chk_address: "عنوان التوصيل", chk_phone: "رقم الموبايل", chk_notes: "ملاحظات (اختياري)"
  },
  en: {
    logo: "Awlad Rizk Market", nav_home: "Home", nav_categories: "Categories",
    nav_offers: "Offers", nav_about: "About", nav_contact: "Contact", lang_toggle: "عربي",
    hero_badge: "🚚 Free Delivery on Orders Over EGP 200",
    hero_title: "Everything Your Home Needs, One Place",
    hero_sub: "The widest selection of Egyptian products at the best prices, delivered to your door",
    hero_cta1: "Shop Now", hero_cta2: "Today's Offers",
    cat_title: "Shop by Category",
    cat_chips: "Chips & Snacks", cat_juice: "Juices & Drinks", cat_coffee: "Coffee & Tea",
    cat_dairy: "Dairy & Cheese", cat_biscuits: "Biscuits & Sweets", cat_canned: "Canned Food",
    cat_clean: "Cleaning", cat_pasta: "Pasta & Rice",
    offer1: "🔥 30% Off Chipsy Large Bags", offer2: "🧃 Juhayna Juice: Buy 3 Get 1 Free",
    offer3: "☕ Nescafé Offer: Large Pack EGP 85 Instead of 120",
    offer4: "🚚 Free Delivery on Orders Over EGP 200",
    prod_title: "Featured Products", sale: "Sale", add_cart: "Add to Cart",
    p1_name: "Chipsy Large - Cheese Flavor", p1_price: "EGP 25", p1_old: "EGP 35",
    p2_name: "Juhayna Classic Juice - Mango", p2_price: "EGP 18 / liter",
    p3_name: "Nescafé 3in1 - 30 Sachets", p3_price: "EGP 85", p3_old: "EGP 120",
    p4_name: "Domty White Cheese Triangles", p4_price: "EGP 42",
    p5_name: "El Arosa Tea - 200 Bags", p5_price: "EGP 95", p5_old: "EGP 125",
    p6_name: "Molto Croissant - 12 Pack", p6_price: "EGP 60",
    p7_name: "Chipsy - Ketchup Flavor", p7_price: "EGP 15",
    p8_name: "Beyti Juice - Guava", p8_price: "EGP 12",
    p9_name: "California Garden Foul", p9_price: "EGP 28", p9_old: "EGP 38",
    p10_name: "Gena Tuna", p10_price: "EGP 35",
    p11_name: "Regina Pasta - 500g", p11_price: "EGP 22",
    p12_name: "Persil Detergent - 4kg", p12_price: "EGP 155", p12_old: "EGP 190",
    why_title: "Why Awlad Rizk Market?",
    feat1_title: "Premium Quality", feat1_desc: "We source the best Egyptian products from trusted brands",
    feat2_title: "Fast Delivery", feat2_desc: "Delivery within one hour to all areas",
    feat3_title: "Wide Selection", feat3_desc: "Everything your home needs from authentic Egyptian brands",
    feat4_title: "Best Prices", feat4_desc: "Competitive prices with exclusive daily offers",
    test_title: "Customer Reviews",
    t1_quote: "\"The best market in the area. Great prices and super fast delivery!\"", t1_name: "Amina Mohamed",
    t2_quote: "\"Huge selection and nothing is missing. Been shopping here for a year!\"", t2_name: "Khaled Abdullah",
    t3_quote: "\"Daily offers save a lot, and customer service is excellent and very helpful!\"", t3_name: "Sara Ahmed",
    news_title: "Subscribe to Our Newsletter",
    news_sub: "Get the latest offers and discounts directly to your inbox",
    news_email: "Email Address", news_btn: "Subscribe Now",
    footer_tagline: "A premium market offering the widest selection of Egyptian products at the best prices",
    footer_links: "Quick Links", footer_contact: "Contact Us", footer_social: "Follow Us",
    footer_address: "Nawi Village, Shibin El Qanater Center, Qalyubia",
    footer_copy: "© 2026 Awlad Rizk Market. All rights reserved.",
    cat_page_title: "All Categories", cat_page_sub: "Browse our products by category",
    filter_all: "All", search_placeholder: "Search for a product...",
    offers_page_title: "Offers & Deals", offers_page_sub: "Save more with our daily offers",
    offer_valid: "Offer valid until end of the week", offer_terms: "Terms & Conditions",
    offer_terms_text: "Offers valid while stocks last. Cannot combine multiple offers. Free delivery for orders over EGP 200 only.",
    offer_d1: "30% off all Chipsy products", offer_d2: "Buy 3 Juhayna get 1 free",
    offer_d3: "Nescafé 3in1 EGP 85 instead of 120", offer_d4: "20% off all cleaning products",
    offer_d5: "El Arosa Tea 200 bags for EGP 95 only", offer_d6: "California Garden Foul 25% off",
    about_page_title: "About Us", about_page_sub: "Learn the story of Awlad Rizk Market",
    about_story_title: "Our Story", about_story_p1: "Awlad Rizk Market started as a small shop in Nawi Village, Shibin El Qanater, with a simple dream: to provide the community with everything they need at great prices and high quality.",
    about_story_p2: "Today, we've become one of the largest markets in the area, with a wide range of authentic Egyptian products and fast delivery service.",
    about_mission_title: "Our Mission", about_mission: "To provide every Egyptian household with authentic products at fair prices with the best shopping experience.",
    about_vision_title: "Our Vision", about_vision: "To be the #1 market in Qalyubia and the entire region.",
    about_values_title: "Our Values",
    about_v1: "Integrity", about_v1d: "We deal honestly and transparently with all our customers",
    about_v2: "Quality", about_v2d: "We select the best products from the best sources",
    about_v3: "Customer Service", about_v3d: "Customer satisfaction is our top priority",
    about_v4: "Innovation", about_v4d: "We continuously improve our services to be the best",
    about_stats_title: "Our Numbers Speak",
    stat1: "5000+", stat1_label: "Happy Customers", stat2: "1000+", stat2_label: "Products Available",
    stat3: "3+", stat3_label: "Years Experience", stat4: "24/7", stat4_label: "Customer Support",
    contact_page_title: "Contact Us", contact_page_sub: "We'd love to hear from you! Reach out anytime",
    contact_name: "Full Name", contact_email: "Email Address",
    contact_phone: "Phone Number", contact_message: "Message", contact_send: "Send Message",
    contact_info_title: "Contact Information",
    contact_hours: "Working Hours", contact_hours_val: "Daily from 8 AM to 12 AM",
    contact_faq_title: "FAQ",
    faq1_q: "What areas do you deliver to?", faq1_a: "We deliver to all areas in Shibin El Qanater and nearby regions.",
    faq2_q: "How long does delivery take?", faq2_a: "Delivery within one hour from order confirmation.",
    faq3_q: "Is there a minimum order?", faq3_a: "Minimum order is EGP 50. Free delivery for orders over EGP 200.",
    cart_title: "Shopping Cart", cart_empty: "Your cart is empty!", cart_empty_sub: "Start shopping and add products to your cart",
    cart_continue: "Continue Shopping", cart_product: "Product", cart_price: "Price",
    cart_qty: "Quantity", cart_total: "Total", cart_remove: "Remove",
    cart_subtotal: "Subtotal", cart_delivery: "Delivery", cart_free: "Free",
    cart_grand_total: "Grand Total", cart_checkout: "Checkout",
    cart_delivery_fee: "EGP 15",
    chk_title: "Checkout", chk_order_summary: "Order Summary",
    chk_payment_method: "Payment Method", chk_visa: "Credit/Debit Card (Visa/Mastercard)",
    chk_fawry: "Fawry", chk_wallet: "E-Wallet",
    chk_card_number: "Card Number", chk_card_name: "Name on Card",
    chk_expiry: "Expiry Date (MM/YY)", chk_cvv: "CVV",
    chk_fawry_ref: "Reference Number", chk_fawry_msg: "A Fawry payment code will be generated for the nearest outlet",
    chk_wallet_phone: "Mobile Number Linked to Wallet",
    chk_wallet_provider: "Select Wallet",
    chk_pay_now: "Pay Now", chk_secure: "\ud83d\udd12 Secure & Encrypted Payment",
    chk_success_title: "Order Placed Successfully! \ud83c\udf89",
    chk_success_msg: "Thank you for your order. Delivery within one hour.",
    chk_back_home: "Back to Home",
    chk_address: "Delivery Address", chk_phone: "Phone Number", chk_notes: "Notes (optional)"
  }
};

let currentLang = localStorage.getItem('fm_lang') || 'ar';
let cartCount = parseInt(localStorage.getItem('fm_cart') || '0');
let tickerMessages = [];
const CATEGORY_SLUG_MAP = {
  chips: ["chips", "chips & snacks", "شيبسي ومقرمشات"],
  juice: ["juice", "juices & drinks", "عصائر ومشروبات"],
  coffee: ["coffee", "coffee & tea", "قهوة وشاي"],
  dairy: ["dairy", "dairy & cheese", "ألبان وأجبان"],
  biscuits: ["biscuits", "biscuits & sweets", "بسكويت وحلويات"],
  canned: ["canned", "canned food", "معلبات"],
  clean: ["clean", "cleaning", "منظفات"],
  pasta: ["pasta", "pasta & rice", "معكرونة وأرز"]
};

function inferCategorySlug(product) {
  const raw = (
    product.categorySlug ||
    product.categoryNameEn ||
    product.categoryNameAr ||
    product.category?.nameEn ||
    product.category?.nameAr ||
    ""
  ).toString().toLowerCase().trim();
  for (const [slug, names] of Object.entries(CATEGORY_SLUG_MAP)) {
    if (names.some(n => raw.includes(n))) return slug;
  }
  return "chips";
}

function normalizeImageUrl(url) {
  if (!url) return "images/chipsy-cheese.png";
  if (url.startsWith("http://") || url.startsWith("https://")) return url;
  // Legacy seeded data uses /images/* which exists in frontend assets.
  if (url.startsWith("/images/")) return url.slice(1);
  // Uploaded admin files are served by API static files under /uploads/*.
  if (url.startsWith("/uploads/") && window.ApiClient) return window.ApiClient.getBaseUrl() + url;
  if (url.startsWith("/") && window.ApiClient) return window.ApiClient.getBaseUrl() + url;
  return url;
}

function renderStoreProductCard(product, index) {
  const name = currentLang === "ar" ? (product.nameAr || product.nameEn || "") : (product.nameEn || product.nameAr || "");
  const categoryAr = product.categoryNameAr || product.category?.nameAr || TRANSLATIONS.ar.cat_chips;
  const categoryEn = product.categoryNameEn || product.category?.nameEn || TRANSLATIONS.en.cat_chips;
  const category = currentLang === "ar" ? categoryAr : categoryEn;
  const categorySlug = inferCategorySlug(product);
  const stockQty = Number(product.stockQty ?? 0);
  const inStock = product.inStock !== false && stockQty > 0;
  const lowStock = inStock && stockQty <= 5;
  const lowStockText = currentLang === "ar" ? `كمية محدودة: ${stockQty}` : `Low stock: ${stockQty}`;
  const oldPriceHtml = product.oldPrice ? `<span class="product-card__old">${currentLang === "ar" ? Number(product.oldPrice).toLocaleString("ar-EG") + " جنيه" : "EGP " + Number(product.oldPrice).toLocaleString("en")}</span>` : "";
  const priceText = currentLang === "ar" ? Number(product.price).toLocaleString("ar-EG") + " جنيه" : "EGP " + Number(product.price).toLocaleString("en");
  return `<div class="product-card reveal-up hover-lift" data-category="${escapeHtmlAttr(categorySlug)}" style="--i:${index}">
    <div class="product-card__img-wrap">
      ${product.isOnSale ? `<div class="product-card__ribbon">${currentLang === "ar" ? "خصم" : "Sale"}</div>` : ""}
      ${lowStock ? `<div class="product-card__low-stock">${escapeHtmlText(lowStockText)}</div>` : ""}
      <img src="${escapeHtmlAttr(normalizeImageUrl(product.imageUrl))}" alt="${escapeHtmlAttr(name)}" loading="lazy">
    </div>
    <div class="product-card__body">
      <span class="product-card__tag">${escapeHtmlText(category)}</span>
      <h3 class="product-card__name">${escapeHtmlText(name)}</h3>
      <div class="product-card__price">${oldPriceHtml}<span>${escapeHtmlText(priceText)}</span></div>
      <button class="btn btn--cart add-cart-btn" data-product-id="${escapeHtmlAttr(product.id)}" data-stock-qty="${escapeHtmlAttr(stockQty)}" ${inStock ? "" : "disabled"}>${inStock ? (currentLang === "ar" ? "أضف للسلة" : "Add to Cart") : (currentLang === "ar" ? "نفذت الكمية" : "Out of stock")}</button>
    </div>
  </div>`;
}

function escapeHtmlText(v) {
  return String(v ?? "").replaceAll("&", "&amp;").replaceAll("<", "&lt;").replaceAll(">", "&gt;");
}

function escapeHtmlAttr(v) {
  return escapeHtmlText(v).replaceAll('"', "&quot;").replaceAll("'", "&#039;");
}

async function initStorefrontProducts() {
  if (!window.ApiClient) return;
  const featuredGrid = document.getElementById("featuredProductsGrid");
  const categoriesGrid = document.getElementById("productsGrid");
  if (!featuredGrid && !categoriesGrid) return;

  try {
    if (featuredGrid) {
      const featured = await window.ApiClient.request("/api/products/featured?count=6", { method: "GET" });
      if (Array.isArray(featured) && featured.length) {
        featuredGrid.innerHTML = featured.map((p, i) => renderStoreProductCard(p, i)).join("");
      }
    }
    if (categoriesGrid) {
      const payload = await window.ApiClient.request("/api/products?page=1&pageSize=200", { method: "GET" });
      const items = payload && Array.isArray(payload.items) ? payload.items : [];
      if (items.length) {
        categoriesGrid.innerHTML = items.map((p, i) => renderStoreProductCard(p, i)).join("");
      }
      initCategoriesFilters();
    }
    initCart();
    initObservers();
  } catch (_) {
    // fallback to static HTML if API unavailable
    initCategoriesFilters();
  }
}

function initCategoriesFilters() {
  const filterBar = document.getElementById("filterBar");
  const searchInput = document.getElementById("searchInput");
  if (!filterBar) return;

  let activeCategory = "all";
  function applyFilter() {
    const q = (searchInput && searchInput.value ? searchInput.value : "").toLowerCase().trim();
    document.querySelectorAll(".product-card[data-category]").forEach(card => {
      const byCat = activeCategory === "all" || card.dataset.category === activeCategory;
      const name = (card.querySelector(".product-card__name")?.textContent || "").toLowerCase();
      const bySearch = !q || name.includes(q);
      card.style.display = byCat && bySearch ? "" : "none";
    });
  }

  filterBar.querySelectorAll(".filter-btn").forEach(btn => {
    btn.addEventListener("click", () => {
      filterBar.querySelectorAll(".filter-btn").forEach(x => x.classList.remove("active"));
      btn.classList.add("active");
      activeCategory = btn.dataset.cat || "all";
      applyFilter();
    });
  });
  if (searchInput) searchInput.addEventListener("input", applyFilter);
  applyFilter();
}

function setLanguage(lang) {
  currentLang = lang;
  localStorage.setItem('fm_lang', lang);
  const html = document.documentElement;
  html.setAttribute('dir', lang === 'ar' ? 'rtl' : 'ltr');
  html.setAttribute('lang', lang);
  const t = TRANSLATIONS[lang];
  document.querySelectorAll('[data-i18n]').forEach(el => {
    const key = el.getAttribute('data-i18n');
    if (t[key] !== undefined) {
      if (el.tagName === 'INPUT' || el.tagName === 'TEXTAREA') el.placeholder = t[key];
      else if (el.tagName === 'LABEL') el.textContent = t[key];
      else el.textContent = t[key];
    }
  });
  renderTicker();
  // Re-render API-driven content in selected language.
  initStorefrontProducts();
  initOffersFromApi();
}

function updateCartBadge() {
  const badge = document.getElementById('cartBadge');
  if (badge) { badge.textContent = cartCount; }
}

function initObservers() {
  const els = document.querySelectorAll('.reveal-up, .reveal-left, .reveal-right, .section__bar');
  const obs = new IntersectionObserver(entries => {
    entries.forEach(e => { if (e.isIntersecting) e.target.classList.add('is-visible'); });
  }, { threshold: 0.15 });
  els.forEach(el => obs.observe(el));
}

function initNavbar() {
  const navbar = document.getElementById('navbar');
  if (!navbar) return;
  let ticking = false;
  window.addEventListener('scroll', () => {
    if (!ticking) {
      requestAnimationFrame(() => { navbar.classList.toggle('scrolled', window.scrollY > 60); ticking = false; });
      ticking = true;
    }
  });
}

function initCart() {
  updateCartBadge();
  document.querySelectorAll('.add-cart-btn').forEach(btn => {
    btn.addEventListener('click', () => {
      if (btn.disabled) return;
      const orig = btn.textContent;
      btn.classList.add('added'); btn.textContent = '\u2713';
      setTimeout(() => { btn.classList.remove('added'); btn.textContent = orig; }, 1500);

      // Detect product ID from the card
      const card = btn.closest('.product-card');
      let productId = btn.getAttribute("data-product-id") || 'p1';
      if (!btn.getAttribute("data-product-id") && card) {
        const nameEl = card.querySelector('[data-i18n^="p"][data-i18n$="_name"]');
        if (nameEl) productId = nameEl.getAttribute('data-i18n').replace('_name', '');
      }

      // Add to cart items in localStorage
      const items = JSON.parse(localStorage.getItem('fm_cart_items') || '[]');
      const existing = items.find(i => i.id === productId);
      const maxQty = Number(btn.getAttribute("data-stock-qty") || 0);
      const currentQty = existing ? Number(existing.qty || 0) : 0;
      if (maxQty > 0 && currentQty >= maxQty) {
        const msg = currentLang === "ar" ? "الكمية المتاحة غير كافية في المخزون." : "No more stock available for this product.";
        alert(msg);
        return;
      }
      if (existing) { existing.qty++; } else { items.push({ id: productId, qty: 1 }); }
      localStorage.setItem('fm_cart_items', JSON.stringify(items));

      cartCount = items.reduce((s, i) => s + i.qty, 0);
      localStorage.setItem('fm_cart', cartCount);
      updateCartBadge();
      const badge = document.getElementById('cartBadge');
      if (badge) { badge.classList.add('bump'); setTimeout(() => badge.classList.remove('bump'), 300); }
    });
  });
}

let currentSlide = 0, autoSlideTimer = null;
function initTestimonials() {
  const cards = document.querySelectorAll('.test-card');
  const dots = document.querySelectorAll('.dot');
  if (!cards.length) return;
  function show(i) { currentSlide = i; cards.forEach((c,j) => c.classList.toggle('active-slide',j===i)); dots.forEach((d,j) => d.classList.toggle('active',j===i)); }
  dots.forEach(d => d.addEventListener('click', () => { show(parseInt(d.dataset.index)); clearInterval(autoSlideTimer); autoSlideTimer = setInterval(() => show((currentSlide+1)%cards.length), 4000); }));
  show(0); autoSlideTimer = setInterval(() => show((currentSlide+1)%cards.length), 4000);
}

function initMobileMenu() {
  const h = document.getElementById('hamburger'), d = document.getElementById('mobileDrawer'),
        o = document.getElementById('drawerOverlay'), c = document.getElementById('drawerClose');
  if (!h || !d) return;
  const open = () => { d.classList.add('open'); document.body.style.overflow = 'hidden'; };
  const close = () => { d.classList.remove('open'); document.body.style.overflow = ''; };
  h.addEventListener('click', open); o.addEventListener('click', close); c.addEventListener('click', close);
  d.querySelectorAll('a').forEach(a => a.addEventListener('click', close));
}

function initNewsletter() {
  const form = document.getElementById('newsForm');
  if (!form) return;
  const btn = form.querySelector('.btn--ripple');
  form.addEventListener('submit', async e => {
    e.preventDefault(); btn.classList.add('rippling');
    setTimeout(() => btn.classList.remove('rippling'), 600);
    const input = document.getElementById('emailInput');
    if (!input || !input.value) return;

    const email = input.value.trim();
    if (!email) return;

    try {
      if (window.ApiClient) {
        await window.ApiClient.subscribeNewsletter(email);
      }
      input.value = '';
      btn.textContent = '✓';
      setTimeout(() => { btn.textContent = TRANSLATIONS[currentLang].news_btn; }, 2000);
    } catch (err) {
      const msg = currentLang === 'ar'
        ? 'تعذر الاشتراك الآن. حاول مرة أخرى لاحقاً.'
        : 'Unable to subscribe right now. Please try again later.';
      alert(msg);
      btn.textContent = TRANSLATIONS[currentLang].news_btn;
    }
  });
}

function initLangToggle() {
  const toggle = document.getElementById('langToggle');
  if (toggle) toggle.addEventListener('click', () => setLanguage(currentLang === 'ar' ? 'en' : 'ar'));
}

function initDarkMode() {
  const isDark = localStorage.getItem('fm_dark') === 'true';
  if (isDark) document.documentElement.classList.add('dark');
  const toggle = document.getElementById('darkToggle');
  if (toggle) {
    toggle.textContent = isDark ? '☀️' : '🌙';
    toggle.addEventListener('click', () => {
      const nowDark = document.documentElement.classList.toggle('dark');
      localStorage.setItem('fm_dark', nowDark);
      toggle.textContent = nowDark ? '☀️' : '🌙';
    });
  }
}

function renderTicker() {
  const track = document.getElementById('tickerTrack');
  if (!track || !tickerMessages.length) return;

  const lines = [];
  tickerMessages.forEach(msg => {
    const text = currentLang === 'ar' ? msg.textAr : msg.textEn;
    if (!text) return;
    lines.push(`<span>${text}</span>`);
    lines.push('<span class="ticker__sep">✦</span>');
  });

  track.innerHTML = lines.join('');
}

async function initTickerFromApi() {
  if (!window.ApiClient || !document.getElementById('tickerTrack')) return;
  try {
    const items = await window.ApiClient.request('/api/ticker', { method: 'GET' });
    if (Array.isArray(items) && items.length) {
      tickerMessages = items;
      renderTicker();
    }
  } catch (_) {
    // Keep static HTML ticker as fallback if API is unavailable.
  }
}

async function initOffersFromApi() {
  if (!window.ApiClient) return;
  const grid = document.querySelector(".offers-grid");
  if (!grid) return;
  try {
    const offers = await window.ApiClient.request("/api/offers", { method: "GET" });
    if (!Array.isArray(offers) || !offers.length) return;
    grid.innerHTML = offers
      .filter(o => o.isActive !== false)
      .map((o, i) => {
        const title = currentLang === "ar" ? (o.titleAr || o.titleEn || "") : (o.titleEn || o.titleAr || "");
        const desc = currentLang === "ar" ? "العرض ساري حتى نهاية الأسبوع" : "Offer valid until end of week";
        const badge = o.badgeText || `-${o.discountPercent || 0}%`;
        const icon = o.icon || "🔥";
        return `<div class="offer-card reveal-up" style="--i:${i}">
          <span class="offer-card__badge">${escapeHtmlText(badge)}</span>
          <div class="offer-card__icon">${escapeHtmlText(icon)}</div>
          <h3>${escapeHtmlText(title)}</h3>
          <p>${escapeHtmlText(desc)}</p>
        </div>`;
      }).join("");
    initObservers();
  } catch (_) {
    // keep static offers if API unavailable
  }
}

document.addEventListener('DOMContentLoaded', () => {
  setLanguage(currentLang);
  initObservers(); initNavbar(); initCart(); initTestimonials();
  initMobileMenu(); initNewsletter(); initLangToggle(); initDarkMode(); updateCartBadge();
  initTickerFromApi();
  initStorefrontProducts();
  initOffersFromApi();
});
