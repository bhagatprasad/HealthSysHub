import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { NotificationService } from '../../services/notification.service';
import { PharmacyService } from '../../services/pharmacy.service';
import { Pharmacy } from '../../models/pharmacy';

@Component({
  selector: 'app-signup',
  imports: [RouterModule, ReactiveFormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  pharmacyForm: FormGroup;

  constructor(private fb: FormBuilder,
    private notifyService: NotificationService,
    private pharmacyService: PharmacyService,
    private router: Router) {
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
      // Handle the form submission logic here
      console.log('Form Data:', formData);


      const pharmacy: Pharmacy = {
        hospitalId: this.pharmacyForm.value.hospitalId,
        pharmacyName: this.pharmacyForm.value.pharmacyName,
        pharmacyCode: this.pharmacyForm.value.pharmacyCode,
        registrationNumber: this.pharmacyForm.value.registrationNumber,
        address: this.pharmacyForm.value.address,
        city: this.pharmacyForm.value.city,
        state: this.pharmacyForm.value.state,
        country: this.pharmacyForm.value.country,
        postalCode: this.pharmacyForm.value.postalCode,
        phoneNumber: this.pharmacyForm.value.phoneNumber,
        email: this.pharmacyForm.value.email,
        website: this.pharmacyForm.value.website,
        logoUrl: this.pharmacyForm.value.logoUrl,
        isActive: this.pharmacyForm.value.isActive,
        createdOn: new Date(),
        modifiedOn: new Date(),
        createdBy: "",
        modifiedBy: ""
      };

      console.log(JSON.stringify(pharmacy));

      this.pharmacyService.InsertOrUpdatePharmacyAsync(pharmacy).subscribe({
        next: (response) => this.handlePharmacyOnBoarding(response),
        error: (error) => this.handleAuthError(error)
      })
    }
  }
  private handlePharmacyOnBoarding(pharmacy: Pharmacy): void {
    if (pharmacy) {
      this.notifyService.showSuccess("Welcome onboarding, now you can login");
        this.router.navigate(['/thankyou'], {
          queryParams: {
            pharmacyId: pharmacy.pharmacyId,
          }
        }).then(() => {
          this.notifyService.showSuccess("Thankyou");
        }).catch(err => {
          console.error('Navigation error:', err);
          this.notifyService.showError('Failed to navigate to reset password page');
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