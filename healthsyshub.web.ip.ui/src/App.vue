<template>
  <div class="pcoded-main-container">
    <Sidebar />
    <div class="pcoded-content">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import Sidebar from '@/components/common/Sidebar.vue'

export default {
  name: 'App',
  components: {
    Sidebar
  }
}
</script>

<style>
/* Import the template CSS */
@import './assets/css/style.css';
</style>