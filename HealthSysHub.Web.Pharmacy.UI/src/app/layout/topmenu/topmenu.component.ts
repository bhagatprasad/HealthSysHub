import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router'; // Corrected import
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-topmenu',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink], // Added required imports
  templateUrl: './topmenu.component.html',
  styleUrls: ['./topmenu.component.css']
})
export class TopmenuComponent implements OnInit {
  userImage = 'assets/images/faces/face1.jpg';
  userName = 'Loading...';
  userRole = 'Loading...';
  isAdmin = false;
  searchQuery = '';

  menuItems = [
    { title: 'Orders', icon: 'mdi-cart-outline', link: '/orders' },
    { title: 'Sales', icon: 'mdi-chart-line', link: '/sales' },
  ];

  constructor(private router: Router, private accountService: AccountService) { }

  ngOnInit() {
    this.loadUserData();

    // Add admin-specific items if needed
    // if (this.isAdmin) {
    //   this.menuItems.splice(2, 0,
    //     { title: 'User/Staff', icon: 'mdi-account-group', link: '/users' },
    //     { title: 'Medicines', icon: 'mdi-pill', link: '/medicines' }
    //   );
    // }
  }

  loadUserData() {
    const userData = localStorage.getItem('ApplicationUser');
    if (userData) {
      try {
        const user = JSON.parse(userData);
        this.userName = user.fullName || 'User';
        this.userRole = user.roleName || 'Staff';
        this.isAdmin = user.roleId.toUpperCase() === '971ECB66-1CFC-43F2-AB4E-0F352A0F9354';

        if (user.profileImage) {
          this.userImage = user.profileImage;
        }
      } catch (e) {
        console.error('Error parsing user data', e);
      }
    }
  }

  search(event: Event) {
    event.preventDefault();
    if (this.searchQuery.trim()) {
      this.router.navigate(['/search'], { queryParams: { q: this.searchQuery } });
    }
  }
  requestToLogOut(): void {
    this.accountService.clearUserSession(); // Clear the user session
    this.router.navigate(['/login']); // Navigate to login page
  }
}