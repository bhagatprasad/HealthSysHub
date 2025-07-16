<template>
  <nav class="pcoded-navbar">
    <div class="navbar-wrapper">
      <div class="navbar-content scroll-div">
        <!-- User Profile -->
        <div class="">
          <div class="main-menu-header">
            <img class="img-radius" src="/assets/images/user/avatar-2.jpg" alt="User-Profile-Image">
            <div class="user-details">
              <span>John Doe</span>
              <div id="more-details">UX Designer<i class="fa fa-chevron-down m-l-5"></i></div>
            </div>
          </div>
        </div>

        <!-- Navigation Menu -->
        <ul class="nav pcoded-inner-navbar">
          <li class="nav-item pcoded-menu-caption">
            <label>Navigation</label>
          </li>
          <li class="nav-item">
            <router-link to="/" class="nav-link">
              <span class="pcoded-micon"><i class="feather icon-home"></i></span>
              <span class="pcoded-mtext">Dashboard</span>
            </router-link>
          </li>
          <!-- Other menu items -->
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: 'Sidebar'
}
</script>