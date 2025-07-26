<template>
    <Notifications />
    <li>
        <div class="dropdown drp-user">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                <i class="feather icon-user"></i>
            </a>
            <div class="dropdown-menu dropdown-menu-right profile-notification">
                <div class="pro-head">
                    <img src="@/assets/images/user/avatar-1.jpg" class="img-radius" alt="User-Profile-Image">
                    <span>John Doe</span>
                    <a @click="handleLogOut" class="dud-logout" title="Logout">
                        <i class="feather icon-log-out"></i>
                    </a>
                </div>
                <ul class="pro-body">
                    <li><a href="user-profile.html" class="dropdown-item"><i class="feather icon-user"></i>
                            Profile</a>
                    </li>
                    <li><a href="email_inbox.html" class="dropdown-item"><i class="feather icon-mail"></i>
                            My Messages</a>
                    </li>
                    <li><a href="auth-signin.html" class="dropdown-item"><i class="feather icon-lock"></i>
                            Lock Screen</a>
                    </li>
                </ul>
            </div>
        </div>
    </li>


</template>

<script>
import { useAuthStore } from '@/stores/auth.store'
import { useRouter } from 'vue-router'
import Notifications from '@/components/common/Notifications.vue';

export default {
    name: 'Profile',
    components: {
        Notifications
    },
    setup() {
        const authStore = useAuthStore()
        const router = useRouter()

        const handleLogOut = async () => {
            await authStore.logout()
            router.push('/login')
        }

        return {
            handleLogOut
        }
    }
}
</script>

<style scoped>
/* If you need to add any component-specific styles */
</style>