import { Component, Inject, OnDestroy, OnInit, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { TopmenuComponent } from './layout/topmenu/topmenu.component';
import { SidemenuComponent } from './layout/sidemenu/sidemenu.component';
import { LoaderComponent } from './shared/loader/loader.component';
import { AccountService } from './services/account.service';
import { Subscription, filter, take } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, TopmenuComponent, SidemenuComponent, LoaderComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'HealthSysHub';
  isBrowser: boolean;
  isSignUpRoute: boolean = false;
  private authSubscription?: Subscription;
  showContent = false; // Changed from isLoading to showContent for better semantics

  constructor(
    public accountService: AccountService,
    private router: Router,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  ngOnInit() {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.isSignUpRoute = event.url === '/signup'; // Check if the current route is '/signup'
      });

    if (!this.isBrowser) {
      this.showContent = true;
      return;
    }

    // First synchronous check
    const initialAuth = this.accountService.checkInitialAuthState();
    this.accountService.authenticationState.next(initialAuth);

    // Immediate redirect if authenticated and on login page
    if (initialAuth && this.isLoginPage()) {
      this.router.navigate([this.accountService.redirectUrl || '/landing']);
      return;
    }

    this.authSubscription = this.accountService.authenticationState$.pipe(
      filter(state => state !== null),
      take(1)
    ).subscribe({
      next: (isAuthenticated) => {
        this.handleAuthState(isAuthenticated);
        this.showContent = true;
      },
      error: (err) => {
        console.error('Authentication state error:', err);
        this.accountService.clearUserSession();
        this.showContent = true;
      }
    });
  }

  private isLoginPage(): boolean {
    return this.router.url.startsWith('/login');
  }

  private handleAuthState(isAuthenticated: boolean): void {
    if (isAuthenticated && this.isLoginPage()) {
      const redirectUrl = this.accountService.redirectUrl || '/landing';
      this.router.navigateByUrl(redirectUrl);
    } else if (!isAuthenticated && !this.isLoginPage()) {
      this.router.navigate(['/login']);
    }
  }

  ngOnDestroy() {
    this.authSubscription?.unsubscribe();
  }
}