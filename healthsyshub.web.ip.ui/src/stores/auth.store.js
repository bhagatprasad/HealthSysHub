import { defineStore } from 'pinia'
import accountService from '@/global/API/auth.service'
import hospitalService from '@/global/API/hospital.service'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    isAuthenticated: accountService.state.isAuthenticated,
    user: accountService.state.user,
    hospitalInformation: accountService.state.hospitalInformation,
    loading: false
  }),

  actions: {
    async login(credentials) {
      this.loading = true
      const authResponse = await accountService.authenticateUser(credentials)
      this.loading = false
      return authResponse
    },
    async managerUserSession(authResponse) {
      this.loading = true

      const user = await accountService.generateUserClaims(authResponse)

      this.isAuthenticated = true
      // this.user = user



      accountService.storeUserSession(user, authResponse.jwtToken)

      const hospitalInformation = await hospitalService.getHospitalInformationByIdAsync(user.hospitalId)

      accountService.setHospitalSession(hospitalInformation)

      this.loading = false
      return user;
    },
    logout() {
      this.isAuthenticated = false
      this.user = null
      accountService.clearUserSession()
    },

    initialize() {
      const storedUser = JSON.parse(localStorage.getItem('ApplicationUser'))
      const token = localStorage.getItem('AccessToken')

      if (storedUser && token) {
        this.isAuthenticated = true
        this.user = storedUser
      }
    }
  },

  getters: {
    currentUser: (state) => state.user
  }
})