import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";

@Injectable({
    providedIn: "root"
})
export class PharmacyMedicineService {
    constructor(private apiService: ApiService) { }
}