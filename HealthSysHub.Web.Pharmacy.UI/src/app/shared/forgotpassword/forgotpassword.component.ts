import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { NotificationService } from '../../services/notification.service';
import { CommonModule } from '@angular/common';
import { ForgotPassword } from '../../models/forgotpassword';
import { ForgotPasswordResponse } from '../../models/forgotpasswordresponse';

@Component({
  selector: 'app-forgotpassword',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css']
})
export class ForgotpasswordComponent {
  forgotPasswordForm: FormGroup;
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private notifyService: NotificationService,
    private router: Router
  ) {
    this.forgotPasswordForm = this.fb.group({
      entityCode: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit(): void {
    if (this.forgotPasswordForm.valid) {
      this.isLoading = true;
      const formdata: ForgotPassword = {
        email: this.forgotPasswordForm.value.email,
        entityCode: this.forgotPasswordForm.value.entityCode,
        entityType: "Pharmacy"
      };

      this.accountService.forgotPasswordAsync(formdata).subscribe({
        next: (result) => {
          this.isLoading = false;
          this.handleForgotPasswordResponse(result);
        },
        error: (error) => {
          this.isLoading = false;
          this.handleAuthError(error);
        },
      });
    }
  }

  private handleForgotPasswordResponse(response: ForgotPasswordResponse) {
    if (response.success) {
      if (response.id && response.staffId) {
        this.router.navigate(['/resetpassword'], {
          queryParams: {
            id: response.id,
            staffId: response.staffId
          }
        }).then(() => {
          this.notifyService.showSuccess(response.message);
        }).catch(err => {
          console.error('Navigation error:', err);
          this.notifyService.showError('Failed to navigate to reset password page');
        });
      } else {
        this.notifyService.showError('Invalid response from server');
      }
    } else {
      this.notifyService.showError(response.message);
    }
  }

  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to process forgot password request';
    this.notifyService.showError(errorMessage);
  }

  navigateToLogin(): void {
    this.router.navigate(['/login']);
  }
}