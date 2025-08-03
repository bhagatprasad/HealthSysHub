// src/services/api.service.js
import axios from 'axios';
import router from '@/router';
import { endpoints } from '@/environment';

const API_BASE_URL = endpoints.BaseUrl;

// Ensure baseURL ends with a single slash
const normalizedBaseUrl = API_BASE_URL.endsWith('/') 
  ? API_BASE_URL 
  : API_BASE_URL + '/';

const api = axios.create({
  baseURL: normalizedBaseUrl,
  headers: {
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  }
});

// Request interceptor
api.interceptors.request.use(
  (config) => {
    const accessToken = localStorage.getItem('AccessToken');
    
    // Only modify relative URLs
    if (config.url && !config.url.startsWith('http')) {
      // Remove leading slashes to prevent double slashes
      config.url = config.url.replace(/^\/+/, '');
    }

    // Add Authorization header if token exists
    if (accessToken) {
      config.headers['Authorization'] = accessToken;
    }

    // Log final URL for debugging
    console.debug(`[API] ${config.method?.toUpperCase()} ${config.baseURL}${config.url}`);
    
    return config;
  },
  (error) => {
    console.error('Request interceptor error:', error);
    return Promise.reject(error);
  }
);

// Response interceptor
api.interceptors.response.use(
  (response) => {
    console.debug(`[API Response] ${response.status} ${response.config.url}`);
    return response.data;
  },
  (error) => {
    console.error('[API Error]', {
      url: error.config?.url,
      status: error.response?.status,
      message: error.message,
      data: error.response?.data
    });

    if (error.response?.status === 401) {
      // Token expired or invalid
      localStorage.removeItem('AccessToken');
      localStorage.removeItem('ApplicationUser');
      router.push('/login?sessionExpired=true');
    }

    // Return consistent error format
    return Promise.reject({
      status: error.response?.status || 500,
      message: error.response?.data?.message || 'Network Error',
      data: error.response?.data
    });
  }
);

// Enhanced send method
export const send = async (method, url, data = null, customHeaders = {}) => {
  try {
    const config = {
      method,
      url,
      ...(data && { data }),
      headers: { ...customHeaders }
    };
    
    const response = await api(config);
    return response;
  } catch (error) {
    console.error('API request failed:', {
      endpoint: url,
      error: error.message
    });
    throw error;
  }
};

export default api;