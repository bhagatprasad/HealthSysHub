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
  orderStatuses: string[] = ["Pending", "Cancelled", "SentForPharmacy", "Completed"];

  // Order counts for status buttons
  orderCounts = {
    Pending: 0,
    Completed: 0,
    Cancelled: 0,
    SentForPharmacy: 0
  };

  // Sales counts
  salesCounts = {
    total: 0,
    today: 0,
    thisMonth: 0,
    thisYear: 0
  };

  // Payment counts
  paymentCounts = {
    total: {
      count: 0,
      sum: 0
    },
    today: {
      count: 0,
      sum: 0
    },
    thisWeek: {
      count: 0,
      sum: 0
    },
    thisMonth: {
      count: 0,
      sum: 0
    },
    thisYear: {
      count: 0,
      sum: 0
    }
  };

  constructor(
    private router: Router,
    private ordersService: PharmacyOrderRequestService,
    private salesService: OrderService,
    private paymentsService: PaymentService,
    private notifyService: NotificationService,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    const pharmacy = this.accountService.getCurrentApplicationUserPharmacy();
    if (pharmacy) {
      this.currentPharmacy = pharmacy;
      this.loadDashBoardAsync(pharmacy.pharmacyId);
    }
  }

  loadDashBoardAsync(pharmacyId: string) {
    forkJoin([
      this.ordersService.GetPharmacyOrderRequestsByPharmacyAsync(pharmacyId),
      this.salesService.GetPharmacyOrdersListByPharmacyAsync(pharmacyId),
      this.paymentsService.GetPharmacyPaymentListAsync(pharmacyId)
    ]).subscribe(
      result => {
        this.handleOrdersResponseSuccess(result[0]);
        this.handlePharmacySalesSuccessResponse(result[1]);
        this.handlePharmacyPaymentsSuccessResponse(result[2]);
      },
      error => this.handleAuthError(error)
    );
  }

  private calculateOrderCounts(): void {
    // Reset counts
    this.orderCounts = {
      Pending: 0,
      Completed: 0,
      Cancelled: 0,
      SentForPharmacy: 0
    };

    this.currentOrders.forEach(order => {
      if (order.status) {
        switch (order.status) {
          case 'Pending':
            this.orderCounts.Pending++;
            break;
          case 'Completed':
            this.orderCounts.Completed++;
            break;
          case 'Cancelled':
            this.orderCounts.Cancelled++;
            break;
          case 'SentForPharmacy':
            this.orderCounts.SentForPharmacy++;
            break;
        }
      }
    });
  }

  private calculateSalesCounts(): void {
    const today = new Date();
    const thisMonth = today.getMonth();
    const thisYear = today.getFullYear();

    this.salesCounts = {
      total: this.currentSales.length,
      today: this.currentSales.filter(sale => {
        if (!sale.createdOn) return false;
        const saleDate = new Date(sale.createdOn);
        return saleDate.getDate() === today.getDate() &&
               saleDate.getMonth() === thisMonth &&
               saleDate.getFullYear() === thisYear;
      }).length,
      thisMonth: this.currentSales.filter(sale => {
        if (!sale.createdOn) return false;
        const saleDate = new Date(sale.createdOn);
        return saleDate.getMonth() === thisMonth &&
               saleDate.getFullYear() === thisYear;
      }).length,
      thisYear: this.currentSales.filter(sale => {
        if (!sale.createdOn) return false;
        const saleDate = new Date(sale.createdOn);
        return saleDate.getFullYear() === thisYear;
      }).length
    };
  }

  private calculatePaymentCounts(): void {
    const now = new Date();
    const today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    const startOfWeek = new Date(today);
    startOfWeek.setDate(today.getDate() - today.getDay()); // Start of week (Sunday)
    const startOfMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    const startOfYear = new Date(today.getFullYear(), 0, 1);

    // Reset counts
    this.paymentCounts = {
      total: { count: 0, sum: 0 },
      today: { count: 0, sum: 0 },
      thisWeek: { count: 0, sum: 0 },
      thisMonth: { count: 0, sum: 0 },
      thisYear: { count: 0, sum: 0 }
    };

    this.currentPayments.forEach(payment => {
      if (!payment.paymentDate) return;

      const paymentDate = new Date(payment.paymentDate);
      const paymentDay = new Date(paymentDate.getFullYear(), paymentDate.getMonth(), paymentDate.getDate());
      const amount = payment.paymentAmount || 0;

      // Always increment total
      this.paymentCounts.total.count++;
      this.paymentCounts.total.sum += amount;

      // Check time periods
      if (paymentDay >= today) {
        this.paymentCounts.today.count++;
        this.paymentCounts.today.sum += amount;
      }
      if (paymentDay >= startOfWeek) {
        this.paymentCounts.thisWeek.count++;
        this.paymentCounts.thisWeek.sum += amount;
      }
      if (paymentDay >= startOfMonth) {
        this.paymentCounts.thisMonth.count++;
        this.paymentCounts.thisMonth.sum += amount;
      }
      if (paymentDay >= startOfYear) {
        this.paymentCounts.thisYear.count++;
        this.paymentCounts.thisYear.sum += amount;
      }
    });
  }

  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to load dashboard data';
    this.notifyService.showError(errorMessage);
  }

  private handleOrdersResponseSuccess(orders: PharmacyOrderRequestDetails[]): void {
    this.currentOrders = orders;
    this.calculateOrderCounts();
  }

  private handlePharmacySalesSuccessResponse(sales: PharmacyOrderDetails[]): void {
    this.currentSales = sales;
    this.calculateSalesCounts();
  }

  private handlePharmacyPaymentsSuccessResponse(payments: PharmacyPaymentDetail[]): void {
    this.currentPayments = payments;
    this.calculatePaymentCounts();
  }

  requestToOrders(): void {
    this.router.navigate(["/orders"]);
  }

  requestToSales(): void {
    this.router.navigate(["/sales"]);
  }

  filterOrdersByStatus(status: string): void {
    this.router.navigate(['/orders'], { queryParams: { status } });
  }

  filterSalesByPeriod(period: string): void {
    let startDate: Date | null = null;
    const today = new Date();
    
    switch(period) {
      case 'Today':
        startDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        break;
      case 'ThisMonth':
        startDate = new Date(today.getFullYear(), today.getMonth(), 1);
        break;
      case 'ThisYear':
        startDate = new Date(today.getFullYear(), 0, 1);
        break;
      default: // 'All'
        startDate = null;
    }

    const queryParams: any = {};
    if (startDate) {
      queryParams.startDate = startDate.toISOString();
      queryParams.endDate = new Date().toISOString();
    }

    this.router.navigate(['/sales'], { queryParams });
  }

  filterPaymentsByPeriod(period: string): void {
    let startDate: Date | null = null;
    const today = new Date();
    const startOfWeek = new Date(today);
    startOfWeek.setDate(today.getDate() - today.getDay()); // Start of week (Sunday)
    
    switch(period) {
      case 'Today':
        startDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        break;
      case 'ThisWeek':
        startDate = startOfWeek;
        break;
      case 'ThisMonth':
        startDate = new Date(today.getFullYear(), today.getMonth(), 1);
        break;
      case 'ThisYear':
        startDate = new Date(today.getFullYear(), 0, 1);
        break;
      default: // 'All'
        startDate = null;
    }

    const queryParams: any = {};
    if (startDate) {
      queryParams.startDate = startDate.toISOString();
      queryParams.endDate = new Date().toISOString();
    }

    this.router.navigate(['/payments'], { queryParams });
  }
}