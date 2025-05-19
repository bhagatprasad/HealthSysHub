import { Component, OnInit } from '@angular/core';
import { PharmacyMedicineService } from '../services/pharmacy-medicine-service';
import { PharmacyMedicine } from '../models/pharmacymedicine';
import { IApplicationUser } from '../models/applicationuser';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-medicines-list',
  imports: [],
  templateUrl: './medicines-list.component.html',
  styleUrl: './medicines-list.component.css'
})
export class MedicinesListComponent implements OnInit {

  medicines: PharmacyMedicine[] = [];
  applicationUser: IApplicationUser = {};

  constructor(private pharmacyMedicineService: PharmacyMedicineService, private notificationService: NotificationService) { }

  ngOnInit() {
    var appUser = localStorage.getItem("ApplicationUser");
    if (appUser) {
      this.applicationUser = JSON.parse(appUser);
      this.GetPharmacyMedicineByPharmacyAsync(this.applicationUser.pharmacyId);
    }
  }

  GetPharmacyMedicineByPharmacyAsync(pharmacyId?: string) {
    if (pharmacyId) {
      this.pharmacyMedicineService.GetPharmacyMedicineByPharmacyAsync(pharmacyId).subscribe({
        next: (medicines) => this.handlePharmacyMedicineSuccess(medicines),
        error: (error) => this.handleAuthError(error)
      })
    }
  }
  private handlePharmacyMedicineSuccess(medicines: PharmacyMedicine[]): void {
    this.medicines = medicines;
  }
  private handleAuthError(error: any): void {
    console.error('Authentication error:', error);
    const errorMessage = error?.error?.message || error.message || 'Login failed';
    this.notificationService.showError(errorMessage);
  }

}
