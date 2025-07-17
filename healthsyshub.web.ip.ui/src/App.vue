<template>
  <Navbar />
  <div class="pcoded-main-container">
    <div class="pcoded-content">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import Navbar from './components/common/Navbar.vue';

export default {
  name: 'App',
  components: {
    Navbar
  }
}
</script>

<style>
/* Import the template CSS */
@import './assets/css/style.css';
</style>