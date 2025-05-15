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

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [
    CommonModule,
    RouterLink,
    PasswordToggleDirective,
    FormsModule,  // This provides ngForm directive
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
    private router: Router
  ) {}

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
    if (!authResponse?.JwtToken) {
      this.notificationService.showError(authResponse?.StatusMessage || 'Authentication failed');
      return;
    }

    this.accountService.generateUserClaims(authResponse)
      .subscribe({
        next: (user) => this.handleUserClaimsSuccess(user, authResponse.JwtToken),
        error: (error) => this.handleAuthError(error)
      });
  }

  private handleUserClaimsSuccess(user: IApplicationUser, token: string): void {
    if (!user) {
      this.notificationService.showError('Failed to load user claims');
      return;
    }

    this.accountService.storeUserSession(user, token);
    this.notificationService.showSuccess('Login successful');
    this.router.navigate(['/dashboard']); // Redirect to your desired route
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