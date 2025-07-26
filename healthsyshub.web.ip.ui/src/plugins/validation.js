import { useValidation } from '@/utils/validations'

export default {
  install(app) {
    app.config.globalProperties.$validate = useValidation()
    
    // For Composition API
    app.provide('validation', useValidation())
  }
}