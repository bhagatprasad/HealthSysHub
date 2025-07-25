<template>
  <Navbar />
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