import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountService } from './account.service';
import { HospitalService } from './hospital.service';
import { PharmacyService } from './pharmacy.service';
import { PharmacyMedicineService } from './pharmacy-medicine-service';
import { PharmacyStaffService } from './pharmacystaff.service';
import { PharmacyOrderRequestService } from './pharmacy-order-request-service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    AccountService,
    HospitalService,
    PharmacyService,
    PharmacyMedicineService,
    PharmacyStaffService,
    PharmacyOrderRequestService,
  ],
  exports: []
})
export class SharedModule { }
