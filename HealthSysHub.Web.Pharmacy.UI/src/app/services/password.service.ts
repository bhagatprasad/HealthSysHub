import { Injectable } from "@angular/core";
import { ApiService } from "./apiservice.service";
import { ChangePassword } from "../models/changepassword";
import { Observable } from "rxjs";
import { ChangePasswordResult } from "../models/changepasswordresult";
import { environment } from "../../environment";

@Injectable({
    providedIn: 'root'
})
export class PasswordService {
    constructor(private apiService: ApiService) { }

    changePasswordAsync(changePassword: ChangePassword): Observable<ChangePasswordResult> {
        return this.apiService.send<ChangePasswordResult>("POST", environment.UrlConstants.ChangePasswordAsync, changePassword);
    }
}