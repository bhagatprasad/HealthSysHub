import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";
import { PharmacyPayment } from "../models/pharmacy-payment";
import { Observable } from "rxjs";
import { environment } from "../../environment";

@Injectable({
    providedIn: 'root'
})
export class PaymentService {
    constructor(private apiService: ApiService) { }

    ProcessPharmacyOrderPaymentAsync(pharmacyPayment: PharmacyPayment): Observable<PharmacyPayment> {
        return this.apiService.send<PharmacyPayment>('POST', environment.UrlConstants.ProcessPharmacyOrderPaymentAsync, pharmacyPayment);
    }
}