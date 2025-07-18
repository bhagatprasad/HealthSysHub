<template>
  <div>
    <Header></Header>
    <sidebar></sidebar>
    <div class="pcoded-main-container">
      <div class="pcoded-content">
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script>
import Sidebar from './Sidebar.vue';
import Header from './Header.vue';
export default {
  name: 'Navbar',
  components: {
    Sidebar,
    Header
  }
}
</script>

<style scoped>
/* If you need to add any component-specific styles */
</style>