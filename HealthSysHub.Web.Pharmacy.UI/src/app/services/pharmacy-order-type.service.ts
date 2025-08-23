import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";
import { PharmacyOrderType } from "../models/pharmacy-orderType";
import { Observable } from "rxjs";
import { environment } from "../../environment";

@Injectable({
    providedIn: 'root'
})
export class PharmacyOrderTypeService {
    constructor(private apiService: ApiService) { }

    GetPharmacyOrderTypesAsync(): Observable<PharmacyOrderType[]> {
        const url = `${environment.UrlConstants.GetPharmacyOrderTypesAsync}`;
        return this.apiService.send<PharmacyOrderType[]>("GET", url);
    }
    GetPharmacyOrderTypeByIdAsync(pharmacyOrderTypeId: string): Observable<PharmacyOrderType> {
        const url = `${environment.UrlConstants.GetPharmacyOrderTypeByIdAsync}/${pharmacyOrderTypeId}`;
        return this.apiService.send<PharmacyOrderType>("GET", url);
    }
    InsertOrUpdatePharmacyOrderTypeAsync(pharmacyOrderType: PharmacyOrderType): Observable<PharmacyOrderType> {
        return this.apiService.send<PharmacyOrderType>("POST", environment.UrlConstants.InsertOrUpdatePharmacyOrderTypeAsync, pharmacyOrderType);
    }

}