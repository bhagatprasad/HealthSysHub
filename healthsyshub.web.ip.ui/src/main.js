import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index'
import { createPinia } from 'pinia'
import accountService from '@/global/API/auth.service'

const app = createApp(App)
const pinia = createPinia()

app.use(pinia)
app.use(router)

// Initialize auth state and inactivity monitoring
accountService.initialize()

app.mount('#app')