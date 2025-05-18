import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Pharmacy } from "../models/pharmacy";
import { ApiService } from "./apiservice.service";
import { environment } from "../../environment";

@Injectable({
    providedIn: 'root'
})
export class PharmacyService {
    constructor(private apiService: ApiService) { }

    GetPharmacyByIdAsync(pharmacyId: string): Observable<Pharmacy> {
        const url = `${environment.UrlConstants.GetPharmacyByIdAsync}/${pharmacyId}`;
        return this.apiService.send<Pharmacy>("GET", url);
    }

    InsertOrUpdatePharmacyAsync(pharmacy: Pharmacy): Observable<Pharmacy> {
        return this.apiService.send<Pharmacy>('POST', environment.UrlConstants.InsertOrUpdatePharmacyAsync, pharmacy);
      }
    
}