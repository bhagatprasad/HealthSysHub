import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { NotificationService } from '../services/notification.service';
import { AuditFieldsService } from '../services/audit-fields.service';
import { OrderService } from '../services/order.service';
import { Pharmacy } from '../models/pharmacy';
import { PharmacyOrderDetails } from '../models/pharmacy-order-details';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { Router } from '@angular/router';
import { ModalComponent } from '../shared/popups/modal.component';

@Component({
  selector: 'app-list',
  imports: [CommonModule, ModalComponent],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class OrdersListComponent implements OnInit {
  showModal = false;
  modalTitle: string = '';
  modalMessage: string = '';
  actionType: string = '';
  currentRequest: PharmacyOrderDetails | null = null;
  pharmacyOrderDetails: PharmacyOrderDetails[] = [];

  currentUserPharmacy: Pharmacy | null = null;

  constructor(private accountService: AccountService, private notifyService: NotificationService,
    private auditService: AuditFieldsService, private orderService: OrderService,
    private router: Router) { }

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
  handleConfirm(notes: string): void {

  }

  openModal(request: PharmacyOrderDetails, action: string): void {
    this.currentRequest = request;
    this.actionType = action;
    this.modalTitle = `${action} Order Request`;
    this.modalMessage = `Are you sure you want to ${action} this order request?`;
    this.showModal = true;
  }

  requestPayNow(request: PharmacyOrderDetails) {

  }
  requestToPharmacyOrderDetails(pharmacyOrderDetail: PharmacyOrderDetails): void {

  }
  requestToOrders(): void {
    this.router.navigate(["/orders"]);
  }
  requestToAddOrder(): void {
    this.router.navigate(["/addorder"]);
  }
}
