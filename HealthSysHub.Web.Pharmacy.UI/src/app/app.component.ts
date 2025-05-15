import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { TopmenuComponent } from './layout/topmenu/topmenu.component';
import { SidemenuComponent } from './layout/sidemenu/sidemenu.component';
import { LoaderComponent } from './shared/loader/loader.component';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, TopmenuComponent, SidemenuComponent, LoaderComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Smaya-Sales';
  isAuthenticated = false;
  isBrowser: boolean;

  constructor(
    private accountService: AccountService,
    private router: Router,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  ngOnInit() {
    if (!this.isBrowser) return;

    this.accountService.authenticationState$.subscribe({
      next: (isAuthenticated) => {
        this.isAuthenticated = isAuthenticated;
        if (!isAuthenticated && this.router.url !== '/login') {
          this.router.navigate(['/login']);
        }
      },
      error: (err) => {
        console.error('Authentication state error:', err);
        this.isAuthenticated = false;
      }
    });
  }
}