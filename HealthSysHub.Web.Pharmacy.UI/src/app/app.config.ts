import { ApplicationConfig, ErrorHandler, inject } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, provideRouter, Router } from '@angular/router';
import { routes } from './app.routes';
import { provideToastr } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { LoaderService } from './services/loader.service';
import { LoadingInterceptor } from './intercepters/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([LoadingInterceptor])
    ),
    provideAnimations(),
    provideToastr({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      closeButton: true,
      progressBar: true,
    }),
    { provide: ErrorHandler, useClass: ErrorHandler },
    {
      provide: 'routerEvents',
      useFactory: () => {
        const router = inject(Router);
        const loaderService = inject(LoaderService);
        router.events.subscribe({
          next: (event) => {
            if (event instanceof NavigationStart) {
              console.log('Router - navigation started');
              loaderService.show();
            }
            if (event instanceof NavigationEnd || 
                event instanceof NavigationCancel ||
                event instanceof NavigationError) {
              console.log('Router - navigation ended');
              loaderService.hide();
            }
          },
          error: (err) => {
            console.error('Router error:', err);
            loaderService.reset();
          }
        });
      }
    }
  ]
};