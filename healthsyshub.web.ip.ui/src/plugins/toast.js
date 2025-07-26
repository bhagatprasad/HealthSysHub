import toastService from '@/global/toste.service'

export default {
  install(app) {
    // Make available globally as $toast
    app.config.globalProperties.$toast = toastService
    
    // Optional: Provide for Composition API usage
    app.provide('toast', toastService)
  }
}