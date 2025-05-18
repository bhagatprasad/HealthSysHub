import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";
import { Observable } from "rxjs";
import { PharmacyStaff } from "../models/pharmacystaff";
import { environment } from "../../environment";

@Injectable({
    providedIn: "root"
})
export class PharmacyStaffService {
    constructor(private apiServices: ApiService) { }

    GetPharmacyStaffByharmacyAsync(pharmacyId?: string): Observable<PharmacyStaff[]> {
        const url = `${environment.UrlConstants.GetPharmacyStaffByharmacyAsync}/${pharmacyId}`;
        return this.apiServices.send<PharmacyStaff[]>("GET", url);
    }
    InsertOrUpdatePharmacyStaffAsync(pharmacystaff: PharmacyStaff): Observable<PharmacyStaff> {
        return this.apiServices.send<PharmacyStaff>("POST", environment.UrlConstants.InsertOrUpdatePharmacyStaffAsync, pharmacystaff);
    }
}