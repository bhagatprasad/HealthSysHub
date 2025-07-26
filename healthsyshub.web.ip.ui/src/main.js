import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index'
import { createPinia } from 'pinia'
import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'
import accountService from '@/global/API/auth.service'
import toastPlugin from '@/plugins/toast' // Import your toast plugin

const app = createApp(App)
const pinia = createPinia()

// Toastification global config
app.use(Toast, {
  position: 'top-right',
  transition: 'Vue-Toastification__bounce',
  maxToasts: 3,                  // Limit simultaneous toasts
  newestOnTop: true,             // New toasts appear on top
  filterBeforeCreate: (toast, toasts) => {
    // Prevent duplicate toasts
    if (toasts.some(t => t.content.toString() === toast.content.toString())) {
      return false
    }
    return toast
  }
})

// Core setup
app.use(pinia)
app.use(router)
app.use(toastPlugin) // Register your toast plugin
accountService.initialize()

app.mount('#app')