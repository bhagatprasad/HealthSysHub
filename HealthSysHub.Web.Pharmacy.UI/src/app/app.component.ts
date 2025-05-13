import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
   selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],  // ✅ Valid in standalone components
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'HealthSysHub.Web.Pharmacy.UI';
}
