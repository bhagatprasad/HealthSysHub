import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ReactiveFormsModule } from '@angular/forms';
import { PasswordToggleDirective } from '../directives/password.toggle';
import { NotificationService } from '../services/notification.service';
import { AccountService } from '../services/account.service';
import { PasswordService } from '../services/password.service';
import { AuditFieldsService } from '../services/audit-fields.service';
import { IApplicationUser } from '../models/applicationuser';
import { CommonModule } from '@angular/common';
import { ChangePassword } from '../models/changepassword';
import { ChangePasswordResult } from '../models/changepasswordresult';

@Component({
  imports: [PasswordToggleDirective, ReactiveFormsModule, CommonModule],
  standalone: true,
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.css']
})
export class PasswordChangeComponent implements OnInit {
  changePasswordForm: FormGroup;
  applicationUser: IApplicationUser | null = null;

  constructor(private fb: FormBuilder,
    private notifyService: NotificationService,
    private accountService: AccountService,
    private passwordService: PasswordService,
    private auditService: AuditFieldsService) {
    this.changePasswordForm = this.fb.group({
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
    var currentUser = this.accountService.getCurrentUser();
    if (currentUser) {
      this.applicationUser = currentUser;
    }
  }

  OnSubmit() {
    if (this.changePasswordForm.valid) {
      // Handle form submission
      console.log('Passwords match!', this.changePasswordForm.value);
      const changePasswordData: ChangePassword = {
        id: this.applicationUser?.id,
        hospitalId: this.changePasswordForm.value.hospitalId,
        staffId: this.applicationUser?.staffId,
        labId: this.applicationUser?.labId,
        password: this.changePasswordForm.value.newPassword
      };
      console.log("form data", changePasswordData);

      const changePassword = this.auditService.appendAuditFields(changePasswordData);

      console.log("After Audit", changePassword);

      this.passwordService.changePasswordAsync(changePassword).subscribe({
        next: (result) => this.handlePasswordResponse(result),
        error: (error) => this.handleAuthError(error),
      });
    } else {
      this.notifyService.showError("Invalid form submited");
    }
  }
  private handlePasswordResponse(changePasswordResult: ChangePasswordResult) {
    if (changePasswordResult.success) {
      this.notifyService.showSuccess(changePasswordResult.message);
    }else{
      this.notifyService.showError(changePasswordResult.message);
    }
  }
  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to update password';
    this.notifyService.showError(errorMessage);
  }
}