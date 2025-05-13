(function($) {
    'use strict';
    $(function() {
        // Wait for DOM to be ready
        $(document).ready(function() {
            var proBanner = document.querySelector('#proBanner');
            var bannerClose = document.querySelector('#bannerClose');
            
            if (!proBanner) {
                console.error('proBanner element not found');
                return;
            }

            if ($.cookie('plus-pro-banner') != "true") {
                proBanner.classList.add('d-flex');
            } else {
                proBanner.classList.add('d-none');
            }
            
            if (bannerClose) {
                bannerClose.addEventListener('click', function() {
                    proBanner.classList.add('d-none');
                    proBanner.classList.remove('d-flex');
                    var date = new Date();
                    date.setTime(date.getTime() + 24 * 60 * 60 * 1000); 
                    $.cookie('plus-pro-banner', "true", { expires: date });
                });
            } else {
                console.warn('bannerClose element not found');
            }
        });
    });
})(jQuery);