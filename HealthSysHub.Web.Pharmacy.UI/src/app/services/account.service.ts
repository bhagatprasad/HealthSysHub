import { Injectable, PLATFORM_ID, Inject, OnDestroy } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { BehaviorSubject, Observable, Subject, of } from 'rxjs';
import { filter, distinctUntilChanged, take } from 'rxjs/operators';
import { Router } from '@angular/router';
import { environment } from '../../environment';
import { ApiService } from './apiservice.service';
import { IUserAuthentication } from '../models/userauthentication';
import { IAuthResponse } from '../models/authresponse';
import { IApplicationUser } from '../models/applicationuser';

@Injectable({
  providedIn: 'root'
})
export class AccountService implements OnDestroy {
  private readonly authEndpoint = environment.UrlConstants.Authenticate;
  private readonly claimsEndpoint = environment.UrlConstants.GenerateUserCliams;
  private authenticationState = new BehaviorSubject<boolean | null>(null);
  private isBrowser: boolean;
  private inactivityTimer: any;
  private readonly INACTIVITY_TIMEOUT = 30 * 60 * 1000; // 30 minutes
  private destroy$ = new Subject<void>();
  public redirectUrl: string = '';

  authenticationState$ = this.authenticationState.asObservable().pipe(
    filter(state => state !== null),
    distinctUntilChanged()
  );

  constructor(
    private apiService: ApiService,
    private router: Router,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
    this.initializeAuthState();
    this.setupInactivityMonitoring();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.clearInactivityTimer();
  }

  private initializeAuthState(): void {
    if (!this.isBrowser) {
      this.authenticationState.next(false);
      return;
    }
    
    setTimeout(() => {
      const isAuth = this.isAuthenticated();
      this.authenticationState.next(isAuth);
      if (isAuth) {
        this.resetInactivityTimer();
      }
    }, 50);
  }

  private setupInactivityMonitoring(): void {
    if (!this.isBrowser) return;

    const events = ['mousemove', 'keypress', 'scroll', 'click'];
    events.forEach(event => {
      window.addEventListener(event, this.resetInactivityTimer.bind(this));
    });
  }

  private resetInactivityTimer(): void {
    this.clearInactivityTimer();
    if (this.isAuthenticated()) {
      this.inactivityTimer = setTimeout(
        () => this.clearUserSession(), 
        this.INACTIVITY_TIMEOUT
      );
    }
  }

  private clearInactivityTimer(): void {
    if (this.inactivityTimer) {
      clearTimeout(this.inactivityTimer);
      this.inactivityTimer = null;
    }
  }

  isAuthenticated(): boolean {
    return !!this.safeLocalStorageGet('ApplicationUser') && 
           !!this.safeLocalStorageGet('AccessToken');
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
    this.safeLocalStorageSet('ApplicationUser', JSON.stringify(user));
    this.safeLocalStorageSet('AccessToken', token);
    this.authenticationState.next(true);
    this.resetInactivityTimer();
    
    const redirect = this.redirectUrl || '/dashboard';
    this.redirectUrl = '';
    this.router.navigateByUrl(redirect);
  }

  clearUserSession(): void {
    this.safeLocalStorageRemove('ApplicationUser');
    this.safeLocalStorageRemove('AccessToken');
    this.authenticationState.next(false);
    this.clearInactivityTimer();
    
    if (this.isBrowser && !this.router.url.startsWith('/login')) {
      this.router.navigate(['/login']);
    }
  }

  checkInitialAuthState(): boolean {
    return this.isAuthenticated();
  }

  getCurrentUser(): IApplicationUser | null {
    const user = this.safeLocalStorageGet('ApplicationUser');
    return user ? JSON.parse(user) : null;
  }

  getAccessToken(): string | null {
    return this.safeLocalStorageGet('AccessToken');
  }
}