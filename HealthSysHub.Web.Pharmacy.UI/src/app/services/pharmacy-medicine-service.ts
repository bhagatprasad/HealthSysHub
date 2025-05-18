import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";
import { Observable } from "rxjs";
import { PharmacyMedicine } from "../models/pharmacymedicine";
import { environment } from "../../environment";

@Injectable({
    providedIn: 'root'
})
export class PharmacyMedicineService {
    constructor(private apiService: ApiService) { }

    GetPharmacyMedicineByPharmacyAsync(pharmacyId: string): Observable<PharmacyMedicine[]> {
        const url = `${environment.UrlConstants.GetPharmacyMedicineByPharmacyAsync}/${pharmacyId}`;
        return this.apiService.send<PharmacyMedicine[]>("GET", url);
    }

    GetPharmacyMedicineByMedicineIdAsync(medicineId: string): Observable<PharmacyMedicine> {
        const url = `${environment.UrlConstants.GetPharmacyMedicineByMedicineIdAsync}/${medicineId}`;
        return this.apiService.send<PharmacyMedicine>("GET", url);
    }

    InsertOrUpdatePharmacyMedicineAsync(pharmacyMedicine: PharmacyMedicine): Observable<PharmacyMedicine> {
        return this.apiService.send<PharmacyMedicine>('POST', environment.UrlConstants.InsertOrUpdatePharmacyMedicineAsync, pharmacyMedicine);
    }
}