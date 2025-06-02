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
import { Pharmacy } from '../models/pharmacy';
import { ForgotPassword } from '../models/forgotpassword';
import { ForgotPasswordResponse } from '../models/forgotpasswordresponse';
import { ResetPassword } from '../models/resetpassword';
import { ResetPasswordResponse } from '../models/resetpasswordresponse';
import { ActivateOrInActivateUser } from '../models/activateorinactivateuser';
import { ActivateOrInActivateUserResponse } from '../models/activateorinactivateuserresponse';
import { ProfileUpdateRequest } from '../models/profile-update-request';
import { ProfileUpdateRequestResponse } from '../models/profile-update-request-response';

@Injectable({ providedIn: 'root' })
export class AccountService implements OnDestroy {
  private readonly authEndpoint = environment.UrlConstants.Authenticate;
  private readonly claimsEndpoint = environment.UrlConstants.GenerateUserCliams;
  public authenticationState = new BehaviorSubject<boolean | null>(null);
  private isBrowser: boolean;
  private inactivityTimer: any;
  private readonly INACTIVITY_TIMEOUT = 30 * 60 * 1000; // 30 minutes
  private destroy$ = new Subject<void>();
  public redirectUrl: string = '/landing'; // Set default to landing page

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

    // Immediate synchronous check
    const isAuth = this.isAuthenticated();
    this.authenticationState.next(isAuth);

    // Auto-redirect if authenticated and on login page
    if (isAuth && this.isLoginPage()) {
      this.router.navigate([this.redirectUrl]);
    }

    if (isAuth) {
      this.resetInactivityTimer();
    }
  }

  private isLoginPage(): boolean {
    return this.isBrowser && window.location.pathname.includes('/login');
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

  forgotPasswordAsync(forgotpassword: ForgotPassword): Observable<ForgotPasswordResponse> {
    return this.apiService.send<ForgotPasswordResponse>('POST', environment.UrlConstants.ForgotPasswordAsync, forgotpassword);
  }
  resetPasswordAsync(resetpassword: ResetPassword): Observable<ResetPasswordResponse> {
    return this.apiService.send<ResetPasswordResponse>('POST', environment.UrlConstants.ResetPasswordAsync, resetpassword);
  }
  activateOrInActivateUserAsync(activateuser: ActivateOrInActivateUser): Observable<ActivateOrInActivateUserResponse> {
    return this.apiService.send<ActivateOrInActivateUserResponse>('POST', environment.UrlConstants.ActivateOrInActivateUserAsync, activateuser);
  }

  profileUpdateRequestAsync(profileUpdateRequest: ProfileUpdateRequest): Observable<ProfileUpdateRequestResponse> {
    return this.apiService.send<ProfileUpdateRequestResponse>('POST', environment.UrlConstants.ProfileUpdateRequestAsync, profileUpdateRequest);
  }

  storeUserSession(user: IApplicationUser, token: string): void {
    this.safeLocalStorageSet('ApplicationUser', JSON.stringify(user));

    this.safeLocalStorageSet('AccessToken', token);
    this.authenticationState.next(true);
    this.resetInactivityTimer();

    // Use redirectUrl if set, otherwise default to landing
    const redirect = this.redirectUrl || '/landing';
    this.redirectUrl = '/landing'; // Reset to default after use
    this.router.navigateByUrl(redirect);
  }
  storeUserPharmacy(pharmacy: Pharmacy): void {
    this.safeLocalStorageSet('ApplicationUserPharmacy', JSON.stringify(pharmacy));
  }
  clearUserSession(): void {
    this.safeLocalStorageRemove('ApplicationUser');
    this.safeLocalStorageRemove('AccessToken');
    this.safeLocalStorageRemove('ApplicationUserPharmacy');
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
  getCurrentApplicationUserPharmacy(): Pharmacy | null {
    const pharmacy = this.safeLocalStorageGet('ApplicationUserPharmacy');
    return pharmacy ? JSON.parse(pharmacy) : null;
  }
}