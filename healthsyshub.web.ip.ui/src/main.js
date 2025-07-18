import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index' // Correct import path to your router file

const app = createApp(App)

app.use(router)

app.mount('#app')