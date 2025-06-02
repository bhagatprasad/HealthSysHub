import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { PharmacyOrderRequestService } from '../../services/pharmacy-order-request-service';
import { PaymentService } from '../../services/payment.service';
import { NotificationService } from '../../services/notification.service';
import { AccountService } from '../../services/account.service';
import { Pharmacy } from '../../models/pharmacy';
import { forkJoin } from 'rxjs';
import { PharmacyOrderRequestDetails } from '../../models/pharmacyorderrequestdetails';
import { PharmacyOrderDetails } from '../../models/pharmacy-order-details';
import { PharmacyPaymentDetail } from '../../models/pharmacy-payment-detail';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  currentPharmacy: Pharmacy | null = null;
  currentOrders: PharmacyOrderRequestDetails[] = [];
  currentSales: PharmacyOrderDetails[] = [];
  currentPayments: PharmacyPaymentDetail[] = [];

  constructor(private router: Router,
    private ordersService: PharmacyOrderRequestService,
    private salesService: OrderService,
    private paymentsService: PaymentService,
    private notifyService: NotificationService,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    var pharmacy = this.accountService.getCurrentApplicationUserPharmacy();
    if (pharmacy) {
      this.currentPharmacy = pharmacy;
      this.loadDashBoardAsync(pharmacy.pharmacyId);
    }
  }
  loadDashBoardAsync(pharmacyId: string) {
    forkJoin([
      this.ordersService.GetPharmacyOrderRequestsByPharmacyAsync(pharmacyId),
      this.salesService.GetPharmacyOrdersListByPharmacyAsync(pharmacyId),
      this.paymentsService.GetPharmacyPaymentListAsync(pharmacyId)])
      .subscribe(
        result => {
          this.handleOrdersResponseSuccess(result[0]);
          this.handlePharmacySalesSuccessResponse(result[1]);
          this.handlePharmacyPaymentsSuccessResponse(result[2]);
        },
        error => this.handleAuthError(error));
  }
  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to load medicines';
    this.notifyService.showError(errorMessage);
  }

  private handleOrdersResponseSuccess(orders: PharmacyOrderRequestDetails[]): void {
    this.currentOrders = orders
    console.log("order", orders);
  }
  private handlePharmacySalesSuccessResponse(sales: PharmacyOrderDetails[]): void {
    this.currentSales = sales;
    console.log("sales", sales);
  }
  private handlePharmacyPaymentsSuccessResponse(payments: PharmacyPaymentDetail[]): void {
    this.currentPayments = payments;
    console.log("payments", payments);
  }

  requestToOrders(): void {
    this.router.navigate(["/orders"]);
  }
  requestToSales(): void {
    this.router.navigate(["/sales"]);
  }
}