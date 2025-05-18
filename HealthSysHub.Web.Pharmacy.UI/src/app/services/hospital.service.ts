import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";
import { Observable } from "rxjs";
import { Hospital } from "../models/hospital";
import { environment } from "../../environment";
import { HospitalInformation } from "../models/hospital-information";

@Injectable({
    providedIn: 'root'
})
export class HospitalService {

    constructor(private apiService: ApiService) {
    }

    GetHospitalsAsync(): Observable<Hospital[]> {
        return this.apiService.send<Hospital[]>('GET', environment.UrlConstants.GetHospitalsAsync);
    }
    GetHospitalInformationsAsync(): Observable<HospitalInformation[]> {
        return this.apiService.send<HospitalInformation[]>('GET', environment.UrlConstants.GetHospitalInformationsAsync);
    }
    GetHospitalByIdAsync(hospitalId: string): Observable<Hospital> {
        const url = `${environment.UrlConstants.GetHospitalByIdAsync}/${hospitalId}`;
        return this.apiService.send<Hospital>('GET', url);
    }
    GetHospitalInformationByIdAsync(hospitalId: string): Observable<HospitalInformation> {
        const url = `${environment.UrlConstants.GetHospitalInformationByIdAsync}/${hospitalId}`;
        return this.apiService.send<HospitalInformation>('GET', url);
    }

}