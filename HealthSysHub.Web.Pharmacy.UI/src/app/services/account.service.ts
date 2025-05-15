import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environment';
import { IAuthResponse } from '../models/authresponse';
import { ApiService } from './apiservice.service';
import { IUserAuthentication } from '../models/userauthentication';
import { IApplicationUser } from '../models/applicationuser';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly authEndpoint = environment.UrlConstants.Authenticate;
  private readonly claimsEndpoint = environment.UrlConstants.GenerateUserCliams;

  constructor(private apiService: ApiService) { }

  authenticateUser(userAuthentication: IUserAuthentication): Observable<IAuthResponse> {
    return this.apiService.send<IAuthResponse>('POST', this.authEndpoint, userAuthentication);
  }

  generateUserClaims(authResponse: IAuthResponse): Observable<IApplicationUser> {
    return this.apiService.send<IApplicationUser>('POST', this.claimsEndpoint, authResponse);
  }

  storeUserSession(user: IApplicationUser, token: string): void {
    localStorage.removeItem('ApplicationUser');
    localStorage.removeItem('AccessToken');
    localStorage.setItem('ApplicationUser', JSON.stringify(user));
    localStorage.setItem('AccessToken', token);
  }
}