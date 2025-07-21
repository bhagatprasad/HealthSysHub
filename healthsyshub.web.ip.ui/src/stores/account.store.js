// src/stores/account.store.js
import { defineStore } from 'pinia';
import { send } from '@/global/API/api.service'
import router from '@/router';

const INACTIVITY_TIMEOUT = 30 * 60 * 1000; // 30 minutes

export const useAccountStore = defineStore('account', {
  state: () => ({
    isAuthenticated: !!localStorage.getItem('AccessToken'),
    user: JSON.parse(localStorage.getItem('ApplicationUser')) || null,
    pharmacy: JSON.parse(localStorage.getItem('ApplicationUserPharmacy')) || null,
    redirectUrl: '/landing',
    inactivityTimer: null
  }),

  actions: {
    initialize() {
      this.setupInactivityMonitoring();
      if (this.isAuthenticated) {
        this.resetInactivityTimer();
      }
    },

    setupInactivityMonitoring() {
      const events = ['mousemove', 'keypress', 'scroll', 'click'];
      events.forEach(event => {
        window.addEventListener(event, this.resetInactivityTimer);
      });
    },

    resetInactivityTimer() {
      this.clearInactivityTimer();
      if (this.isAuthenticated) {
        this.inactivityTimer = setTimeout(
          this.clearUserSession,
          INACTIVITY_TIMEOUT
        );
      }
    },

    clearInactivityTimer() {
      if (this.inactivityTimer) {
        clearTimeout(this.inactivityTimer);
        this.inactivityTimer = null;
      }
    },

    async authenticateUser(userAuthentication) {
      try {
        return await send('POST', 'Authenticate', userAuthentication);
      } catch (error) {
        throw error;
      }
    },

    async generateUserClaims(authResponse) {
      try {
        const user = await send('POST', 'GenerateUserClaims', authResponse);
        return user;
      } catch (error) {
        throw error;
      }
    },

    storeUserSession(user, token) {
      localStorage.setItem('ApplicationUser', JSON.stringify(user));
      localStorage.setItem('AccessToken', token);
      this.isAuthenticated = true;
      this.user = user;
      this.resetInactivityTimer();

      const redirect = this.redirectUrl || '/landing';
      this.redirectUrl = '/landing';
      router.push(redirect);
    },

    storeUserPharmacy(pharmacy) {
      localStorage.setItem('ApplicationUserPharmacy', JSON.stringify(pharmacy));
      this.pharmacy = pharmacy;
    },

    clearUserSession() {
      localStorage.removeItem('ApplicationUser');
      localStorage.removeItem('AccessToken');
      localStorage.removeItem('ApplicationUserPharmacy');
      this.isAuthenticated = false;
      this.user = null;
      this.pharmacy = null;
      this.clearInactivityTimer();

      if (!window.location.pathname.startsWith('/login')) {
        router.push('/login');
      }
    }
  },

  getters: {
    currentUser: (state) => state.user,
    accessToken: (state) => localStorage.getItem('AccessToken'),
    currentPharmacy: (state) => state.pharmacy
  }
});