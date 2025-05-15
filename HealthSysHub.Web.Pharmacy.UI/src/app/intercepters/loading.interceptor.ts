import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { LoaderService } from '../services/loader.service';
import { finalize } from 'rxjs';

export const LoadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loaderService = inject(LoaderService);
  
  console.log('Interceptor - request started');
  loaderService.show();

  return next(req).pipe(
    finalize(() => {
      console.log('Interceptor - request completed');
      loaderService.hide();
    })
  );
};