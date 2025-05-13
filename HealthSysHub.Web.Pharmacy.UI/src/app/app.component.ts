import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidemenuComponent } from './layout/sidemenu/sidemenu.component';
import { TopmenuComponent } from './layout/topmenu/topmenu.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,SidemenuComponent,TopmenuComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent { }
