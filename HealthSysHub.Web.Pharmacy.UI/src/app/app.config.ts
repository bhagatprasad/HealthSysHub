import { ApplicationConfig } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [
    // Must come first
    provideAnimations(),
    
    // Toastr configuration
    provideToastr({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      progressBar: true,
      closeButton: true,
      disableTimeOut: false,
      tapToDismiss: false,
      easeTime: 300,
      newestOnTop: true
    }),
  ]
};