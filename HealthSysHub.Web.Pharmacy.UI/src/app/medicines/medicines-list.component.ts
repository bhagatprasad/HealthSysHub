import { Component, OnInit } from '@angular/core';
import { PharmacyMedicineService } from '../services/pharmacy-medicine-service';
import { PharmacyMedicine } from '../models/pharmacymedicine';
import { IApplicationUser } from '../models/applicationuser';
import { NotificationService } from '../services/notification.service';
import { CommonModule } from '@angular/common';
import { MedicineSidebarComponent } from './medicine-sidebar.component';
import { AuditFieldsService } from '../services/audit-fields.service';

@Component({
  selector: 'app-medicines-list',
  templateUrl: './medicines-list.component.html',
  styleUrls: ['./medicines-list.component.css'],

  imports: [CommonModule, MedicineSidebarComponent]
})
export class MedicinesListComponent implements OnInit {

  medicines: PharmacyMedicine[] = [];
  applicationUser: IApplicationUser = {};
  showSidebar: boolean = false;
  selectedMedicine: PharmacyMedicine | null = null;
  constructor(
    private pharmacyMedicineService: PharmacyMedicineService,
    private notificationService: NotificationService,
    private auditService: AuditFieldsService
  ) { }

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
  requestMedicineProcess(medicine: PharmacyMedicine): void {
    this.selectedMedicine = { ...medicine };
    this.showSidebar = true;
  }
  requestGetMedicineSales(medicine: PharmacyMedicine): void {
    console.log(medicine);
  }
  openAddMedicine(): void {
    this.selectedMedicine = null;
    this.showSidebar = true;
  }
  onCloseSidebar(): void {
    this.showSidebar = false;
  }

  onSaveMedicine(pharmacyMedicine: PharmacyMedicine): void {
    // Handle save logic here
    console.log('Medicine to save:', pharmacyMedicine);

    var _pharmacyMedicine = this.auditService.appendAuditFields(pharmacyMedicine);

    console.log('Medicine final to save:', _pharmacyMedicine);
    this.pharmacyMedicineService.InsertOrUpdatePharmacyMedicineAsync(_pharmacyMedicine).subscribe({
      next: (medicine) => this.handlePharmacyMedicineInsertOrUpdateSuccess(medicine),
      error: (error) => this.handleAuthError(error),
    });
    
  }
  private handlePharmacyMedicineInsertOrUpdateSuccess(medicine: PharmacyMedicine): void {
    this.showSidebar = false;
    console.log('Medicine to saved:', medicine);
    this.notificationService.showSuccess("Pharmacy Medicine Proccessed Successful");
    this.loadMedicines();
  }
}
