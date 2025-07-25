import { send } from '@/global/API/api.service'
import router from '@/router/index'
import { endpoints } from '@/environment'
import { useAuthStore } from '@/stores/auth.store'

const INACTIVITY_TIMEOUT = 30 * 60 * 1000 // 30 minutes
let inactivityTimer = null

export default {
  state: {
    isAuthenticated: !!localStorage.getItem('AccessToken'),
    user: JSON.parse(localStorage.getItem('ApplicationUser')) || null,
    pharmacy: JSON.parse(localStorage.getItem('ApplicationUserPharmacy')) || null,
    redirectUrl: '/landing'
  },

  initialize() {
    this.setupInactivityMonitoring()
    if (this.state.isAuthenticated) {
      this.resetInactivityTimer()
    }
  },

  setupInactivityMonitoring() {
    const events = ['mousemove', 'keypress', 'scroll', 'click']
    events.forEach(event => {
      window.addEventListener(event, this.resetInactivityTimer.bind(this))
    })
  },

  resetInactivityTimer() {
    this.clearInactivityTimer()
    if (this.state.isAuthenticated) {
      inactivityTimer = setTimeout(
        () => this.clearUserSession(),
        INACTIVITY_TIMEOUT
      )
    }
  },

  clearInactivityTimer() {
    if (inactivityTimer) {
      clearTimeout(inactivityTimer)
      inactivityTimer = null
    }
  },

  async authenticateUser(userAuthentication) {
    const response = await send('POST', endpoints.UrlConstants.Authenticate, userAuthentication)
    return response
  },

  async generateUserClaims(authResponse) {
    const user = await send('POST', endpoints.UrlConstants.GenerateUserCliams, authResponse)
    return user
  },

  storeUserSession(user, token) {
    localStorage.setItem('ApplicationUser', JSON.stringify(user))
    localStorage.setItem('AccessToken', token)
    this.state.isAuthenticated = true
    this.state.user = user
    this.resetInactivityTimer()

    const authStore = useAuthStore()
    authStore.isAuthenticated = true
    authStore.user = user

    const redirect = this.state.redirectUrl || '/landing'
    this.state.redirectUrl = '/landing'
    router.push(redirect)
  },

  storeUserPharmacy(pharmacy) {
    localStorage.setItem('ApplicationUserPharmacy', JSON.stringify(pharmacy))
    this.state.pharmacy = pharmacy
  },

  clearUserSession() {
    localStorage.removeItem('ApplicationUser')
    localStorage.removeItem('AccessToken')
    localStorage.removeItem('ApplicationUserPharmacy')
    this.state.isAuthenticated = false
    this.state.user = null
    this.state.pharmacy = null
    this.clearInactivityTimer()

    const authStore = useAuthStore()
    authStore.isAuthenticated = false
    authStore.user = null

    if (!window.location.pathname.startsWith('/login')) {
      router.push('/login')
    }
  },

  getCurrentUser() {
    return this.state.user
  },

  getAccessToken() {
    return localStorage.getItem('AccessToken')
  },

  getCurrentPharmacy() {
    return this.state.pharmacy
  }
}