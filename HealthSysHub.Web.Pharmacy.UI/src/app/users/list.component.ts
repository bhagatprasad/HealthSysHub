import { Component, OnInit } from '@angular/core';
import { PharmacyStaff } from '../models/pharmacystaff';
import { PharmacyStaffService } from '../services/pharmacystaff.service';
import { NotificationService } from '../services/notification.service';
import { AccountService } from '../services/account.service';
import { Pharmacy } from '../models/pharmacy';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create.component';
import { AuditFieldsService } from '../services/audit-fields.service';

@Component({
  selector: 'app-list',
  imports: [CommonModule, CreateComponent],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class UsersListComponent implements OnInit {
  pharmacyStaffUser: PharmacyStaff[] = [];
  pharmcy: Pharmacy | null = null;

  showSidebar: boolean = false;
  selectedPharmacyStaff: PharmacyStaff | null = null;

  constructor(private pharmcyStaffService: PharmacyStaffService,
    private notifyService: NotificationService,
    private accountService: AccountService,
    private auditService: AuditFieldsService) {

  }
  ngOnInit(): void {
    var pharmacyDetails = this.accountService.getCurrentApplicationUserPharmacy();
    if (pharmacyDetails) {
      this.pharmcy = pharmacyDetails;
      if (this.pharmcy.pharmacyId)
        this.loadPharmacyStaffDetails(this.pharmcy.pharmacyId);
    }
  }
  loadPharmacyStaffDetails(pharmacyId: string): void {
    this.pharmcyStaffService.GetPharmacyStaffByharmacyAsync(pharmacyId).subscribe({
      next: (response) => {
        this.pharmacyStaffUser = response;
      },
      error: (err) => {
        this.notifyService.showError('Failed to pharmacy staff details', 'Error');
        console.error('Failed to pharmacy staff details:', err);
      }
    })
  }
  requestGetStaffDetails(staff: PharmacyStaff): void {
    console.log(staff);
  }
  requestStaffEdit(staff: PharmacyStaff): void {
    this.selectedPharmacyStaff = staff;
    this.showSidebar = true;
  }
  onSavePharmacyStaff(staff: PharmacyStaff): void {
    var _pharmacyStaff = this.auditService.appendAuditFields(staff);
    if (_pharmacyStaff) {
      _pharmacyStaff.hospitalId = this.pharmcy?.hospitalId;
      this.pharmcyStaffService.InsertOrUpdatePharmacyStaffAsync(_pharmacyStaff).subscribe({
        next: (response) => this.handleInsertOrUpdatePharmacyStaffSuccss(response),
        error: (err) => {
          this.notifyService.showError('Failed to pharmacy staff details', 'Error');
          console.error('Failed to pharmacy staff details:', err);
        }
      });
    }
  }
  handleInsertOrUpdatePharmacyStaffSuccss(staff: PharmacyStaff): void {
    this.selectedPharmacyStaff = null;
    this.showSidebar = false;
    if (this.pharmcy?.pharmacyId) {
      this.loadPharmacyStaffDetails(this.pharmcy?.pharmacyId);
    }
  }
  onCloseSidebar(): void {
    this.selectedPharmacyStaff = null;
    this.showSidebar = false;
  }
  openAddPharmacyStaff(): void {
    this.selectedPharmacyStaff = null;
    this.showSidebar = true;
  }
}
