import { createRouter, createWebHistory } from 'vue-router'
import Login from '@/components/auth/Login.vue'
import SignUp from '@/components/auth/SignUp.vue'
import ForgotPassword from '@/components/auth/ForgotPassword.vue'
import ChangePassword from '@/components/auth/ChangePassword.vue'
import ResetPasswrod from '@/components/auth/ResetPasswrod.vue'
import AcitivateInactivateUserAccount from '@/components/auth/AcitivateInactivateUserAccount.vue'
const routes = [
    {
        path: '/login',  // Changed from '/' to '/login'
        name: 'login',
        component: Login
    },
    {
        path: '/',
        redirect: '/login'  // Add redirect for root path
    },
    {
        path: '/signup',
        name: 'signup',
        component: SignUp
    },
    {
        path: '/forgotpassword',
        name: 'forgotpassword',
        component: ForgotPassword
    },
    {
        path: '/changepassword',
        name: 'changepassword',
        component: ChangePassword
    },
    {
        path: '/resetpassword',
        name: 'resetpassword',
        component: ResetPasswrod
    },
    {
        path: '/activateuser',
        name: 'activateuser',
        component: AcitivateInactivateUserAccount
    }
]

const router = createRouter({
    history: createWebHistory('/'),  // Updated to use import.meta.env
    routes
})

export default router