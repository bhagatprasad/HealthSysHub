import { Component } from '@angular/core';
import { Router } from '@angular/router'; // Import Router for navigation
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-sidemenu',
  standalone: true,
  templateUrl: './sidemenu.component.html',
  styleUrls: ['./sidemenu.component.css']
})
export class SidemenuComponent {

  constructor(
    private accountService: AccountService,
    private router: Router
  ) {}

  requestToLogOut() {
    this.accountService.clearUserSession(); // Clear the user session
    this.router.navigate(['/login']); // Navigate to login page
  }
   toggleSidebar() {
    const sidebar = document.querySelector('.sidebar-offcanvas');
    if (sidebar) {
      sidebar.classList.toggle('active');
    }
  }
}