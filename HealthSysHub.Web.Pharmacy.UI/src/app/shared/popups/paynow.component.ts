import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PharmacyPayment } from '../../models/pharmacy-payment';
import { PharmacyOrderDetails } from '../../models/pharmacy-order-details';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Pharmacy } from '../../models/pharmacy';
import { AccountService } from '../../services/account.service';

@Component({
    selector: 'app-paynow',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './paynow.component.html',
    styleUrls: ['./paynow.component.css']
})
export class PaynowComponent {
    paymentForm: FormGroup;
    showTransactionNumberField: boolean = false;
    @Input() isOpen = false;
    private _pharmacyOrderDetails: PharmacyOrderDetails | null = null;

    @Input()
    set pharmacyOrderDetails(value: PharmacyOrderDetails | null) {
        this._pharmacyOrderDetails = value;
        if (value) {
            this.patchForm(value);
        }
    }

    get pharmacyOrderDetails(): PharmacyOrderDetails | null {
        return this._pharmacyOrderDetails;
    }

    @Output() confirmed = new EventEmitter<PharmacyPayment>();
    @Output() closed = new EventEmitter<void>();

    constructor(private fb: FormBuilder, private accountService: AccountService) {
        this.paymentForm = this.fb.group({
            paymentMethod: ['', Validators.required],
            paymentAmount: [{ value: null, disabled: true }, [Validators.required, Validators.min(0)]],
            paymentNumber: [''],
            paymentNotes: ['']
        });

        // Listen to payment method changes
        this.paymentForm.get('paymentMethod')?.valueChanges.subscribe(method => {
            this.handlePaymentMethodChange(method);
        });
    }

    private patchForm(orderDetails: PharmacyOrderDetails): void {
        this.paymentForm.patchValue({
            paymentAmount: orderDetails.finalAmount
        });
    }

    private handlePaymentMethodChange(method: string): void {
        this.showTransactionNumberField = method !== 'cash';
        const paymentNumberControl = this.paymentForm.get('paymentNumber');

        if (method !== 'cash') {
            paymentNumberControl?.setValidators([Validators.required]);
        } else {
            paymentNumberControl?.clearValidators();
            paymentNumberControl?.reset();
        }
        paymentNumberControl?.updateValueAndValidity();
    }

    confirm(): void {
        if (this.paymentForm.valid) {
            const paymentData: PharmacyPayment = {
                paymentAmount: this.pharmacyOrderDetails?.finalAmount ? this.pharmacyOrderDetails?.finalAmount : 0,
                paymentMethod: this.paymentForm.value.paymentMethod,
                paymentNumber: this.paymentForm.value.paymentNumber,
                gatewayResponse: "",
                hospitalId: "",
                notes: this.paymentForm.value.paymentNotes,
                paymentGateway: "",
                pharmacyOrderId: this.pharmacyOrderDetails?.pharmacyOrderId,
                status: "Completed",
                paymentDate: new Date().toISOString(),
                isActive: true,
                createdOn: new Date().toISOString()
            };
            this.confirmed.emit(paymentData);
            this.close();
        }
    }

    close(): void {
        this.paymentForm.reset();
        this.closed.emit();
        this.isOpen = false;
    }
}