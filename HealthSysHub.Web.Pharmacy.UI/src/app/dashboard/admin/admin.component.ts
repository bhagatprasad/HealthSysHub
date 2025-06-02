import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {

  constructor(private router: Router) { }

  requestToOrders(): void {
    this.router.navigate(["/orders"]);
  }
  requestToSales(): void {
    this.router.navigate(["/sales"]);
  }
}