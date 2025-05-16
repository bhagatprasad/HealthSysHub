// auth.guard.ts
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { map, Observable, take } from 'rxjs';
import { AccountService } from '../services/account.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  canActivate(): Observable<boolean> {
    return this.accountService.authenticationState$.pipe(
      take(1),
      map(isAuthenticated => {
        if (!isAuthenticated) {
          this.router.navigate(['/login']);
          return false;
        }
        return true;
      })
    );
  }
}