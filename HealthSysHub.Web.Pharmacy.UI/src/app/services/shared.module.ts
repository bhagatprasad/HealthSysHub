import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountService } from './account.service';
import { HospitalService } from './hospital.service';
import { PharmacyService } from './pharmacy.service';
import { PharmacyMedicineService } from './pharmacy-medicine-service';
import { PharmacyStaffService } from './pharmacystaff.service';
import { PharmacyOrderRequestService } from './pharmacy-order-request-service';
import { AuditFieldsService } from './audit-fields.service';
import { PasswordService } from './password.service';
import { NotificationService } from './notification.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    NotificationService,  
    AccountService,
    HospitalService,
    PharmacyService,
    PharmacyMedicineService,
    PharmacyStaffService,
    PharmacyOrderRequestService,
    AuditFieldsService,
    PasswordService
  ],
  exports: []
})
export class SharedModule { }
