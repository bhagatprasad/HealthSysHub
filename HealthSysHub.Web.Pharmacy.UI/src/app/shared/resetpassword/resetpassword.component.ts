import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NotificationService } from '../../services/notification.service';
import { AccountService } from '../../services/account.service';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PasswordToggleDirective } from '../../directives/password.toggle';
import { ResetPassword } from '../../models/resetpassword';
import { ResetPasswordResponse } from '../../models/resetpasswordresponse';

@Component({
  selector: 'app-resetpassword',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, PasswordToggleDirective],
  templateUrl: './resetpassword.component.html',
  styleUrl: './resetpassword.component.css'
})
export class ResetpasswordComponent implements OnInit {
  id?: string;
  staffId?: string;
  resetPasswordForm: FormGroup;
  constructor(private activateRouterService: ActivatedRoute,
    private notifyService: NotificationService,
    private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router) {
    this.resetPasswordForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }
  // Custom validator to check if passwords match
  passwordMatchValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const newPassword = control.get('newPassword')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (newPassword !== confirmPassword) {
      control.get('confirmPassword')?.setErrors({ mismatch: true });
      return { mismatch: true };
    } else {
      control.get('confirmPassword')?.setErrors(null);
      return null;
    }
  }
  ngOnInit(): void {
    this.activateRouterService.queryParams.subscribe(params => {
      this.id = params['id'];
      this.staffId = params['staffId'];
      if (!this.id || !this.staffId) {
        this.notifyService.showError("User or Staff Id's are missing,Please try again");
        this.router.navigate(['/forgotpassword']);
      }
    });
  }
  OnSubmit(): void {
    if (this.resetPasswordForm.valid) {
      const resetPasswordData: ResetPassword = {
        password: this.resetPasswordForm.value.newPassword,
        id: this.id,
        staffId: this.staffId,
        modifiedOn: new Date(),
      };

      this.accountService.resetPasswordAsync(resetPasswordData).subscribe({
        next: (result) => this.handleResetPasswordResponse(result),
        error: (error) => this.handleAuthError(error),
      });
    }
  }
  private handleResetPasswordResponse(resetPasswordResponse: ResetPasswordResponse) {
    if (resetPasswordResponse.success) {
      this.notifyService.showSuccess(resetPasswordResponse.message);
      this.router.navigate(["/login"]);
    } else {
      this.notifyService.showError(resetPasswordResponse.message);
    }
  }

  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to process reset password request';
    this.notifyService.showError(errorMessage);
  }
  requestTologin(): void {
    this.router.navigate(["/login"]);
  }
}
