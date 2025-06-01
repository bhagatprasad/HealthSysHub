import { Component, OnInit } from '@angular/core';
import { PharmacyMedicineService } from '../services/pharmacy-medicine-service';
import { PharmacyOrderRequestService } from '../services/pharmacy-order-request-service';
import { AccountService } from '../services/account.service';
import { AuditFieldsService } from '../services/audit-fields.service';
import { NotificationService } from '../services/notification.service';
import { PharmacyMedicine } from '../models/pharmacymedicine';
import { HospitalService } from '../services/hospital.service';
import { Pharmacy } from '../models/pharmacy';
import { forkJoin } from 'rxjs';
import { PharmacyOrderRequestDetails } from '../models/pharmacyorderrequestdetails';
import { PharmacyOrderRequestItemDetails } from '../models/pharmacyorderrequestItemdetails';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HospitalInformation } from '../models/hospital-information';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-order',
  imports: [CurrencyPipe, CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './add-order.component.html',
  styleUrl: './add-order.component.css'
})
export class AddOrderComponent implements OnInit {

  currentPharmacy: Pharmacy | null = null;
  pharmacyMedicines: PharmacyMedicine[] = [];
  hospitalInformation: HospitalInformation[] = [];
  filteredMedicines: PharmacyMedicine[] = [];

  searchTerm: string = "";

  orderRequest: PharmacyOrderRequestDetails = {
    pharmacyId: '',
    name: '',
    phone: '',
    hospitalId: '',
    doctorName: '',
    notes: '',
    isActive: true,
    status: 'Pending',
    pharmacyOrderRequestItemDetails: []
  };
  constructor(private pharmacyMedicineService: PharmacyMedicineService,
    private orderRequestService: PharmacyOrderRequestService,
    private hospitalService: HospitalService,
    private accountService: AccountService,
    private auditService: AuditFieldsService,
    private notifyService: NotificationService,
    private router: Router

  ) { }
  ngOnInit(): void {
    var currentPharmacyDetails = this.accountService.getCurrentApplicationUserPharmacy();
    if (currentPharmacyDetails) {
      this.currentPharmacy = currentPharmacyDetails;
      this.loadPharmacyMedicineAndHospitalDetails(this.currentPharmacy.pharmacyId);
      this.initializeOrderRequest();
    }
  }
  private loadPharmacyMedicineAndHospitalDetails(pharmacyId: string) {
    forkJoin([this.hospitalService.GetHospitalInformationsAsync(), this.pharmacyMedicineService.GetPharmacyMedicineByPharmacyAsync(pharmacyId)])
      .subscribe(
        result => { this.handleHospitalResponseSuccess(result[0]); this.handlePharmacyMedicineSuccessResponse(result[1]); },
        error => this.handleAuthError(error));
  }
  private handleHospitalResponseSuccess(hospitalInformation: HospitalInformation[]): void {
    this.hospitalInformation = hospitalInformation;
    console.log(hospitalInformation);
  }
  private handlePharmacyMedicineSuccessResponse(pharmacyMedicine: PharmacyMedicine[]): void {
    this.pharmacyMedicines = pharmacyMedicine;
    this.filteredMedicines = [...pharmacyMedicine]; // Initialize filtered list with all medicines
    console.log(pharmacyMedicine);
  }
  private handleAuthError(error: Error | any): void {
    console.error('Error:', error);
    const errorMessage = error?.message || 'Failed to fetch orders';
    this.notifyService.showError(errorMessage);
  }

  requestToOrders(): void {
    this.router.navigate(["/orders"]);
  }
  // Initialize an empty cart/order request
  initializeOrderRequest(): void {
    this.orderRequest = {
      pharmacyId: this.currentPharmacy?.pharmacyId || '',
      name: '',
      phone: '',
      hospitalId: '',
      doctorName: '',
      notes: '',
      status: 'Pending', // Default status
      isActive: true,
      pharmacyOrderRequestItemDetails: []
    };
  }

  // Add medicine to cart
  addToCart(medicine: PharmacyMedicine): void {
    // Check if medicine already exists in cart
    const existingItemIndex = this.orderRequest.pharmacyOrderRequestItemDetails.findIndex(
      item => item.medicineId === medicine.medicineId
    );

    if (existingItemIndex >= 0) {
      // If exists, increase quantity if stock allows
      this.increaseQuantity(existingItemIndex);
    } else {
      // If new, add to cart with quantity 1
      const newItem: PharmacyOrderRequestItemDetails = {
        medicineId: medicine.medicineId,
        medicineName: medicine.medicineName ? medicine.medicineName : "",
        itemQty: 1,
        usage: '',
        isActive: true,
        hospitalId: this.orderRequest.hospitalId
      };
      this.orderRequest.pharmacyOrderRequestItemDetails.push(this.auditService.appendAuditFields(newItem));
      console.log(this.orderRequest);
      this.notifyService.showSuccess(`${medicine.medicineName} added to cart`);
    }
  }

  // Remove item from cart
  removeItem(index: number): void {
    const removedItem = this.orderRequest.pharmacyOrderRequestItemDetails[index];
    this.orderRequest.pharmacyOrderRequestItemDetails.splice(index, 1);
    this.notifyService.showWarning(`${removedItem.medicineName} removed from cart`);
  }

  // Increase item quantity
  increaseQuantity(index: number): void {
    const item = this.orderRequest.pharmacyOrderRequestItemDetails[index];
    const medicine = this.pharmacyMedicines.find(m => m.medicineId === item.medicineId);

    if (!medicine) return;

    if (item.itemQty && item.itemQty < medicine.stockQuantity) {
      item.itemQty++;
    } else {
      this.notifyService.showWarning(`Cannot exceed available stock (${medicine.stockQuantity})`);
    }
  }

  // Decrease item quantity
  decreaseQuantity(index: number): void {
    const item = this.orderRequest.pharmacyOrderRequestItemDetails[index];
    if (item.itemQty && item.itemQty > 1) {
      item.itemQty--;
    } else {
      // If quantity would go to 0, remove the item instead
      this.removeItem(index);
    }
  }

  // Get maximum allowed quantity for a medicine
  getMaxQuantity(medicineId: string | undefined): number {
    if (!medicineId) return 0;
    const medicine = this.pharmacyMedicines.find(m => m.medicineId === medicineId);
    return medicine ? medicine.stockQuantity : 0;
  }

  // Calculate total items in cart
  getTotalItems(): number {
    return this.orderRequest.pharmacyOrderRequestItemDetails.length;
  }

  // Calculate total quantity of all items
  getTotalQuantity(): number {
    return this.orderRequest.pharmacyOrderRequestItemDetails.reduce(
      (total, item) => total + (item.itemQty || 0), 0
    );
  }

  // Calculate total price (if you have price information)
  getTotalPrice(): number {
    return this.orderRequest.pharmacyOrderRequestItemDetails.reduce((total, item) => {
      const medicine = this.pharmacyMedicines.find(m => m.medicineId === item.medicineId);
      return total + ((item.itemQty || 0) * (medicine?.pricePerUnit || 0));
    }, 0);
  }

  // Clear the cart
  clearCart(): void {
    this.orderRequest.pharmacyOrderRequestItemDetails = [];
    this.notifyService.showInfo('Cart cleared');
  }

  // Check if medicine is already in cart
  isInCart(medicineId: string): boolean {
    return this.orderRequest.pharmacyOrderRequestItemDetails.some(
      item => item.medicineId === medicineId
    );
  }

  // Update hospital for all items when hospital selection changes
  updateHospitalForItems(hospitalId: string): void {
    this.orderRequest.pharmacyOrderRequestItemDetails.forEach(item => {
      item.hospitalId = hospitalId;
    });
  }

  // Submit the order
  submitOrder(): void {
    if (!this.validateOrder()) return;

    // Set createdBy and other audit fields

    var _orderRequest = this.auditService.appendAuditFields(this.orderRequest);


    this.orderRequestService.InsertOrUpdatePharmacyOrderRequestAsync(_orderRequest).subscribe({
      next: (response) => {
        this.notifyService.showSuccess('Order submitted successfully');
        this.router.navigate(["/orders"]);
        this.initializeOrderRequest();
      },
      error: (error) => {
        console.error('Error submitting order:', error);
        this.notifyService.showError('Failed to submit order');
      }
    });
  }

  // Validate the order before submission
  private validateOrder(): boolean {
    if (!this.orderRequest.name) {
      this.notifyService.showWarning('Please enter patient name');
      return false;
    }

    if (!this.orderRequest.phone) {
      this.notifyService.showWarning('Please enter phone number');
      return false;
    }

    // if (!this.orderRequest.hospitalId) {
    //   this.notifyService.showWarning('Please select a hospital');
    //   return false;
    // }

    if (this.orderRequest.pharmacyOrderRequestItemDetails.length === 0) {
      this.notifyService.showWarning('Please add at least one medicine to the order');
      return false;
    }

    return true;
  }

  filterMedicines(): void {
    if (!this.searchTerm) {
      this.filteredMedicines = [...this.pharmacyMedicines];
      return;
    }

    const searchTermLower = this.searchTerm.toLowerCase();
    this.filteredMedicines = this.pharmacyMedicines.filter(medicine =>
      medicine.medicineName?.toLowerCase().includes(searchTermLower)
    );
  }
}
