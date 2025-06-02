import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { NotificationService } from '../../services/notification.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IApplicationUser } from '../../models/applicationuser';
import { ProfileUpdateRequest } from '../../models/profile-update-request';
import { ProfileUpdateRequestResponse } from '../../models/profile-update-request-response';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  profileFrom: FormGroup;

  applicationUser: IApplicationUser | null = null;

  constructor(private accountService: AccountService, private notifyService: NotificationService, private fb: FormBuilder) {

    this.profileFrom = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phone: ['', Validators.required]
    });
  }
  ngOnInit(): void {
    var currentUser = this.accountService.getCurrentUser();
    if (currentUser) {
      this.applicationUser = currentUser;

      this.profileFrom.patchValue({
        firstName: this.applicationUser.firstName,
        lastName: this.applicationUser.lastName,
        phone: this.applicationUser.phone
      });
    }
  }
  OnSubmit(): void {
    const profileData: ProfileUpdateRequest = {
      id: this.applicationUser?.id,
      hospitalId: this.applicationUser?.hospitalId,
      staffId: this.applicationUser?.staffId,
      pharmacyId: this.applicationUser?.pharmacyId,
      entityType: "Pharmacy",
      firstName: this.profileFrom.value.firstName,
      lastName: this.profileFrom.value.lastName,
      phone: this.profileFrom.value.phone,
      modifiedBy: this.applicationUser?.id,
      modifiedOn: new Date().toDateString(),
      labId: null
    };

    this.accountService.profileUpdateRequestAsync(profileData).subscribe({
      next: (response) => this.handleProfileUpdateRespnse(response),
      error: (error) => this.handleAuthError(error),
    });

  }
  private handleProfileUpdateRespnse(profileResponse: ProfileUpdateRequestResponse): void {
    console.log('status:', profileResponse.message);
    if (profileResponse.success)
      this.notifyService.showSuccess(profileResponse.message);
    else
      this.notifyService.showError(profileResponse.message);
  }
  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to update profile';
    this.notifyService.showError(errorMessage);
  }
}
