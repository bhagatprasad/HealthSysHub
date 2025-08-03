<template>
    <div>
        <h1>Hellow</h1>
    </div>
</template>

<script>
import { useAuthStore } from '@/stores/auth.store'
import { showLoader, hideLoader } from '@/components/common/Loader.vue'
export default {
    name: 'Dashboard',
    data() {
        return {
            loading: false,
            apiData: null
        }
    },
    setup() {
        const authStore = useAuthStore()

        return {
            authStore
        }
    },
    computed: {
        applicationUser() {
            // Get user from Vuex store
            return this.$store.state.user?.name || 'User';
        },
        userId() {
            // Get user ID from Vuex store
            return this.$store.state.user?.id;
        }
    },
    async created() {
        // This hook is called when the component is created
        await this.fetchData();
    },
    methods: {
        async fetchData() {

            try {
                showLoader();

                // Example API call using the user ID from store
                const response = await this.$axios.get(`/api/data/${this.userId}`);
                this.apiData = response.data;
                hideLoader();

            } catch (error) {
                console.error('Error fetching data:', error);
                hideLoader();
                // Handle error (show message, etc.)
            } finally {
                this.loading = false;
                hideLoader();
            }
        }
    }
}
</script>

<style lang="scss" scoped></style>