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
                                    <input type="email" class="form-control" id="Email" placeholder="Email address"
                                        v-model="credentials.email" required>
                                </div>
                                <div class="form-group mb-4">
                                    <input type="password" class="form-control" id="Password" placeholder="Password"
                                        v-model="credentials.password" required>
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
                                <router-link to="/reset-password" class="f-w-400">Reset</router-link>
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
import authService from '@/global/API/auth.service';

export default {
    name: "Login",
    data() {
        return {
            credentials: {
                email: '',
                password: ''
            },
            rememberMe: false,
            loading: false,
            error: ''
        };
    },
    methods: {
        async handleLogin() {
            this.error = '';

            try {
                // Debug: Check credentials before sending
                console.log('Login credentials:', this.credentials);

                this.loading = true;

                // Debug: Verify the authService is properly imported
                console.log('AuthService:', authService);

                var authenticate = {
                    username: this.credentials.email,
                    password: this.credentials.password
                };

                const response = await authService.authenticateUser(authenticate);

                // Debug: Inspect the full response
                console.log('Full response:', response);
                console.log('Response data:', response?.data);

                // Handle successful login
                if (this.rememberMe) {
                    localStorage.setItem('rememberedEmail', this.credentials.email);
                    // Debug: Verify storage
                    console.log('Stored email:', localStorage.getItem('rememberedEmail'));
                }

                // Debug: Check route redirect
                console.log('Redirect path:', this.$route.query.redirect);

                const redirectPath = this.$route.query.redirect || '/dashboard';
                this.$router.push(redirectPath);

            } catch (error) {
                // Enhanced error logging
                console.error('Detailed login error:', {
                    message: error.message,
                    response: error.response,
                    stack: error.stack
                });

                this.error = error.response?.data?.message || 'Login failed. Please try again.';
            } finally {
                this.loading = false;
                // Debug: Verify loading state
                console.log('Loading complete, state:', this.loading);
            }
        }
    },
    mounted() {
        // Load remembered email if exists
        const rememberedEmail = localStorage.getItem('rememberedEmail');
        if (rememberedEmail) {
            this.credentials.email = rememberedEmail;
            this.rememberMe = true;
        }
    }
};
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
</style>