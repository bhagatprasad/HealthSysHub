import { ErrorHandler, Injectable, inject } from '@angular/core';
import { LoaderService } from '../services/loader.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  private loaderService = inject(LoaderService);

  handleError(error: any): void {
    console.error('Global error handler:', error);
    this.loaderService.reset();
    // You can add more error handling logic here
  }
}