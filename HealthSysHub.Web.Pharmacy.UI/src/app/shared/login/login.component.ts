import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { AccountService } from '../../services/account.service';
import { NotificationService } from '../../services/notification.service';
import { IAuthResponse } from '../../models/authresponse';
import { IApplicationUser } from '../../models/applicationuser';
import { IUserAuthentication } from '../../models/userauthentication';
import { CommonModule } from '@angular/common';
import { PasswordToggleDirective } from '../../directives/password.toggle';
import { FormValidatorDirective } from '../../directives/form.validator';
import { PharmacyService } from '../../services/pharmacy.service';
import { Pharmacy } from '../../models/pharmacy';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    PasswordToggleDirective,
    FormsModule,
    FormValidatorDirective
  ],
})
export class LoginComponent {
  credentials: IUserAuthentication = {
    username: '',
    password: ''
  };

  rememberMe = false;
  isSubmitting = false;
  constructor(
    private accountService: AccountService,
    private notificationService: NotificationService,
    private pharmacyService: PharmacyService,
    private router: Router
  ) { }

  requestToLogin(form: NgForm): void {
    if (form.invalid) {
      this.markFormControlsAsTouched(form);
      return;
    }

    this.isSubmitting = true;
    this.credentials.username = this.credentials.username.trim();

    this.accountService.authenticateUser(this.credentials)
      .pipe(
        finalize(() => this.isSubmitting = false)
      )
      .subscribe({
        next: (authResponse) => this.handleAuthSuccess(authResponse),
        error: (error) => this.handleAuthError(error)
      });
  }

  private handleAuthSuccess(authResponse: IAuthResponse): void {
    if (!authResponse?.jwtToken) {
      this.notificationService.showError(authResponse?.statusMessage || 'Authentication failed');
      return;
    }
    this.accountService.generateUserClaims(authResponse)
      .subscribe({
        next: (user) => this.handleUserClaimsSuccess(user, authResponse.jwtToken),
        error: (error) => this.handleAuthError(error)
      });
  }

  private handleUserClaimsSuccess(user: IApplicationUser, token: string): void {
    if (!user) {
      this.notificationService.showError('Failed to load user claims');
      return;
    } else if (!user?.pharmacyId) {
      this.notificationService.showError('User is not associated with a pharmacy');
      return;
    }

    this.accountService.storeUserSession(user, token);
    this.pharmacyService.GetPharmacyByIdAsync(user.pharmacyId)
      .subscribe({
        next: (pharmacy) => this.handlePharmacySuccess(pharmacy, token, user),
        error: (error) => this.handleAuthError(error)
      });

  }
  private handlePharmacySuccess(pharmacy: Pharmacy, token: string, user: IApplicationUser): void {
    if (!pharmacy) {
      this.notificationService.showError('Unable to find the pharmacy');
      return;
    }
    this.accountService.storeUserPharmacy(pharmacy);
    this.notificationService.showSuccess('Login successful');

    // Redirect to stored URL or default landing page
    const redirectUrl = this.accountService.redirectUrl || '/landing';
    this.router.navigateByUrl(redirectUrl);
  }
  private handleAuthError(error: any): void {
    console.error('Authentication error:', error);
    const errorMessage = error?.error?.message || error.message || 'Login failed';
    this.notificationService.showError(errorMessage);
  }

  private markFormControlsAsTouched(form: NgForm): void {
    Object.values(form.controls).forEach(control => {
      control.markAsTouched();
    });
  }
}