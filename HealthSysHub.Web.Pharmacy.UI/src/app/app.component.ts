import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NotificationService } from './services/notification.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  /**
   *
   */
  constructor(private toster: NotificationService) {

  }
  ngOnInit(): void {
    this.toster.showSuccess("Success!", "Successfully processed the notification service")
  }
  title = 'HealthSysHub.Web.Pharmacy.UI';
}
