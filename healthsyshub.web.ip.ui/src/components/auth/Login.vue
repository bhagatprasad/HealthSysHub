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
                  <input type="username" class="form-control" id="username"
                    placeholder="username/phone" v-model="credentials.username" required>
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
                    >
                    <div class="input-group-append">
                      <span class="input-group-text" @click="togglePasswordVisibility">
                        <i :class="showPassword ? 'feather icon-eye-off' : 'feather icon-eye'"></i>
                      </span>
                    </div>
                  </div>
                </div>
                <div class="custom-control custom-checkbox text-left mb-4 mt-2">
                  <input type="checkbox" class="custom-control-input" id="rememberMe"
                    v-model="rememberMe">
                  <label class="custom-control-label" for="rememberMe">Remember me</label>
                </div>
                <button type="submit" class="btn btn-block btn-primary mb-4" :disabled="loading">
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
              <div v-if="error" class="alert alert-danger mt-3">
                {{ error }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useAuthStore } from '@/stores/auth.store'
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
  methods: {
    async handleLogin() {
      this.error = ''
      this.loading = true
      
      try {
        await this.authStore.login(this.credentials)
        
        if (this.rememberMe) {
          localStorage.setItem('rememberedUsername', this.credentials.username)
        }
        
        const redirectPath = this.$route.query.redirect || '/dashboard'
        this.$router.push(redirectPath)
        
      } catch (error) {
        this.error = error.response?.data?.message || 'Login failed. Please try again.'
        console.error('Login error:', error)
      } finally {
        this.loading = false
      }
    }
  },
  mounted() {
    const rememberedUsername = localStorage.getItem('rememberedUsername')
    if (rememberedUsername) {
      this.credentials.username = rememberedUsername
      this.rememberMe = true
    }
  }
}
</script>

<style scoped>
.auth-wrapper {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #f5f5f5;
}

.auth-content {
  width: 400px;
  max-width: 90%;
}

.btn:disabled {
  opacity: 0.7;
  cursor: not-allowed;
}

.input-group-text {
  cursor: pointer;
  background-color: #fff;
  border-left: 0;
}

.input-group-text:hover {
  background-color: #f8f9fa;
}

.form-control:focus + .input-group-append .input-group-text {
  border-color: #80bdff;
}
</style>