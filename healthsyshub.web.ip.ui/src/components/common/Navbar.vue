<template>
  <div>
    <Header v-if="isAuthenticated"></Header>
    <Sidebar v-if="isAuthenticated"></Sidebar>
    <div class="pcoded-main-container" :class="{'full-width': !isAuthenticated}">
      <div class="pcoded-content">
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script>
import { useAuthStore } from '@/stores/auth.store'
import { computed } from 'vue'
import Header from './Header.vue'
import Sidebar from './Sidebar.vue'

export default {
  name: 'Navbar',
  components: {
    Header,
    Sidebar
  },
  setup() {
    const authStore = useAuthStore()
    const isAuthenticated = computed(() => authStore.isAuthenticated)
    
    return {
      isAuthenticated
    }
  }
}
</script>

<style scoped>
.full-width {
  margin-left: 0 !important;
  width: 100% !important;
}
</style>