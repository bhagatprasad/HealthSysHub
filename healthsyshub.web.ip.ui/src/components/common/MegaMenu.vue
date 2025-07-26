<template>
    <li class="nav-item">
        <a href="#!" class="pop-search"><i class="feather icon-search"></i></a>
        <div class="search-bar">
            <input type="text" class="form-control border-0 shadow-none" placeholder="Search hear">
            <button type="button" class="close" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    </li>
    <li class="nav-item">
        <div class="dropdown">
            <a class="dropdown-toggle h-drop" href="#" data-toggle="dropdown">Dropdown</a>
            <div class="dropdown-menu profile-notification">
                <ul class="pro-body">
                    <li><a href="user-profile.html" class="dropdown-item"><i class="fas fa-circle"></i>
                            Profile</a></li>
                    <li><a href="email_inbox.html" class="dropdown-item"><i class="fas fa-circle"></i> My
                            Messages</a>
                    </li>
                    <li><a href="auth-signin.html" class="dropdown-item"><i class="fas fa-circle"></i> Lock
                            Screen</a>
                    </li>
                </ul>
            </div>
        </div>
    </li>
    <li class="nav-item">
        <div class="dropdown mega-menu">
            <a class="dropdown-toggle h-drop" href="#" data-toggle="dropdown">Mega</a>
            <div class="dropdown-menu profile-notification">
                <div class="row no-gutters">
                    <div class="col">
                        <h6 class="mega-title">UI Element</h6>
                        <ul class="pro-body">
                            <li><a href="#!" class="dropdown-item"><i class="fas fa-circle"></i> Alert</a>
                            </li>
                            <li><a href="#!" class="dropdown-item"><i class="fas fa-circle"></i> Button</a>
                            </li>
                            <li><a href="#!" class="dropdown-item"><i class="fas fa-circle"></i> Badges</a>
                            </li>
                            <li><a href="#!" class="dropdown-item"><i class="fas fa-circle"></i> Cards</a>
                            </li>
                            <li><a href="#!" class="dropdown-item"><i class="fas fa-circle"></i> Modal</a>
                            </li>
                            <li><a href="#!" class="dropdown-item"><i class="fas fa-circle"></i> Tabs &
                                    pills</a></li>
                        </ul>
                    </div>
                    <div class="col">
                        <h6 class="mega-title">Forms</h6>
                        <ul class="pro-body">
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-minus"></i>
                                    Elements</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-minus"></i>
                                    Validation</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-minus"></i>
                                    Masking</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-minus"></i>
                                    Wizard</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-minus"></i>
                                    Picker</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-minus"></i>
                                    Select</a></li>
                        </ul>
                    </div>
                    <div class="col">
                        <h6 class="mega-title">Application</h6>
                        <ul class="pro-body">
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-mail"></i>
                                    Email</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-clipboard"></i>
                                    Task</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-check-square"></i>
                                    To-Do</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-image"></i>
                                    Gallery</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-help-circle"></i>
                                    Helpdesk</a></li>
                        </ul>
                    </div>
                    <div class="col">
                        <h6 class="mega-title">Extension</h6>
                        <ul class="pro-body">
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-file-plus"></i>
                                    Editor</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-file-minus"></i>
                                    Invoice</a></li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-calendar"></i>
                                    Full calendar</a>
                            </li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-upload-cloud"></i>
                                    File upload</a>
                            </li>
                            <li><a href="#!" class="dropdown-item"><i class="feather icon-scissors"></i>
                                    Image cropper</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </li>
</template>

<script>
export default {
    name: 'MegaMenu',
}
</script>

<style lang="scss" scoped></style>