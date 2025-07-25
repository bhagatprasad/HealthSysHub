import { createRouter, createWebHistory } from 'vue-router'
import Login from '@/components/auth/Login.vue'
import { useAuthStore } from '@/stores/auth.store'  // Changed from default import to named import
import ResetPasswrod from '@/components/auth/ResetPasswrod.vue'
import AcitivateInactivateUserAccount from '@/components/auth/AcitivateInactivateUserAccount.vue'
import ChangePassword from '@/components/auth/ChangePassword.vue'
import ForgotPassword from '@/components/auth/ForgotPassword.vue'
import SignUp from '@/components/auth/SignUp.vue'

const routes = [
  {
    path: '/login',
    name: 'login',
    component: Login,
    meta: { requiresAuth: false }
  },
  {
    path: '/',
    redirect: '/login',
    meta: { requiresAuth: false }
  },
  {
    path: '/signup',
    name: 'signup',
    component: SignUp,
    meta: { requiresAuth: false }
  },
  {
    path: '/forgotpassword',
    name: 'forgotpassword',
    component: ForgotPassword,
    meta: { requiresAuth: false }
  },
  {
    path: '/changepassword',
    name: 'changepassword',
    component: ChangePassword,
    meta: { requiresAuth: true }
  },
  {
    path: '/resetpassword',
    name: 'resetpassword',
    component: ResetPasswrod,
    meta: { requiresAuth: false }
  },
  {
    path: '/activateuser',
    name: 'activateuser',
    component: AcitivateInactivateUserAccount,
    meta: { requiresAuth: true }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()  // Create store instance here
  const isAuthenticated = authStore.isAuthenticated  // Directly access state property
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth)

  if (requiresAuth && !isAuthenticated) {
    next('/login')
  } else if ((to.path === '/login' || to.path === '/') && isAuthenticated) {
    next('/dashboard')
  } else {
    next()
  }
})

export default router