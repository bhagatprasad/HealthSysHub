import { createRouter, createWebHistory } from 'vue-router'
import { showLoader, hideLoader } from '@/components/common/Loader.vue'
import { useAuthStore } from '@/stores/auth.store'

// Lazy-loaded components for better performance
const Login = () => import('@/components/auth/Login.vue')
const ResetPassword = () => import('@/components/auth/ResetPasswrod.vue')
const ActivateInactivateUserAccount = () => import('@/components/auth/AcitivateInactivateUserAccount.vue')
const ChangePassword = () => import('@/components/auth/ChangePassword.vue')
const ForgotPassword = () => import('@/components/auth/ForgotPassword.vue')
const SignUp = () => import('@/components/auth/SignUp.vue')
const Dashboard = () => import('@/views/Dashboard.vue') // Added dashboard route

const routes = [
  {
    path: '/login',
    name: 'login',
    component: Login,
    meta: {
      requiresAuth: false,
      title: 'Login'
    }
  },
  {
    path: '/',
    redirect: '/dashboard',
    meta: { requiresAuth: true }
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    component: Dashboard,
    meta: {
      requiresAuth: true,
      title: 'Dashboard'
    }
  },
  {
    path: '/signup',
    name: 'signup',
    component: SignUp,
    meta: {
      requiresAuth: false,
      title: 'Sign Up'
    }
  },
  {
    path: '/forgotpassword',
    name: 'forgotpassword',
    component: ForgotPassword,
    meta: {
      requiresAuth: false,
      title: 'Forgot Password'
    }
  },
  {
    path: '/changepassword',
    name: 'changepassword',
    component: ChangePassword,
    meta: {
      requiresAuth: true,
      title: 'Change Password'
    }
  },
  {
    path: '/resetpassword',
    name: 'resetpassword',
    component: ResetPassword,
    meta: {
      requiresAuth: false,
      title: 'Reset Password'
    }
  },
  {
    path: '/activateuser',
    name: 'activateuser',
    component: ActivateInactivateUserAccount,
    meta: {
      requiresAuth: true,
      title: 'Activate Account'
    }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    return savedPosition || { top: 0 }
  }
})

// Global loader for route changes
router.beforeEach((to, from, next) => {
  showLoader() // Show loader before navigation
  document.title = to.meta.title || 'My App' // Dynamic page titles

  const authStore = useAuthStore()
  authStore.initialize() // Initialize auth state

  const isAuthenticated = authStore.isAuthenticated
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth)

  if (requiresAuth && !isAuthenticated) {
    next({ name: 'login', query: { redirect: to.fullPath } })
  } else if ((to.name === 'login' || to.path === '/') && isAuthenticated) {
    next({ name: 'dashboard' })
  } else {
    next()
  }
})

router.afterEach(() => {
  hideLoader() // Hide loader after navigation completes
})

// Handle navigation errors (optional)
router.onError((error) => {
  hideLoader()
  console.error('Router error:', error)
})

export default router