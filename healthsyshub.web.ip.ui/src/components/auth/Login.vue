<template>
  <div class="auth-wrapper">
    <div class="auth-content text-center">
      <img src="/assets/images/logo.png" alt="Logo" class="img-fluid mb-4">
      <div class="card borderless">
        <div class="row align-items-center">
          <div class="col-md-12">
            <div class="card-body">
              <h4 class="mb-3 f-w-400">Sign in</h4>
              <hr>
              <form @submit.prevent="handleLogin">
                <div class="form-group mb-3">
                  <input 
                    type="text" 
                    class="form-control" 
                    id="username" 
                    placeholder="username/phone"
                    v-model="credentials.username" 
                    required
                    @input="validateForm"
                  >
                </div>
                <div class="form-group mb-4">
                  <div class="input-group">
                    <input 
                      :type="showPassword ? 'text' : 'password'" 
                      class="form-control" 
                      id="Password"
                      placeholder="*******" 
                      v-model="credentials.password" 
                      required
                      @input="validateForm"
                    >
                    <div class="input-group-append">
                      <span class="input-group-text" @click="togglePasswordVisibility">
                        <i :class="showPassword ? 'feather icon-eye-off' : 'feather icon-eye'"></i>
                      </span>
                    </div>
                  </div>
                </div>
                <div class="custom-control custom-checkbox text-left mb-4 mt-2">
                  <input 
                    type="checkbox" 
                    class="custom-control-input" 
                    id="rememberMe" 
                    v-model="rememberMe"
                  >
                  <label class="custom-control-label" for="rememberMe">Remember me</label>
                </div>
                <button 
                  type="submit" 
                  class="btn btn-block btn-primary mb-4" 
                  :disabled="!isFormValid || loading"
                >
                  <span v-if="loading">Signing in...</span>
                  <span v-else>Sign in</span>
                </button>
              </form>
              <hr>
              <p class="mb-2 text-muted">
                Forgot password?
                <router-link to="/resetpassword" class="f-w-400">Reset</router-link>
              </p>
              <p class="mb-0 text-muted">
                Don't have an account?
                <router-link to="/signup" class="f-w-400">Sign up</router-link>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useAuthStore } from '@/stores/auth.store'
import { showLoader, hideLoader } from '@/components/common/Loader.vue'
import { ref } from 'vue' 

export default {
  name: "Login",
  data() {
    return {
      credentials: {
        username: '',
        password: ''
      },
      rememberMe: false,
      loading: false,
      error: ''
    }
  },
  setup() {
    const authStore = useAuthStore()
    const showPassword = ref(false)

    const togglePasswordVisibility = () => {
      showPassword.value = !showPassword.value
    }

    return {
      authStore,
      showPassword,
      togglePasswordVisibility
    }
  },
  computed: {
    isFormValid() {
      return this.credentials.username.trim() !== '' && 
             this.credentials.password.trim() !== ''
    }
  },
  methods: {
    validateForm() {
      this.error = ''
    },
    async handleLogin() {
      if (!this.isFormValid) return
      
      showLoader()
      this.error = ''
      this.loading = true

      try {
        const response = await this.authStore.login(this.credentials)
        if (response.jwtToken) {
          await this.authStore.managerUserSession(response)  // Removed unused 'user' assignment
          
          if (this.rememberMe) {
            localStorage.setItem('rememberedUsername', this.credentials.username)
          }

          const redirectPath = this.$route.query.redirect || '/dashboard'
          this.$router.push(redirectPath)
        } else {
          this.$toast.error(response.statusMessage)
        }
      } catch (error) {
        this.error = error.response?.data?.message || 'Login failed. Please try again.'
        console.error('Login error:', error)
      } finally {
        this.loading = false
        hideLoader()
      }
    }
  },
  mounted() {
    const rememberedUsername = localStorage.getItem('rememberedUsername')
    if (rememberedUsername) {
      this.credentials.username = rememberedUsername
      this.rememberMe = true
    }
    this.validateForm()
  }
}
</script>

<style scoped>
/* Your existing styles remain the same */
</style>