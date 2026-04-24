/* Centralized API client for frontend pages */
(function attachApiClient(global) {
  const DEFAULT_BASE_URL = "https://localhost:7268";

  function getBaseUrl() {
    const fromWindow = global.__API_BASE_URL__;
    const fromStorage = global.localStorage ? localStorage.getItem("fm_api_base") : null;
    return fromWindow || fromStorage || DEFAULT_BASE_URL;
  }

  async function request(path, options) {
    const response = await fetch(getBaseUrl() + path, {
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
        ...(options && options.headers ? options.headers : {})
      },
      ...options
    });

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
    }
  };

  global.ApiClient = api;
})(window);
