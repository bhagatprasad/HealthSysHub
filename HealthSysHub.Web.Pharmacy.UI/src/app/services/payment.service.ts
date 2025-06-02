import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";
import { PharmacyPayment } from "../models/pharmacy-payment";
import { Observable } from "rxjs";
import { environment } from "../../environment";
import { PharmacyPaymentDetail } from "../models/pharmacy-payment-detail";

@Injectable({
    providedIn: 'root'
})
export class PaymentService {
    constructor(private apiService: ApiService) { }

    ProcessPharmacyOrderPaymentAsync(pharmacyPayment: PharmacyPayment): Observable<PharmacyPayment> {
        return this.apiService.send<PharmacyPayment>('POST', environment.UrlConstants.ProcessPharmacyOrderPaymentAsync, pharmacyPayment);
    }

    GetPharmacyPaymentListAsync(pharmacyId: string): Observable<PharmacyPaymentDetail[]> {
        const url = `${environment.UrlConstants.GetPharmacyPaymentListAsync}/${pharmacyId}`;
        return this.apiService.send<PharmacyPaymentDetail[]>("GET", url);
    }

    GetPharmacyPaymentDetailAsync(pharmacyId: string, pharmacyOrderId: string): Observable<PharmacyPaymentDetail> {
        const url = `${environment.UrlConstants.GetPharmacyPaymentDetailAsync}/${pharmacyId}/${pharmacyOrderId}`;
        return this.apiService.send<PharmacyPaymentDetail>("GET", url);
    }
}