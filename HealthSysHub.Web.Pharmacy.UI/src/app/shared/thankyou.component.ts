import { Component, OnInit } from '@angular/core';
import { PharmacyService } from '../services/pharmacy.service';
import { Pharmacy } from '../models/pharmacy';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-thankyou',
  imports: [],
  standalone: true,
  templateUrl: './thankyou.component.html',
  styleUrl: './thankyou.component.css'
})
export class ThankyouComponent implements OnInit {
  pharmacy: Pharmacy | null = null;
  pharmacyId: string = "";
  constructor(private pharmacyService: PharmacyService,
    private router: Router, private notifyService: NotificationService,
    private activateRouterService: ActivatedRoute) { }

  ngOnInit(): void {
    this.activateRouterService.queryParams.subscribe(params => {
      this.pharmacyId = params['pharmacyId'];
      this.pharmacyService.GetPharmacyByIdAsync(this.pharmacyId).subscribe({
        next: (result) => this.handlePharmacyResponse(result),
        error: (error) => this.handleAuthError(error),
      });
    });

  }
  private handlePharmacyResponse(pharmacy: Pharmacy): void {
    this.pharmacy = pharmacy;
  }

  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to process reset password request';
    this.notifyService.showError(errorMessage);
  }
  requestToSupport(): void {

  }

  requestToLogin(): void {

  }
}
