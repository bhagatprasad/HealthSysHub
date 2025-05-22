import { Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { IApplicationUser } from '../models/applicationuser';

@Injectable({
  providedIn: 'root'
})
export class AuditFieldsService {

  private applicationUser : IApplicationUser  | null = null;

  constructor(private accountService: AccountService) {
    const currentUser  = this.accountService.getCurrentUser ();
    if (currentUser ) {
      this.applicationUser  = currentUser ;
    }
  }
  appendAuditFields<T extends Record<string, any>>(obj: T): T {
    const now = new Date();
    (obj as any)['createdOn'] = (obj as any)['createdOn'] || now;
    (obj as any)['createdBy'] = (obj as any)['createdBy'] || (this.applicationUser ?.id || null); // Use fullName if available
    (obj as any)['modifiedOn'] = now; // Always update modifiedOn
    (obj as any)['modifiedBy'] = this.applicationUser ?.id || null; // Use fullName if available
    (obj as any)['isActive'] = (obj as any)['isActive'] !== undefined ? (obj as any)['isActive'] : true; // Set to true if not defined
    (obj as any)['pharmacyId'] = this.applicationUser ?.pharmacyId || null; // Use pharmacyId if available
    return obj;
  }
}
