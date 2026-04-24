/* Centralized API client for frontend pages */
(function attachApiClient(global) {
  // Default API base url selection:
  // - If site is opened from LAN IP (e.g. 192.168.1.5:5500), use same host for API at :5131
  // - Otherwise default to local dev http profile
  // You can override explicitly with:
  // localStorage.setItem("fm_api_base", "http://192.168.1.5:5131")
  function defaultBaseUrl() {
    try {
      const host = global.location && global.location.hostname ? global.location.hostname : "localhost";
      const isLocal = host === "localhost" || host === "127.0.0.1";
      return isLocal ? "http://localhost:5131" : `http://${host}:5131`;
    } catch {
      return "http://localhost:5131";
    }
  }
  const DEFAULT_BASE_URL = defaultBaseUrl();
  const TOKEN_KEY = "fm_admin_token";
  const FALLBACK_BASE_URLS = ["https://localhost:5024", "http://localhost:5131"];
  let currentBaseOverride = null;

  function getBaseUrl() {
    const fromWindow = global.__API_BASE_URL__;
    const fromStorage = global.localStorage ? localStorage.getItem("fm_api_base") : null;
    return fromWindow || fromStorage || currentBaseOverride || DEFAULT_BASE_URL;
  }

  function getAdminToken() {
    return global.localStorage ? localStorage.getItem(TOKEN_KEY) : null;
  }

  function setAdminToken(token) {
    if (global.localStorage) {
      localStorage.setItem(TOKEN_KEY, token);
    }
  }

  function clearAdminToken() {
    if (global.localStorage) {
      localStorage.removeItem(TOKEN_KEY);
    }
  }

  function shouldAttachAuth(path, options) {
    if (options && options.auth === true) return true;
    return path.startsWith("/api/admin");
  }

  async function request(path, options) {
    const token = getAdminToken();
    let response;
    const baseTried = [];
    const baseCandidates = [getBaseUrl(), ...FALLBACK_BASE_URLS].filter((v, i, a) => v && a.indexOf(v) === i);
    let lastNetworkError = null;

    for (const baseUrl of baseCandidates) {
      baseTried.push(baseUrl);
      try {
        const body = options && Object.prototype.hasOwnProperty.call(options, "body") ? options.body : undefined;
        const isFormData = typeof FormData !== "undefined" && body instanceof FormData;
        const baseHeaders = {
          ...(token && shouldAttachAuth(path, options) ? { Authorization: "Bearer " + token } : {}),
          ...(options && options.headers ? options.headers : {})
        };
        // Don't set Content-Type for FormData; the browser must add boundary.
        const headers = isFormData ? baseHeaders : { "Content-Type": "application/json", ...baseHeaders };

        response = await fetch(baseUrl + path, {
          credentials: "include",
          headers,
          ...options
        });
        currentBaseOverride = baseUrl;
        break;
      } catch (e) {
        lastNetworkError = e;
      }
    }

    if (!response) {
      const err = new Error("Could not reach API. Check API URL, HTTPS certificate, or CORS.");
      err.cause = lastNetworkError || null;
      err.baseTried = baseTried;
      throw err;
    }

    let payload = null;
    const contentType = response.headers.get("content-type") || "";
    if (contentType.includes("application/json")) {
      payload = await response.json();
    } else {
      const text = await response.text();
      payload = text || null;
    }

    if (!response.ok) {
      const message = (payload && (payload.detail || payload.title)) || "API request failed.";
      const error = new Error(message);
      error.status = response.status;
      error.payload = payload;
      throw error;
    }

    return payload;
  }

  const api = {
    getBaseUrl,
    getAdminToken,
    setAdminToken,
    clearAdminToken,
    request,
    getHealth: function getHealth() {
      return request("/api/system/health", { method: "GET" });
    },
    getSession: function getSession() {
      return request("/api/system/session", { method: "GET" });
    },
    subscribeNewsletter: function subscribeNewsletter(email) {
      return request("/api/newsletter/subscribe", {
        method: "POST",
        body: JSON.stringify({ email: email })
      });
    },
    sendContactMessage: function sendContactMessage(data) {
      return request("/api/contact", {
        method: "POST",
        body: JSON.stringify(data)
      });
    },
    placeOrder: function placeOrder(data) {
      return request("/api/orders", {
        method: "POST",
        body: JSON.stringify(data)
      });
    },
    processPayment: function processPayment(data) {
      return request("/api/payments/process", {
        method: "POST",
        body: JSON.stringify(data)
      });
    },
    adminLogin: function adminLogin(email, password) {
      return request("/api/auth/admin-login", {
        method: "POST",
        body: JSON.stringify({ email: email, password: password })
      });
    },
    adminCreateProduct: function adminCreateProduct(data) {
      return request("/api/admin/products", { method: "POST", body: JSON.stringify(data), auth: true });
    },
    adminUpdateProduct: function adminUpdateProduct(id, data) {
      return request("/api/admin/products/" + id, { method: "PUT", body: JSON.stringify(data), auth: true });
    },
    adminDeleteProduct: function adminDeleteProduct(id) {
      return request("/api/admin/products/" + id, { method: "DELETE", auth: true });
    },
    adminCreateOffer: function adminCreateOffer(data) {
      return request("/api/admin/offers", { method: "POST", body: JSON.stringify(data), auth: true });
    },
    adminUpdateOffer: function adminUpdateOffer(id, data) {
      return request("/api/admin/offers/" + id, { method: "PUT", body: JSON.stringify(data), auth: true });
    },
    adminDeleteOffer: function adminDeleteOffer(id) {
      return request("/api/admin/offers/" + id, { method: "DELETE", auth: true });
    },
    adminGetTickerMessages: function adminGetTickerMessages() {
      return request("/api/admin/ticker-messages", { method: "GET", auth: true });
    },
    adminCreateTickerMessage: function adminCreateTickerMessage(data) {
      return request("/api/admin/ticker-messages", { method: "POST", body: JSON.stringify(data), auth: true });
    },
    adminUpdateTickerMessage: function adminUpdateTickerMessage(id, data) {
      return request("/api/admin/ticker-messages/" + id, { method: "PUT", body: JSON.stringify(data), auth: true });
    },
    adminDeleteTickerMessage: function adminDeleteTickerMessage(id) {
      return request("/api/admin/ticker-messages/" + id, { method: "DELETE", auth: true });
    },
    adminUploadImage: async function adminUploadImage(file) {
      const fd = new FormData();
      fd.append("file", file);
      return request("/api/admin/uploads/image", { method: "POST", body: fd, auth: true });
    }
  };

  global.ApiClient = api;
})(window);
