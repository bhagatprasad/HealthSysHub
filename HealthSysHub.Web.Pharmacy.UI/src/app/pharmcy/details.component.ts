import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { PharmacyService } from '../services/pharmacy.service';
import { NotificationService } from '../services/notification.service';
import { Pharmacy } from '../models/pharmacy';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-details',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  public pharmacy: Pharmacy | null = null;
  pharmacyForm: FormGroup;
  isEditing = false;

  // Add these to safely access form controls in template
  get pharmacyCodeControl(): FormControl {
    return this.pharmacyForm.get('pharmacyCode') as FormControl;
  }

  get registrationNumberControl(): FormControl {
    return this.pharmacyForm.get('registrationNumber') as FormControl;
  }

  get hospitalIdControl(): FormControl {
    return this.pharmacyForm.get('hospitalId') as FormControl;
  }

  constructor(
    private accountService: AccountService,
    private pharmacyService: PharmacyService,
    private notifyService: NotificationService,
    private fb: FormBuilder
  ) {
    this.pharmacyForm = this.fb.group({
      pharmacyName: ['', Validators.required],
      pharmacyCode: ['', Validators.required],
      registrationNumber: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
      postalCode: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9]{10,15}$/)]],
      email: ['', [Validators.required, Validators.email]],
      website: ['', Validators.pattern(/^(http|https):\/\/[^ "]+$/)],
      logoUrl: [''],
      hospitalId: [''],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.loadPharmacyDetails();
  }

  loadPharmacyDetails(): void {
    const pharmacyDetails = this.accountService.getCurrentApplicationUserPharmacy();
    if (pharmacyDetails) {
      this.pharmacy = pharmacyDetails;
      
      // First enable all controls to ensure values are patched
      this.pharmacyForm.enable();
      
      // Patch the values
      this.pharmacyForm.patchValue(pharmacyDetails);
      
      // Then disable the read-only fields
      this.pharmacyCodeControl.disable();
      this.registrationNumberControl.disable();
      this.hospitalIdControl.disable();
      
      this.toggleEdit(false);
    }
  }

  toggleEdit(enable: boolean): void {
    this.isEditing = enable;
    if (enable) {
      // Enable editable fields
      this.pharmacyForm.get('pharmacyName')?.enable();
      this.pharmacyForm.get('address')?.enable();
      this.pharmacyForm.get('city')?.enable();
      this.pharmacyForm.get('state')?.enable();
      this.pharmacyForm.get('country')?.enable();
      this.pharmacyForm.get('postalCode')?.enable();
      this.pharmacyForm.get('phoneNumber')?.enable();
      this.pharmacyForm.get('email')?.enable();
      this.pharmacyForm.get('website')?.enable();
      this.pharmacyForm.get('logoUrl')?.enable();
      this.pharmacyForm.get('isActive')?.enable();
    } else {
      this.pharmacyForm.disable();
      // Re-enable then disable read-only fields
      this.pharmacyCodeControl.enable();
      this.registrationNumberControl.enable();
      this.hospitalIdControl.enable();
      this.pharmacyCodeControl.disable();
      this.registrationNumberControl.disable();
      this.hospitalIdControl.disable();
    }
  }

  onSubmit(): void {
    if (this.pharmacyForm.invalid) {
      this.notifyService.showWarning('Please fill all required fields correctly', 'Validation Error');
      return;
    }

    const updatedPharmacy = {
      ...this.pharmacy,
      ...this.pharmacyForm.getRawValue()
    };

    this.pharmacyService.InsertOrUpdatePharmacyAsync(updatedPharmacy).subscribe({
      next: (response) => {
        this.pharmacy = response;
        this.notifyService.showSuccess('Pharmacy details updated successfully', 'Success');
        this.toggleEdit(false);
        this.loadPharmacyDetails();
      },
      error: (err) => {
        this.notifyService.showError('Failed to update pharmacy details', 'Error');
        console.error('Update error:', err);
      }
    });
  }

  onCancel(): void {
    if (this.pharmacy) {
      this.pharmacyForm.patchValue(this.pharmacy);
    }
    this.toggleEdit(false);
  }
}