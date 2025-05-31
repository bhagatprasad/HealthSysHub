import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { NotificationService } from '../services/notification.service';
import { AuditFieldsService } from '../services/audit-fields.service';
import { OrderService } from '../services/order.service';
import { Pharmacy } from '../models/pharmacy';
import { PharmacyOrderDetails } from '../models/pharmacy-order-details';
import { CommonModule, CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-list',
  imports: [CommonModule],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class OrdersListComponent implements OnInit {

  pharmacyOrderDetails: PharmacyOrderDetails[] = [];

  currentUserPharmacy: Pharmacy | null = null;

  constructor(private accountService: AccountService, private notifyService: NotificationService, private auditService: AuditFieldsService, private orderService: OrderService) { }

  ngOnInit(): void {
    var pharmacy = this.accountService.getCurrentApplicationUserPharmacy();
    if (pharmacy) {
      this.currentUserPharmacy = pharmacy;

      this.loadPharmacyOrderRequestDetailsAsync(this.currentUserPharmacy.pharmacyId);
    }
  }

  private loadPharmacyOrderRequestDetailsAsync(pharmacyId: string): void {
    this.orderService.GetPharmacyOrdersListByPharmacyAsync(pharmacyId).subscribe({
      next: (response) => this.handlePharmacyOrderDetails(response),
      error: (error) => this.handleAuthError(error)
    });
  }
  private handlePharmacyOrderDetails(_pharmacyOrderDetails: PharmacyOrderDetails[]): void {
    if (_pharmacyOrderDetails.length > 0) {
      this.pharmacyOrderDetails = _pharmacyOrderDetails;
    }
  }
  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to fetch orders';
    this.notifyService.showError(errorMessage);
  }
  requestOrderEdit(pharmacyOrderDetail: PharmacyOrderDetails) {

  }
  requestToPharmacyOrderDetails(pharmacyOrderDetail: PharmacyOrderDetails): void {

  }
  requestToSales(): void {

  }
  requestToAddOrder(): void {

  }
}
