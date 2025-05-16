import { Component, Inject, OnDestroy, OnInit, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { TopmenuComponent } from './layout/topmenu/topmenu.component';
import { SidemenuComponent } from './layout/sidemenu/sidemenu.component';
import { LoaderComponent } from './shared/loader/loader.component';
import { AccountService } from './services/account.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, TopmenuComponent, SidemenuComponent, LoaderComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'Smaya-Sales';
  isBrowser: boolean;
  private authSubscription?: Subscription;

  constructor(
    public accountService: AccountService,
    private router: Router,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  ngOnInit() {
    if (!this.isBrowser) return;

    // Immediate check before subscribing
    const isInitiallyAuthenticated = this.accountService.checkInitialAuthState();
    const isLoginPage = this.router.url.startsWith('/login');

    if (isInitiallyAuthenticated && isLoginPage) {
      this.router.navigate(['/landing']);
      return;
    }

    if (!isInitiallyAuthenticated && !isLoginPage) {
      this.router.navigate(['/login']);
      return;
    }

    // Subscribe to future changes
    this.authSubscription = this.accountService.authenticationState$.subscribe({
      next: (isAuthenticated) => {
        const currentIsLoginPage = this.router.url.startsWith('/login');
        
        if (isAuthenticated && currentIsLoginPage) {
          this.router.navigate(['/landing']);
        } else if (!isAuthenticated && !currentIsLoginPage) {
          this.router.navigate(['/login']);
        }
      },
      error: (err) => {
        console.error('Authentication state error:', err);
        this.accountService.clearUserSession();
      }
    });
  }

  ngOnDestroy() {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }
}