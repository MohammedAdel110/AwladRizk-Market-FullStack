function ensureAdminAuth() {
  if (!window.ApiClient || !window.ApiClient.getAdminToken()) {
    window.location.href = "admin-login.html";
    return false;
  }
  return true;
}

function $(id) {
  return document.getElementById(id);
}

function escapeHtml(str) {
  return String(str ?? "")
    .replaceAll("&", "&amp;")
    .replaceAll("<", "&lt;")
    .replaceAll(">", "&gt;")
    .replaceAll('"', "&quot;")
    .replaceAll("'", "&#039;");
}

function money(v) {
  const n = Number(v ?? 0);
  return Number.isFinite(n) ? n.toFixed(2) : "0.00";
}

function showToast(message, kind) {
  const el = $("adminToast");
  if (!el) return;
  el.textContent = message;
  el.classList.add("show");
  el.dataset.kind = kind || "info";
  window.clearTimeout(showToast._t);
  showToast._t = window.setTimeout(() => el.classList.remove("show"), 2600);
}

function ensureRealtimeToastStack() {
  let stack = document.getElementById("rtToastStack");
  if (stack) return stack;
  stack = document.createElement("div");
  stack.id = "rtToastStack";
  stack.className = "rt-toast-stack";
  document.body.appendChild(stack);
  return stack;
}

function showRealtimeOrderToast(payload) {
  const stack = ensureRealtimeToastStack();
  const el = document.createElement("div");
  el.className = "rt-toast";
  const orderId = payload && payload.orderId != null ? payload.orderId : "—";
  const name = payload && payload.customerName ? payload.customerName : "Customer";
  const total = payload && payload.totalAmount != null ? Number(payload.totalAmount) : 0;
  el.innerHTML = `
    <div class="rt-toast__icon">🧾</div>
    <div class="rt-toast__body">
      <div class="rt-toast__title">New Order #${escapeHtml(orderId)}</div>
      <div class="rt-toast__meta">
        <span>${escapeHtml(name)}</span>
        <span class="rt-dot">•</span>
        <span>EGP ${escapeHtml(money(total))}</span>
      </div>
    </div>
    <button class="rt-toast__close" type="button" aria-label="Close">×</button>
  `;
  stack.appendChild(el);

  const close = () => {
    el.classList.add("out");
    window.setTimeout(() => el.remove(), 220);
  };
  el.querySelector(".rt-toast__close").addEventListener("click", close);
  window.setTimeout(close, 6500);
}

function playOrderAlertSound() {
  try {
    const audio = playOrderAlertSound._a || (playOrderAlertSound._a = new Audio("assets/audio/alert.mp3"));
    audio.currentTime = 0;
    const p = audio.play();
    if (p && typeof p.catch === "function") p.catch(() => {});
  } catch {
    // ignore
  }
}

async function initOrderRealtimeNotifications() {
  if (!window.signalR || !window.ApiClient) return;
  const token = window.ApiClient.getAdminToken();
  if (!token) return;

  const base = window.ApiClient.getBaseUrl ? window.ApiClient.getBaseUrl() : "";
  const hubUrl = (base ? base.replace(/\/+$/, "") : "") + "/orderHub";

  const connection = new window.signalR.HubConnectionBuilder()
    .withUrl(hubUrl, { accessTokenFactory: () => token })
    .withAutomaticReconnect()
    .build();

  connection.on("ReceiveNewOrder", (payload) => {
    playOrderAlertSound();
    showRealtimeOrderToast(payload || {});
    // Optional: keep admin lists fresh
    // refreshAllData().catch(() => {});
  });

  try {
    await connection.start();
  } catch (err) {
    // keep the dashboard usable even if realtime fails
    showToast("Realtime connection failed.", "error");
  }
}

const EditState = {
  type: null, // "product" | "offer" | "ticker"
  id: null,
  original: null,
  current: null
};

const ListState = {
  products: { items: [], page: 1, pageSize: 8 },
  offers: { items: [], page: 1, pageSize: 6 },
  ticker: { items: [], page: 1, pageSize: 8 }
};

function openModal() {
  const m = $("editModal");
  if (!m) return;
  m.classList.add("open");
  m.setAttribute("aria-hidden", "false");
}

function closeModal() {
  const m = $("editModal");
  if (!m) return;
  m.classList.remove("open");
  m.setAttribute("aria-hidden", "true");
  EditState.type = null;
  EditState.id = null;
  EditState.original = null;
  EditState.current = null;
  const fields = $("editFields");
  if (fields) fields.innerHTML = "";
  const changes = $("editChanges");
  if (changes) changes.innerHTML = "";
  const uploadBtn = $("editUploadBtn");
  if (uploadBtn) uploadBtn.style.display = "none";
  const editOfferPreviewWrap = $("editOfferPreviewWrap");
  if (editOfferPreviewWrap) editOfferPreviewWrap.style.display = "none";
}

function renderPager(type, totalItems, pagerId, onChange) {
  const pager = $(pagerId);
  if (!pager) return;
  const state = ListState[type];
  const totalPages = Math.max(1, Math.ceil(totalItems / state.pageSize));
  if (state.page > totalPages) state.page = totalPages;
  if (totalPages <= 1) {
    pager.innerHTML = "";
    return;
  }
  const pageButtons = [];
  for (let i = 1; i <= totalPages; i++) {
    pageButtons.push(`<button type="button" data-page="${i}" class="${i === state.page ? "active" : ""}">${i}</button>`);
  }
  pager.innerHTML = `<button type="button" data-nav="prev" ${state.page <= 1 ? "disabled" : ""}>Prev</button>${pageButtons.join("")}<button type="button" data-nav="next" ${state.page >= totalPages ? "disabled" : ""}>Next</button>`;
  pager.onclick = (e) => {
    const btn = e.target.closest("button");
    if (!btn) return;
    if (btn.dataset.page) state.page = Number(btn.dataset.page);
    if (btn.dataset.nav === "prev" && state.page > 1) state.page--;
    if (btn.dataset.nav === "next" && state.page < totalPages) state.page++;
    onChange();
  };
}

function renderOfferPreview(prefix, data) {
  const title = data.titleEn || data.titleAr || "Offer title";
  const badge = data.badgeText || `-${Number(data.discountPercent || 0)}%`;
  const icon = data.icon || "🔥";
  const parsed = data.validUntil ? new Date(data.validUntil) : null;
  const validUntilText = parsed && !Number.isNaN(parsed.getTime()) ? parsed.toLocaleDateString() : "Offer valid until end of week";
  const titleEl = $(`${prefix}OfferPreviewTitle`);
  const badgeEl = $(`${prefix}OfferPreviewBadge`);
  const iconEl = $(`${prefix}OfferPreviewIcon`);
  const descEl = $(`${prefix}OfferPreviewDesc`);
  if (titleEl) titleEl.textContent = title;
  if (badgeEl) badgeEl.textContent = badge;
  if (iconEl) iconEl.textContent = icon;
  if (descEl) descEl.textContent = validUntilText;
}

function renderProductPreview(prefix, data) {
  const name = data.nameEn || data.nameAr || "Product name";
  const priceN = Number(data.price || 0);
  const oldPriceN = data.oldPrice == null || data.oldPrice === "" ? null : Number(data.oldPrice);
  const priceText = `EGP ${money(priceN)}`;
  const oldText = oldPriceN != null && Number.isFinite(oldPriceN) ? `EGP ${money(oldPriceN)}` : "";
  const imageUrl = (data.imageUrl || "").trim() || "images/chipsy-cheese.png";
  const categoryLabel = data.categoryLabel || "Category";
  const isOnSale = !!data.isOnSale;

  const nameEl = $(`${prefix}ProductPreviewName`);
  const priceEl = $(`${prefix}ProductPreviewPrice`);
  const oldEl = $(`${prefix}ProductPreviewOld`);
  const imgEl = $(`${prefix}ProductPreviewImg`);
  const catEl = $(`${prefix}ProductPreviewCategory`);
  const ribbonEl = $(`${prefix}ProductPreviewRibbon`);

  if (nameEl) nameEl.textContent = name;
  if (priceEl) priceEl.textContent = priceText;
  if (oldEl) {
    oldEl.textContent = oldText || "EGP 0";
    oldEl.style.display = oldText ? "" : "none";
  }
  if (imgEl) {
    // Avoid infinite loop: only fallback once.
    imgEl.onerror = () => {
      if (imgEl.dataset.fallbackApplied === "1") return;
      imgEl.dataset.fallbackApplied = "1";
      imgEl.src = "images/chipsy-cheese.png";
    };
    imgEl.dataset.fallbackApplied = "0";
    imgEl.src = imageUrl;
  }
  if (catEl) catEl.textContent = categoryLabel;
  if (ribbonEl) ribbonEl.style.display = isOnSale ? "" : "none";
}

function toIsoIfValid(v) {
  if (!v) return null;
  const d = new Date(v);
  return Number.isNaN(d.getTime()) ? null : d.toISOString();
}

function renderChanges(diff) {
  const ul = $("editChanges");
  if (!ul) return;
  if (!diff.length) {
    ul.innerHTML = `<li><div class="k">No changes</div><div class="v">Start editing fields to see what will be modified.</div></li>`;
    return;
  }
  ul.innerHTML = diff.map(d => `<li><div class="k">${escapeHtml(d.key)}</div><div class="v">${escapeHtml(d.from)} → ${escapeHtml(d.to)}</div></li>`).join("");
}

function diffObjects(original, current, keys) {
  const diffs = [];
  for (const k of keys) {
    const a = original && original[k] != null ? String(original[k]) : "";
    const b = current && current[k] != null ? String(current[k]) : "";
    if (a !== b) diffs.push({ key: k, from: a || "—", to: b || "—" });
  }
  return diffs;
}

function getEditCurrentFromForm() {
  const f = $("editForm");
  if (!f) return null;
  const type = EditState.type;
  if (type === "product") {
    return {
      nameAr: f.nameAr.value,
      nameEn: f.nameEn.value,
      price: Number(f.price.value),
      oldPrice: f.oldPrice.value ? Number(f.oldPrice.value) : null,
      imageUrl: f.imageUrl.value,
      stockQty: Number(f.stockQty.value),
      categoryId: Number(f.categoryId.value),
      isOnSale: !!f.isOnSale.checked
    };
  }
  if (type === "offer") {
    return {
      titleAr: f.titleAr.value,
      titleEn: f.titleEn.value,
      descriptionAr: f.descriptionAr.value,
      descriptionEn: f.descriptionEn.value,
      discountPercent: Number(f.discountPercent.value),
      badgeText: f.badgeText.value,
      icon: f.icon.value,
      validUntil: toIsoIfValid(f.validUntil.value),
      isActive: !!f.isActive.checked
    };
  }
  if (type === "ticker") {
    return {
      textAr: f.textAr.value,
      textEn: f.textEn.value,
      sortOrder: Number(f.sortOrder.value),
      isActive: !!f.isActive.checked
    };
  }
  return null;
}

function wireEditFormDiff(keys) {
  const form = $("editForm");
  if (!form) return;
  const handler = () => {
    EditState.current = getEditCurrentFromForm();
    const diff = diffObjects(EditState.original, EditState.current, keys);
    renderChanges(diff);
    if (EditState.type === "offer") {
      renderOfferPreview("edit", EditState.current || {});
    } else if (EditState.type === "product") {
      const catSelect = $("editCategoryId");
      const categoryLabel = catSelect && catSelect.selectedOptions && catSelect.selectedOptions[0]
        ? catSelect.selectedOptions[0].textContent
        : "Category";
      renderProductPreview("edit", { ...(EditState.current || {}), categoryLabel });
    }
  };
  form.oninput = handler;
  form.onchange = handler;
  handler();
}

function setEditFields(html) {
  const fields = $("editFields");
  if (fields) fields.innerHTML = html;
}

async function openEditProduct(id) {
  const editOfferPreviewWrap = $("editOfferPreviewWrap");
  if (editOfferPreviewWrap) editOfferPreviewWrap.style.display = "none";
  const editProductPreviewWrap = $("editProductPreviewWrap");
  if (editProductPreviewWrap) editProductPreviewWrap.style.display = "";
  await loadCategoryOptions();
  const data = await window.ApiClient.request("/api/products/" + id);
  EditState.type = "product";
  EditState.id = id;
  EditState.original = {
    nameAr: data.nameAr ?? "",
    nameEn: data.nameEn ?? "",
    price: Number(data.price ?? 0),
    oldPrice: data.oldPrice ?? null,
    imageUrl: data.imageUrl ?? "",
    stockQty: Number(data.stockQty ?? 0),
    categoryId: Number(data.categoryId ?? 0),
    isOnSale: !!data.isOnSale
  };

  $("editModalKicker").textContent = "Edit product";
  $("editModalTitle").textContent = `#${id}`;

  setEditFields(`
    <div class="admin-field"><label>Arabic name</label><input name="nameAr" required></div>
    <div class="admin-field"><label>English name</label><input name="nameEn" required></div>
    <div class="admin-field"><label>Price (EGP)</label><input name="price" type="number" step="0.01" required></div>
    <div class="admin-field"><label>Old price</label><input name="oldPrice" type="number" step="0.01"></div>
    <div class="admin-field admin-field--wide">
      <label>Image</label>
      <input name="imageUrl" placeholder="/uploads/..." required>
      <div class="admin-file" style="margin-top:10px">
        <input id="editImageFile" type="file" accept="image/*">
      </div>
    </div>
    <div class="admin-field"><label>Stock quantity</label><input name="stockQty" type="number" required></div>
    <div class="admin-field"><label>Category</label><select name="categoryId" id="editCategoryId" required></select></div>
    <div class="admin-field admin-field--inline"><label class="admin-check"><input type="checkbox" name="isOnSale"><span>On sale</span></label></div>
  `);

  const mainCat = $("categoryId");
  const editCat = $("editCategoryId");
  if (mainCat && editCat) editCat.innerHTML = mainCat.innerHTML;

  const f = $("editForm");
  f.nameAr.value = EditState.original.nameAr;
  f.nameEn.value = EditState.original.nameEn;
  f.price.value = String(EditState.original.price);
  f.oldPrice.value = EditState.original.oldPrice == null ? "" : String(EditState.original.oldPrice);
  f.imageUrl.value = EditState.original.imageUrl;
  f.stockQty.value = String(EditState.original.stockQty);
  f.categoryId.value = String(EditState.original.categoryId || "");
  f.isOnSale.checked = !!EditState.original.isOnSale;
  const catLabel = editCat && editCat.selectedOptions && editCat.selectedOptions[0]
    ? editCat.selectedOptions[0].textContent
    : "Category";
  renderProductPreview("edit", { ...EditState.original, categoryLabel: catLabel });

  const uploadBtn = $("editUploadBtn");
  if (uploadBtn) {
    uploadBtn.style.display = "inline-flex";
    uploadBtn.onclick = async () => {
      const fileInput = $("editImageFile");
      const file = fileInput && fileInput.files && fileInput.files[0] ? fileInput.files[0] : null;
      if (!file) return showToast("Choose an image file first.", "error");
      const res = await window.ApiClient.adminUploadImage(file);
      f.imageUrl.value = res.url;
      showToast("Image uploaded.", "ok");
      f.dispatchEvent(new Event("input", { bubbles: true }));
    };
  }

  $("editResetBtn").onclick = () => {
    f.nameAr.value = EditState.original.nameAr;
    f.nameEn.value = EditState.original.nameEn;
    f.price.value = String(EditState.original.price);
    f.oldPrice.value = EditState.original.oldPrice == null ? "" : String(EditState.original.oldPrice);
    f.imageUrl.value = EditState.original.imageUrl;
    f.stockQty.value = String(EditState.original.stockQty);
    f.categoryId.value = String(EditState.original.categoryId || "");
    f.isOnSale.checked = !!EditState.original.isOnSale;
    f.dispatchEvent(new Event("input", { bubbles: true }));
  };

  wireEditFormDiff(["nameAr","nameEn","price","oldPrice","imageUrl","stockQty","categoryId","isOnSale"]);
  openModal();
}

async function openEditOffer(id) {
  const offers = await window.ApiClient.request("/api/offers");
  const current = (offers || []).find(o => o.id === id);
  if (!current) return showToast("Offer not found.", "error");

  EditState.type = "offer";
  EditState.id = id;
  EditState.original = {
    titleAr: current.titleAr ?? "",
    titleEn: current.titleEn ?? "",
    descriptionAr: current.descriptionAr ?? "",
    descriptionEn: current.descriptionEn ?? "",
    discountPercent: Number(current.discountPercent ?? 0),
    badgeText: current.badgeText ?? "",
    icon: current.icon ?? "",
    validUntil: current.validUntil ?? null,
    isActive: !!current.isActive
  };

  $("editModalKicker").textContent = "Edit offer";
  $("editModalTitle").textContent = `#${id}`;

  setEditFields(`
    <div class="admin-field"><label>Arabic title</label><input name="titleAr" required></div>
    <div class="admin-field"><label>English title</label><input name="titleEn" required></div>
    <div class="admin-field admin-field--wide"><label>Arabic description</label><input name="descriptionAr" required></div>
    <div class="admin-field admin-field--wide"><label>English description</label><input name="descriptionEn" required></div>
    <div class="admin-field"><label>Discount %</label><input name="discountPercent" type="number" required></div>
    <div class="admin-field"><label>Badge text</label><input name="badgeText" required></div>
    <div class="admin-field"><label>Icon</label><input name="icon" required></div>
    <div class="admin-field"><label>Valid until</label><input name="validUntil" type="datetime-local" required></div>
    <div class="admin-field admin-field--inline"><label class="admin-check"><input type="checkbox" name="isActive"><span>Active</span></label></div>
  `);

  const f = $("editForm");
  f.titleAr.value = EditState.original.titleAr;
  f.titleEn.value = EditState.original.titleEn;
  f.descriptionAr.value = EditState.original.descriptionAr;
  f.descriptionEn.value = EditState.original.descriptionEn;
  f.discountPercent.value = String(EditState.original.discountPercent);
  f.badgeText.value = EditState.original.badgeText;
  f.icon.value = EditState.original.icon;
  const d = EditState.original.validUntil ? new Date(EditState.original.validUntil) : new Date();
  f.validUntil.value = new Date(d.getTime() - d.getTimezoneOffset() * 60000).toISOString().slice(0, 16);
  f.isActive.checked = !!EditState.original.isActive;

  const uploadBtn = $("editUploadBtn");
  if (uploadBtn) uploadBtn.style.display = "none";
  const editOfferPreviewWrap = $("editOfferPreviewWrap");
  if (editOfferPreviewWrap) editOfferPreviewWrap.style.display = "";
  const editProductPreviewWrap = $("editProductPreviewWrap");
  if (editProductPreviewWrap) editProductPreviewWrap.style.display = "none";
  renderOfferPreview("edit", {
    titleAr: EditState.original.titleAr,
    titleEn: EditState.original.titleEn,
    badgeText: EditState.original.badgeText,
    discountPercent: EditState.original.discountPercent,
    icon: EditState.original.icon,
    validUntil: EditState.original.validUntil
  });

  $("editResetBtn").onclick = () => {
    f.titleAr.value = EditState.original.titleAr;
    f.titleEn.value = EditState.original.titleEn;
    f.descriptionAr.value = EditState.original.descriptionAr;
    f.descriptionEn.value = EditState.original.descriptionEn;
    f.discountPercent.value = String(EditState.original.discountPercent);
    f.badgeText.value = EditState.original.badgeText;
    f.icon.value = EditState.original.icon;
    const dd = EditState.original.validUntil ? new Date(EditState.original.validUntil) : new Date();
    f.validUntil.value = new Date(dd.getTime() - dd.getTimezoneOffset() * 60000).toISOString().slice(0, 16);
    f.isActive.checked = !!EditState.original.isActive;
    f.dispatchEvent(new Event("input", { bubbles: true }));
  };

  wireEditFormDiff(["titleAr","titleEn","descriptionAr","descriptionEn","discountPercent","badgeText","icon","validUntil","isActive"]);
  openModal();
}

async function openEditTicker(id) {
  const editOfferPreviewWrap = $("editOfferPreviewWrap");
  if (editOfferPreviewWrap) editOfferPreviewWrap.style.display = "none";
  const editProductPreviewWrap = $("editProductPreviewWrap");
  if (editProductPreviewWrap) editProductPreviewWrap.style.display = "none";
  const items = await window.ApiClient.adminGetTickerMessages();
  const current = (items || []).find(t => t.id === id);
  if (!current) return showToast("Ticker message not found.", "error");

  EditState.type = "ticker";
  EditState.id = id;
  EditState.original = {
    textAr: current.textAr ?? "",
    textEn: current.textEn ?? "",
    sortOrder: Number(current.sortOrder ?? 0),
    isActive: !!current.isActive
  };

  $("editModalKicker").textContent = "Edit ticker message";
  $("editModalTitle").textContent = `#${id}`;

  setEditFields(`
    <div class="admin-field admin-field--wide"><label>Arabic text</label><input name="textAr" required></div>
    <div class="admin-field admin-field--wide"><label>English text</label><input name="textEn" required></div>
    <div class="admin-field"><label>Sort order</label><input name="sortOrder" type="number" required></div>
    <div class="admin-field admin-field--inline"><label class="admin-check"><input type="checkbox" name="isActive"><span>Active</span></label></div>
  `);

  const f = $("editForm");
  f.textAr.value = EditState.original.textAr;
  f.textEn.value = EditState.original.textEn;
  f.sortOrder.value = String(EditState.original.sortOrder);
  f.isActive.checked = !!EditState.original.isActive;

  const uploadBtn = $("editUploadBtn");
  if (uploadBtn) uploadBtn.style.display = "none";

  $("editResetBtn").onclick = () => {
    f.textAr.value = EditState.original.textAr;
    f.textEn.value = EditState.original.textEn;
    f.sortOrder.value = String(EditState.original.sortOrder);
    f.isActive.checked = !!EditState.original.isActive;
    f.dispatchEvent(new Event("input", { bubbles: true }));
  };

  wireEditFormDiff(["textAr","textEn","sortOrder","isActive"]);
  openModal();
}

function setActiveView(viewKey) {
  const nav = $("adminNav");
  if (nav) {
    nav.querySelectorAll(".admin-nav__item").forEach(btn => {
      btn.classList.toggle("is-active", btn.dataset.view === viewKey);
    });
  }

  document.querySelectorAll(".admin-view").forEach(v => {
    const isTarget = v.dataset.view === viewKey;
    v.classList.toggle("is-active", isTarget);
    if (isTarget) {
      v.classList.remove("is-entering");
      // retrigger animation
      void v.offsetWidth;
      v.classList.add("is-entering");
    }
  });

  const subtitle = $("adminSubtitle");
  if (subtitle) {
    subtitle.textContent =
      viewKey === "products" ? "Manage products, categories, and inventory." :
      viewKey === "offers" ? "Create and maintain promotional offers." :
      "Control the scrolling home ticker content.";
  }
}

async function loadProducts() {
  await loadCategoryOptions();
  const data = await window.ApiClient.request("/api/products");
  const items = data.items || [];
  ListState.products.items = items;
  const list = document.getElementById("productsList");

  const q = ($("productsSearch") && $("productsSearch").value || "").trim().toLowerCase();
  const filtered = q
    ? items.filter(p => String(p.nameEn || "").toLowerCase().includes(q) || String(p.nameAr || "").toLowerCase().includes(q) || String(p.id).includes(q))
    : items;

  const state = ListState.products;
  const total = filtered.length;
  const start = (state.page - 1) * state.pageSize;
  const paged = filtered.slice(start, start + state.pageSize);
  list.innerHTML = paged.map(p => {
    const status = p.isOnSale ? `<span class="admin-badge admin-badge--warn">On sale</span>` : `<span class="admin-badge admin-badge--ok">Active</span>`;
    return `<tr>
      <td>#${escapeHtml(p.id)}</td>
      <td>
        <div style="font-weight:900">${escapeHtml(p.nameEn || "")}</div>
        <div style="color:var(--text-muted);font-size:.85rem">${escapeHtml(p.nameAr || "")}</div>
      </td>
      <td>EGP ${escapeHtml(money(p.price))}</td>
      <td>${escapeHtml(p.stockQty ?? 0)}</td>
      <td>${status}</td>
      <td>
        <div class="admin-actions">
          <button class="admin-btn admin-btn--ghost" type="button" data-action="editProduct" data-id="${escapeHtml(p.id)}">Edit</button>
          <button class="admin-btn admin-btn--danger" type="button" data-action="deleteProduct" data-id="${escapeHtml(p.id)}">Delete</button>
        </div>
      </td>
    </tr>`;
  }).join("");

  const meta = $("metaProducts");
  if (meta) meta.textContent = String(items.length);
  renderPager("products", total, "productsPager", loadProducts);
}

async function loadOffers() {
  const items = await window.ApiClient.request("/api/offers");
  ListState.offers.items = items;
  const list = document.getElementById("offersList");
  const state = ListState.offers;
  const start = (state.page - 1) * state.pageSize;
  const paged = items.slice(start, start + state.pageSize);
  list.innerHTML = paged.map(o => {
    const active = o.isActive ? `<span class="admin-badge admin-badge--ok">Active</span>` : `<span class="admin-badge admin-badge--off">Disabled</span>`;
    const until = o.validUntil ? new Date(o.validUntil).toLocaleString() : "—";
    return `<tr>
      <td>#${escapeHtml(o.id)}</td>
      <td>
        <div style="font-weight:900">${escapeHtml(o.titleEn || "")}</div>
        <div style="color:var(--text-muted);font-size:.85rem">${escapeHtml(o.titleAr || "")}</div>
      </td>
      <td>${escapeHtml(o.discountPercent ?? 0)}%</td>
      <td>${escapeHtml(until)}</td>
      <td>${active}</td>
      <td>
        <div class="admin-actions">
          <button class="admin-btn admin-btn--ghost" type="button" data-action="editOffer" data-id="${escapeHtml(o.id)}">Edit</button>
          <button class="admin-btn admin-btn--danger" type="button" data-action="deleteOffer" data-id="${escapeHtml(o.id)}">Delete</button>
        </div>
      </td>
    </tr>`;
  }).join("");

  const meta = $("metaOffers");
  if (meta) meta.textContent = String(items.length);
  renderPager("offers", items.length, "offersPager", loadOffers);
}

async function loadTicker() {
  const items = await window.ApiClient.adminGetTickerMessages();
  ListState.ticker.items = items;
  const list = document.getElementById("tickerList");
  const state = ListState.ticker;
  const start = (state.page - 1) * state.pageSize;
  const paged = items.slice(start, start + state.pageSize);
  list.innerHTML = paged.map(t => {
    const active = t.isActive ? `<span class="admin-badge admin-badge--ok">Active</span>` : `<span class="admin-badge admin-badge--off">Disabled</span>`;
    return `<tr>
      <td>#${escapeHtml(t.id)}</td>
      <td>
        <div style="font-weight:900">${escapeHtml(t.textEn || "")}</div>
        <div style="color:var(--text-muted);font-size:.85rem">${escapeHtml(t.textAr || "")}</div>
      </td>
      <td>${escapeHtml(t.sortOrder ?? 0)}</td>
      <td>${active}</td>
      <td>
        <div class="admin-actions">
          <button class="admin-btn admin-btn--ghost" type="button" data-action="editTicker" data-id="${escapeHtml(t.id)}">Edit</button>
          <button class="admin-btn admin-btn--danger" type="button" data-action="deleteTicker" data-id="${escapeHtml(t.id)}">Delete</button>
        </div>
      </td>
    </tr>`;
  }).join("");

  const meta = $("metaTicker");
  if (meta) meta.textContent = String(items.length);
  renderPager("ticker", items.length, "tickerPager", loadTicker);
}

async function loadCategoryOptions() {
  const categoryInput = document.getElementById("categoryId");
  if (!categoryInput || categoryInput.dataset.loaded === "1") return;
  const categories = await window.ApiClient.request("/api/categories");
  const opts = [`<option value="">Select category</option>`].concat(
    (categories || []).map(c => `<option value="${escapeHtml(c.id)}">${escapeHtml(c.nameEn || c.nameAr || ("#" + c.id))}</option>`)
  ).join("");
  categoryInput.innerHTML = opts;
  categoryInput.dataset.loaded = "1";
}

async function deleteProduct(id) {
  if (!confirm("Delete this product?")) return;
  await window.ApiClient.adminDeleteProduct(id);
  showToast("Product deleted.", "ok");
  await loadProducts();
}

async function deleteOffer(id) {
  if (!confirm("Delete this offer?")) return;
  await window.ApiClient.adminDeleteOffer(id);
  showToast("Offer deleted.", "ok");
  await loadOffers();
}

async function deleteTicker(id) {
  if (!confirm("Delete this ticker message?")) return;
  await window.ApiClient.adminDeleteTickerMessage(id);
  showToast("Ticker message deleted.", "ok");
  await loadTicker();
}

async function refreshAllData() {
  await Promise.all([loadProducts(), loadOffers(), loadTicker()]);
}

async function editProduct(id) {
  const data = await window.ApiClient.request("/api/products/" + id);
  const nameAr = prompt("Arabic name", data.nameAr);
  if (nameAr === null) return;
  const nameEn = prompt("English name", data.nameEn);
  if (nameEn === null) return;
  const price = prompt("Price", String(data.price));
  if (price === null) return;
  const oldPrice = prompt("Old price (optional)", data.oldPrice ?? "");
  const imageUrl = prompt("Image URL", data.imageUrl);
  if (imageUrl === null) return;
  const stockQty = prompt("Stock quantity", String(data.stockQty));
  if (stockQty === null) return;
  const categoryId = prompt("Category ID", String(data.categoryId || 1));
  if (categoryId === null) return;
  const isOnSale = confirm("Is on sale?");

  await window.ApiClient.adminUpdateProduct(id, {
    nameAr, nameEn, price: Number(price), oldPrice: oldPrice ? Number(oldPrice) : null,
    imageUrl, stockQty: Number(stockQty), categoryId: Number(categoryId), isOnSale
  });
  await loadProducts();
}

async function editOffer(id) {
  const offers = await window.ApiClient.request("/api/offers");
  const current = offers.find(o => o.id === id);
  if (!current) return;

  const titleAr = prompt("Arabic title", current.titleAr);
  if (titleAr === null) return;
  const titleEn = prompt("English title", current.titleEn);
  if (titleEn === null) return;
  const descriptionAr = prompt("Arabic description", current.descriptionAr || "");
  if (descriptionAr === null) return;
  const descriptionEn = prompt("English description", current.descriptionEn || "");
  if (descriptionEn === null) return;
  const discountPercent = prompt("Discount percent", String(current.discountPercent));
  if (discountPercent === null) return;
  const badgeText = prompt("Badge text", current.badgeText);
  if (badgeText === null) return;
  const icon = prompt("Icon", current.icon);
  if (icon === null) return;
  const validUntil = prompt("Valid until (ISO date)", new Date().toISOString());
  if (validUntil === null) return;
  const isActive = confirm("Offer active?");

  await window.ApiClient.adminUpdateOffer(id, {
    titleAr, titleEn, descriptionAr, descriptionEn, discountPercent: Number(discountPercent),
    badgeText, icon, validUntil, isActive
  });
  await loadOffers();
}

async function editTicker(id) {
  const items = await window.ApiClient.adminGetTickerMessages();
  const current = items.find(t => t.id === id);
  if (!current) return;
  const textAr = prompt("Arabic ticker text", current.textAr);
  if (textAr === null) return;
  const textEn = prompt("English ticker text", current.textEn);
  if (textEn === null) return;
  const sortOrder = prompt("Sort order", String(current.sortOrder));
  if (sortOrder === null) return;
  const isActive = confirm("Ticker message active?");
  await window.ApiClient.adminUpdateTickerMessage(id, {
    textAr, textEn, sortOrder: Number(sortOrder), isActive
  });
  await loadTicker();
}

document.addEventListener("DOMContentLoaded", async () => {
  if (!ensureAdminAuth()) return;

  setActiveView("products");
  initOrderRealtimeNotifications();

  const nav = $("adminNav");
  if (nav) {
    nav.addEventListener("click", (e) => {
      const btn = e.target.closest(".admin-nav__item");
      if (!btn) return;
      setActiveView(btn.dataset.view || "products");
    });
  }

  document.addEventListener("click", async (e) => {
    if (e.target && e.target.closest("[data-close='1']")) {
      return closeModal();
    }
    const btn = e.target.closest("button[data-action][data-id]");
    if (!btn) return;
    const id = Number(btn.dataset.id);
    const action = btn.dataset.action;
    try {
      if (action === "editProduct") return await openEditProduct(id);
      if (action === "deleteProduct") return await deleteProduct(id);
      if (action === "editOffer") return await openEditOffer(id);
      if (action === "deleteOffer") return await deleteOffer(id);
      if (action === "editTicker") return await openEditTicker(id);
      if (action === "deleteTicker") return await deleteTicker(id);
    } catch (err) {
      showToast(err && err.message ? err.message : "Action failed.", "error");
    }
  });

  const editForm = $("editForm");
  if (editForm) {
    editForm.addEventListener("submit", async (e) => {
      e.preventDefault();
      if (!EditState.type || !EditState.id) return;
      const payload = getEditCurrentFromForm();
      try {
        if (EditState.type === "product") {
          await window.ApiClient.adminUpdateProduct(EditState.id, payload);
          await loadProducts();
          showToast("Product updated.", "ok");
        } else if (EditState.type === "offer") {
          await window.ApiClient.adminUpdateOffer(EditState.id, payload);
          await loadOffers();
          showToast("Offer updated.", "ok");
        } else if (EditState.type === "ticker") {
          await window.ApiClient.adminUpdateTickerMessage(EditState.id, payload);
          await loadTicker();
          showToast("Ticker message updated.", "ok");
        }
        closeModal();
      } catch (err) {
        showToast(err && err.message ? err.message : "Save failed.", "error");
      }
    });
  }

  const search = $("productsSearch");
  if (search) {
    search.addEventListener("input", async () => {
      try { await loadProducts(); } catch { /* ignore */ }
    });
  }

  document.getElementById("logoutBtn").addEventListener("click", () => {
    window.ApiClient.clearAdminToken();
    window.location.href = "admin-login.html";
  });

  const refreshBtn = $("refreshAdminBtn");
  if (refreshBtn) {
    refreshBtn.addEventListener("click", async () => {
      refreshBtn.disabled = true;
      const oldText = refreshBtn.textContent;
      refreshBtn.textContent = "Refreshing...";
      try {
        await refreshAllData();
        showToast("Admin data refreshed.", "ok");
      } catch (err) {
        showToast(err && err.message ? err.message : "Refresh failed.", "error");
      } finally {
        refreshBtn.disabled = false;
        refreshBtn.textContent = oldText;
      }
    });
  }

  document.getElementById("productForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const f = e.target;
    await window.ApiClient.adminCreateProduct({
      nameAr: f.nameAr.value,
      nameEn: f.nameEn.value,
      price: Number(f.price.value),
      oldPrice: f.oldPrice.value ? Number(f.oldPrice.value) : null,
      imageUrl: f.imageUrl.value,
      isOnSale: f.isOnSale.checked,
      stockQty: Number(f.stockQty.value),
      categoryId: Number(f.categoryId.value)
    });
    f.reset();
    showToast("Product created.", "ok");
    await loadProducts();
  });

  document.getElementById("offerForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const f = e.target;
    await window.ApiClient.adminCreateOffer({
      titleAr: f.titleAr.value,
      titleEn: f.titleEn.value,
      descriptionAr: f.descriptionAr.value,
      descriptionEn: f.descriptionEn.value,
      discountPercent: Number(f.discountPercent.value),
      badgeText: f.badgeText.value,
      icon: f.icon.value,
      validUntil: new Date(f.validUntil.value).toISOString(),
      isActive: f.isActive.checked
    });
    f.reset();
    renderOfferPreview("create", {});
    showToast("Offer created.", "ok");
    await loadOffers();
  });

  document.getElementById("tickerForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const f = e.target;
    await window.ApiClient.adminCreateTickerMessage({
      textAr: f.textAr.value,
      textEn: f.textEn.value,
      sortOrder: Number(f.sortOrder.value),
      isActive: f.isActive.checked
    });
    f.reset();
    showToast("Ticker message created.", "ok");
    await loadTicker();
  });

  try {
    await refreshAllData();
    const productForm = document.getElementById("productForm");
    if (productForm) {
      const updateCreateProductPreview = () => {
        const categoryLabel = productForm.categoryId && productForm.categoryId.selectedOptions && productForm.categoryId.selectedOptions[0]
          ? productForm.categoryId.selectedOptions[0].textContent
          : "Category";
        renderProductPreview("create", {
          nameAr: productForm.nameAr.value,
          nameEn: productForm.nameEn.value,
          price: Number(productForm.price.value || 0),
          oldPrice: productForm.oldPrice.value ? Number(productForm.oldPrice.value) : null,
          imageUrl: productForm.imageUrl.value,
          isOnSale: productForm.isOnSale.checked,
          categoryLabel
        });
      };
      productForm.addEventListener("input", updateCreateProductPreview);
      productForm.addEventListener("change", updateCreateProductPreview);
      updateCreateProductPreview();
    }

    const offerForm = document.getElementById("offerForm");
    if (offerForm) {
      const updateCreateOfferPreview = () => {
        const validUntilIso = offerForm.validUntil && offerForm.validUntil.value
          ? new Date(offerForm.validUntil.value).toISOString()
          : null;
        renderOfferPreview("create", {
          titleAr: offerForm.titleAr.value,
          titleEn: offerForm.titleEn.value,
          badgeText: offerForm.badgeText.value,
          discountPercent: Number(offerForm.discountPercent.value || 0),
          icon: offerForm.icon.value,
          validUntil: validUntilIso
        });
      };
      offerForm.addEventListener("input", updateCreateOfferPreview);
      offerForm.addEventListener("change", updateCreateOfferPreview);
      updateCreateOfferPreview();
    }
  } catch (err) {
    showToast("Failed to load admin data. Make sure API is running and token is valid.", "error");
  }
});
