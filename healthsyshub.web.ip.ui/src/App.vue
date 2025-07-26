<template>
  <Loader />
  <Navbar />
</template>

<script>
import Navbar from './components/common/Navbar.vue';
import Loader from './components/common/Loader.vue';
export default {
  name: 'App',
  components: {
    Navbar,
    Loader
  }
}
</script>

<style>
/* Import the template CSS */
@import './assets/css/style.css';
</style>