import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpEvent,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environment';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Get token from localStorage
    const accessToken = localStorage.getItem('AccessToken');
    
    // Clone the request to modify headers
    let headers = req.headers;
    
    if (accessToken) {
      // Add Authorization header if token exists
      headers = headers.set('Authorization', `Bearer ${accessToken}`);
    }

    // Clone request with new headers and base URL
    const apiReq = req.clone({
      url: `${environment.baseUrl}${req.url}`,
      headers: headers
    });

    return next.handle(apiReq);
  }
}
