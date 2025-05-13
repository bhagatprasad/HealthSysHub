import { BrowserModule } from '@angular/platform-browser';
import { SidemenuComponent } from './layout/sidemenu/sidemenu.component';
import { TopmenuComponent } from './layout/topmenu/topmenu.component';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routes';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    // No need to declare standalone components here
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
    HttpClientModule,
    SidemenuComponent, // If they are standalone, this is correct
    TopmenuComponent    // If they are standalone, this is correct
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
