import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../services/notification.service';
import { AuditFieldsService } from '../services/audit-fields.service';
import { PharmacyOrderRequestService } from '../services/pharmacy-order-request-service';
import { PharmacyOrderRequestDetails } from '../models/pharmacyorderrequestdetails';
import { AccountService } from '../services/account.service';
import { Pharmacy } from '../models/pharmacy';
import { CommonModule, DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { ModalComponent } from '../shared/popups/modal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProcessPharmacyOrderRequest } from '../models/processpharmacyorderrequest';
import { ProcessPharmacyOrderRequestResponse } from '../models/processpharmacyorderrequestresponse';

@Component({
  selector: 'app-order-request-list',
  standalone: true,
  imports: [DatePipe, CommonModule, ModalComponent, ReactiveFormsModule],
  templateUrl: './order-request-list.component.html',
  styleUrls: ['./order-request-list.component.css']
})
export class OrderRequestListComponent implements OnInit {
  pharmacyOrderRequestDetails: PharmacyOrderRequestDetails[] = [];
  currentPharmacy: Pharmacy | null = null;
  showModal = false;
  modalTitle: string = '';
  modalMessage: string = '';
  currentRequest: PharmacyOrderRequestDetails | null = null;
  actionType: string = '';

  constructor(
    private notifyService: NotificationService,
    private auditService: AuditFieldsService,
    private pharmacyOrderRequestService: PharmacyOrderRequestService,
    private accountService: AccountService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const currentPharmacyDetails = this.accountService.getCurrentApplicationUserPharmacy();
    if (currentPharmacyDetails) {
      this.currentPharmacy = currentPharmacyDetails;
      this.loadPharmacyOrderRequests(this.currentPharmacy.pharmacyId);
    }
  }

  private loadPharmacyOrderRequests(pharmacyId: string): void {
    this.pharmacyOrderRequestService.GetPharmacyOrderRequestsByPharmacyAsync(pharmacyId).subscribe({
      next: (response) => this.pharmacyOrderRequestDetails = response,
      error: (error) => {
        console.error('Error:', error);
        this.notifyService.showError(error?.message || 'Failed to fetch orders');
      }
    });
  }

  openModal(request: PharmacyOrderRequestDetails, action: string): void {
    this.currentRequest = request;
    this.actionType = action;
    this.modalTitle = action === 'Approved' ? 'Approve Order Request' : 'Cancel Order Request';
    this.modalMessage = `Are you sure you want to ${action} this order request?`;
    this.showModal = true;
  }

  handleConfirm(notes: string): void {
    const processOrderRequest: ProcessPharmacyOrderRequest = {
      pharmacyOrderRequestId: this.currentRequest?.pharmacyOrderRequestId,
      status: this.actionType,
      notes: notes
    };
    var processPharmacyOrderRequest = this.auditService.appendAuditFields(processOrderRequest);
    console.log(JSON.stringify(processPharmacyOrderRequest));
    this.pharmacyOrderRequestService.ProcessPharmacyOrderRequestAsync(processPharmacyOrderRequest).subscribe({
      next: (response) => this.handleProcessPharmacyOrderRequest(response),
      error: (error) => {
        console.error('Error:', error);
        this.notifyService.showError(error?.message || 'Failed to process orders');
      }
    })

  }
  private handleProcessPharmacyOrderRequest(processPharmacyOrderRequestResponse: ProcessPharmacyOrderRequestResponse): void {
    this.modalTitle = '';
    this.modalMessage = '';
    this.currentRequest = null;
    this.actionType = '';
    this.showModal = false;
    this.notifyService.showSuccess(processPharmacyOrderRequestResponse.message);
    if (this.currentPharmacy)
      this.loadPharmacyOrderRequests(this.currentPharmacy.pharmacyId);
  }

  requestToAddOrder(): void {
    this.router.navigate(['/addorder']);
  }

  requestGetOrderDetails(request: PharmacyOrderRequestDetails): void {
    // Implement your details viewing logic here
    // this.router.navigate(['/order-details', request.id]);
  }

  requestToSales(): void {
    this.router.navigate(['/sales']);
  }
}