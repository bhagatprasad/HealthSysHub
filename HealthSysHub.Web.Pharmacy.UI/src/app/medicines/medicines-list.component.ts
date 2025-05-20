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

  ngOnInit(): void {
    this.loadMedicines();
  }

  private loadMedicines(): void {
    const appUser = localStorage.getItem("ApplicationUser");
    if (appUser) {
      try {
        this.applicationUser = JSON.parse(appUser);
        if (this.applicationUser?.pharmacyId) {
          this.pharmacyMedicineService.GetPharmacyMedicineByPharmacyAsync(this.applicationUser.pharmacyId)
            .subscribe({
              next: (medicines) => this.handlePharmacyMedicineSuccess(medicines),
              error: (error) => this.handleAuthError(error),
            });
        }
      } catch (error) {
        this.handleAuthError(new Error('Invalid user data in local storage'));
      }
    }
  }

  private handlePharmacyMedicineSuccess(medicines: PharmacyMedicine[]): void {
    this.medicines = medicines.map(medicine => ({
      ...medicine,
      selected: false
    }));
  }

  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to load medicines';
    this.notificationService.showError(errorMessage);
  }
}
