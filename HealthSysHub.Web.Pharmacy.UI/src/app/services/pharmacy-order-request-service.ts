import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";
import { Observable } from "rxjs";
import { environment } from "../../environment";
import { PharmacyOrderRequestDetails } from "../models/pharmacyorderrequestdetails";
import { ProcessPharmacyOrderRequest } from "../models/processpharmacyorderrequest";
import { ProcessPharmacyOrderRequestResponse } from "../models/processpharmacyorderrequestresponse";

@Injectable({
    providedIn: "root"
})
export class PharmacyOrderRequestService {
    constructor(private apiService: ApiService) { }

    GetPharmacyOrderRequestsByPharmacyAsync(pharmacyId: string): Observable<PharmacyOrderRequestDetails[]> {
        const url = `${environment.UrlConstants.GetPharmacyOrderRequestsByPharmacyAsync}/${pharmacyId}`;
        return this.apiService.send<PharmacyOrderRequestDetails[]>("GET", url);
    }
    GetPharmacyOrderRequestsByHospitalAsync(hospitalId: string): Observable<PharmacyOrderRequestDetails[]> {
        const url = `${environment.UrlConstants.GetPharmacyOrderRequestsByHospitalAsync}/${hospitalId}`;
        return this.apiService.send<PharmacyOrderRequestDetails[]>("GET", url);
    }
    GetPharmacyOrderRequestsByPatientAsync(patientId: string): Observable<PharmacyOrderRequestDetails[]> {
        const url = `${environment.UrlConstants.GetPharmacyOrderRequestsByPatientAsync}/${patientId}`;
        return this.apiService.send<PharmacyOrderRequestDetails[]>("GET", url);
    }
    GetPharmacyOrderRequestDetailAsync(pharmacyOrderRequestId: string): Observable<PharmacyOrderRequestDetails> {
        const url = `${environment.UrlConstants.GetPharmacyOrderRequestDetailAsync}/${pharmacyOrderRequestId}`;
        return this.apiService.send<PharmacyOrderRequestDetails>("GET", url);
    }
    InsertOrUpdatePharmacyOrderRequestAsync(pharmacyOrderReques: PharmacyOrderRequestDetails): Observable<PharmacyOrderRequestDetails> {
        return this.apiService.send<PharmacyOrderRequestDetails>('POST', environment.UrlConstants.InsertOrUpdatePharmacyOrderRequestAsync, pharmacyOrderReques);
    }
    ProcessPharmacyOrderRequestAsync(requestDetails: ProcessPharmacyOrderRequest): Observable<ProcessPharmacyOrderRequestResponse> {
        return this.apiService.send<ProcessPharmacyOrderRequestResponse>('POST', environment.UrlConstants.ProcessPharmacyOrderRequestAsync, requestDetails);
    }
}