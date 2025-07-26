<template>
    <header class="navbar pcoded-header navbar-expand-lg navbar-light header-dark">
        <div class="m-header">
            <a class="mobile-menu" id="mobile-collapse" href="#!"><span></span></a>
            <a href="#!" class="b-brand">
                <img src="@/assets/images/logo.png" alt="" class="logo">
                <img src="@/assets/images/logo-icon.png" alt="" class="logo-thumb">
            </a>
            <a href="#!" class="mob-toggler">
                <i class="feather icon-more-vertical"></i>
            </a>
        </div>
        <div class="collapse navbar-collapse">
            <ul class="navbar-nav mr-auto">
                <MegaMenu />
            </ul>
            <ul class="navbar-nav ml-auto">
                <Profile />
            </ul>
        </div>
    </header>
</template>
<script>
import Profile from '@/components/common/Profile.vue'
import MegaMenu from '@/components/common/MegaMenu.vue';

export default {
    name: 'Header',
    components: {
        Profile,
        MegaMenu
    }
}
</script>
<style scoped>
/* If you need to add any component-specific styles */
</style>