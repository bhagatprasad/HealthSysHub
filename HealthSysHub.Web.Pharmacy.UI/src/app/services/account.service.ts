import { Injectable, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { BehaviorSubject, Observable } from 'rxjs';
import { IAuthResponse } from '../models/authresponse';
import { IApplicationUser } from '../models/applicationuser';
import { IUserAuthentication } from '../models/userauthentication';
import { Router } from '@angular/router';
import { environment } from '../../environment';
import { ApiService } from './apiservice.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly authEndpoint = environment.UrlConstants.Authenticate;
  private readonly claimsEndpoint = environment.UrlConstants.GenerateUserCliams;
  private authenticationState = new BehaviorSubject<boolean>(false);
  private isBrowser: boolean;

  authenticationState$ = this.authenticationState.asObservable();

  constructor(
    private apiService: ApiService,
    private router: Router,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
    this.checkAuthStatus();
  }

  private safeLocalStorageGet(key: string): string | null {
    return this.isBrowser ? localStorage.getItem(key) : null;
  }

  private safeLocalStorageSet(key: string, value: string): void {
    if (this.isBrowser) {
      localStorage.setItem(key, value);
    }
  }

  private safeLocalStorageRemove(key: string): void {
    if (this.isBrowser) {
      localStorage.removeItem(key);
    }
  }

  authenticateUser(userAuthentication: IUserAuthentication): Observable<IAuthResponse> {
    return this.apiService.send<IAuthResponse>('POST', this.authEndpoint, userAuthentication);
  }

  generateUserClaims(authResponse: IAuthResponse): Observable<IApplicationUser> {
    return this.apiService.send<IApplicationUser>('POST', this.claimsEndpoint, authResponse);
  }

  storeUserSession(user: IApplicationUser, token: string): void {
    this.safeLocalStorageRemove('ApplicationUser');
    this.safeLocalStorageRemove('AccessToken');
    this.safeLocalStorageSet('ApplicationUser', JSON.stringify(user));
    this.safeLocalStorageSet('AccessToken', token);
    this.authenticationState.next(true);
  }

  clearUserSession(): void {
    this.safeLocalStorageRemove('ApplicationUser');
    this.safeLocalStorageRemove('AccessToken');
    this.authenticationState.next(false);
    if (this.isBrowser) {
      this.router.navigate(['/login']);
    }
  }

  checkAuthStatus(): void {
    if (!this.isBrowser) {
      this.authenticationState.next(false);
      return;
    }
    
    const user = this.safeLocalStorageGet('ApplicationUser');
    const token = this.safeLocalStorageGet('AccessToken');
    this.authenticationState.next(!!user && !!token);
  }

  getCurrentUser(): IApplicationUser | null {
    const user = this.safeLocalStorageGet('ApplicationUser');
    return user ? JSON.parse(user) : null;
  }

  getAccessToken(): string | null {
    return this.safeLocalStorageGet('AccessToken');
  }
}