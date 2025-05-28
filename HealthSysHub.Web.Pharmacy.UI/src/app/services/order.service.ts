import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";
import { Observable } from "rxjs";
import { PharmacyOrderDetails } from "../models/pharmacy-order-details";
import { environment } from "../../environment";
import { PharmacyOrdersProcessRequest } from "../models/pharmacy-orders-process-request";
import { PharmacyOrdersProcessResponse } from "../models/pharmacy-orders-process-response";

@Injectable({
    providedIn: 'root'
})
export class OrderService {

    constructor(private apiService: ApiService) { }

    GetPharmacyOrdersListByPharmacyAsync(pharmacyId: string): Observable<PharmacyOrderDetails[]> {
        const url = `${environment.UrlConstants.GetPharmacyOrdersListByPharmacyAsync}/${pharmacyId}`;
        return this.apiService.send<PharmacyOrderDetails[]>("GET", url);
    }
    
    ProcessPharmacyOrdersRequestAsync(pharmacyOrdersProcessRequest: PharmacyOrdersProcessRequest): Observable<PharmacyOrdersProcessResponse> {
        return this.apiService.send<PharmacyOrdersProcessResponse>('POST', environment.UrlConstants.ProcessPharmacyOrdersRequestAsync, pharmacyOrdersProcessRequest);
    }
}