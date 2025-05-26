import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../services/notification.service';
import { AuditFieldsService } from '../services/audit-fields.service';
import { PharmacyOrderRequestService } from '../services/pharmacy-order-request-service';
import { PharmacyOrderRequestDetails } from '../models/pharmacyorderrequestdetails';
import { AccountService } from '../services/account.service';
import { Pharmacy } from '../models/pharmacy';
import { CommonModule, DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order-request-list',
  imports: [DatePipe, CommonModule],
  templateUrl: './order-request-list.component.html',
  styleUrl: './order-request-list.component.css'
})
export class OrderRequestListComponent implements OnInit {

  //Declaration
  pharmacyOrderRequestDetails: PharmacyOrderRequestDetails[] = [];
  currentPharmacy: Pharmacy | null = null;

  //Initialize the services in construcotr 
  constructor(private notifyService: NotificationService,
    private auditService: AuditFieldsService,
    private pharmacyOrderRequestService: PharmacyOrderRequestService,
    private accountService: AccountService,
    private router: Router) { }

  ngOnInit(): void {
    var currentPharmacyDetails = this.accountService.getCurrentApplicationUserPharmacy();
    if (currentPharmacyDetails) {
      this.currentPharmacy = currentPharmacyDetails;
      this.loadPharmacyOrderRequests(this.currentPharmacy.pharmacyId);
    }
  }
  private loadPharmacyOrderRequests(pharmacyId: string) {
    this.pharmacyOrderRequestService.GetPharmacyOrderRequestsByPharmacyAsync(pharmacyId).subscribe({
      next: (response) => this.handlePharmacyOrderRequestDetails(response),
      error: (error) => this.handleAuthError(error)
    });
  }
  private handlePharmacyOrderRequestDetails(_pharmacyOrderRequestDetails: PharmacyOrderRequestDetails[]) {
    this.pharmacyOrderRequestDetails = _pharmacyOrderRequestDetails;
  }
  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to fetch orders';
    this.notifyService.showError(errorMessage);
  }
  requestOrderEdit(pharmacyOrderRequestDetails: PharmacyOrderRequestDetails) {

  }
  requestToAddOrder(): void {
    this.router.navigate(["/addorder"]);
  }
  requestGetOrderDetails(pharmacyOrderRequestDetails: PharmacyOrderRequestDetails) {

  }
  requestToSales(): void {

  }
}
