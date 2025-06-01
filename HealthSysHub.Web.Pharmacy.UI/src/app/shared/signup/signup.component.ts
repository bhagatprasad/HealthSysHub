import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NotificationService } from '../../services/notification.service';
import { PharmacyService } from '../../services/pharmacy.service';
import { Pharmacy } from '../../models/pharmacy';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'] // Corrected from styleUrl to styleUrls
})
export class SignupComponent implements OnInit {
  pharmacyForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private notifyService: NotificationService,
    private pharmacyService: PharmacyService,
    private router: Router
  ) {
    this.pharmacyForm = this.fb.group({
      pharmacyId: [''],
      hospitalId: [''],
      pharmacyName: ['', Validators.required],
      pharmacyCode: ['', Validators.required],
      registrationNumber: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
      postalCode: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      website: [''],
      logoUrl: [''],
      isActive: [true]
    });
  }

  ngOnInit(): void { }

  onSubmit(): void {
    if (this.pharmacyForm.valid) {
      const formData = this.pharmacyForm.value;
      console.log('Form Data:', formData);

      const pharmacy: Pharmacy = {
        hospitalId: formData.hospitalId,
        pharmacyId: "",
        pharmacyName: formData.pharmacyName,
        pharmacyCode: formData.pharmacyCode,
        registrationNumber: formData.registrationNumber,
        address: formData.address,
        city: formData.city,
        state: formData.state,
        country: formData.country,
        postalCode: formData.postalCode,
        phoneNumber: formData.phoneNumber,
        email: formData.email,
        website: formData.website,
        logoUrl: formData.logoUrl,
        isActive: formData.isActive,
        createdOn: new Date(),
        modifiedOn: new Date(),
        createdBy: "",
        modifiedBy: ""
      };

      console.log(JSON.stringify(pharmacy));

      this.pharmacyService.InsertOrUpdatePharmacyAsync(pharmacy).subscribe({
        next: (response) => this.handlePharmacyOnBoarding(response),
        error: (error) => this.handleAuthError(error)
      });
    }
  }
  requestToLogin(): void {
    this.router.navigate(['/login']);
  }
  private handlePharmacyOnBoarding(pharmacy: Pharmacy): void {
    if (pharmacy) {
      this.notifyService.showSuccess("Welcome onboarding, now you can login");
      this.router.navigate(['/thankyou'], {
        queryParams: {
          pharmacyId: pharmacy.pharmacyId,
        }
      }).then(() => {
        this.notifyService.showSuccess("Thank you");
      }).catch(err => {
        console.error('Navigation error:', err);
        this.notifyService.showError('Failed to navigate to thank you page');
      });
    }
  }

  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to process pharmacy';
    this.notifyService.showError(errorMessage);
  }

  generatePharmacyCode(): void {
    const pharmacyName = this.pharmacyForm.get('pharmacyName')?.value?.trim();

    if (!pharmacyName) {
      return;
    }

    // Generate code part from pharmacy name
    const upperMatches = pharmacyName.match(/[A-Z]/g) || [];
    let codePart = '';

    if (upperMatches.length >= 3) {
      codePart = upperMatches.slice(0, 4).join('');
    } else {
      codePart = pharmacyName.replace(/[^a-zA-Z]/g, '').slice(0, 4).toUpperCase();
    }

    if (codePart.length < 3) {
      codePart = codePart.padEnd(3, 'X');
    }

    // Get current time HHMM
    const now = new Date();
    const hh = now.getHours().toString().padStart(2, '0');
    const mm = now.getMinutes().toString().padStart(2, '0');
    const ss = now.getSeconds().toString().padStart(2, '0'); // Get seconds
    const ms = Math.floor(now.getMilliseconds() / 10).toString().padStart(2, '0'); // Get milliseconds (in 2 digits)
    const timePart = hh + mm + ss + ms; // Combine HHMMSSMM
    const finalCode = codePart + timePart;
    this.pharmacyForm.get('pharmacyCode')?.setValue(finalCode);
  }
}
