<template>
  <div v-if="isLoading" class="global-loader">
    <div class="spinner-border text-primary" role="status">
      <span class="sr-only">Loading...</span>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'

export const isLoading = ref(false)

export const showLoader = () => (isLoading.value = true)
export const hideLoader = () => (isLoading.value = false)

export default {
  name: 'Loader',
  setup() {
    return { isLoading }
  }
}
</script>

<style scoped>
.global-loader {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999;
}

.spinner-border {
  width: 3rem;
  height: 3rem;
}
</style>