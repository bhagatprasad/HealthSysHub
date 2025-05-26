import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AdminComponent } from './dashboard/admin/admin.component';
import { LoginComponent } from './shared/login/login.component';
import { SignupComponent } from './shared/signup/signup.component';
import { ForgotpasswordComponent } from './shared/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from './shared/resetpassword/resetpassword.component';
import { NotfoundComponent } from './shared/notfound/notfound.component';
import { SidemenuComponent } from './layout/sidemenu/sidemenu.component';
import { TopmenuComponent } from './layout/topmenu/topmenu.component';
import { SharedModule } from './services/shared.module';
import { PasswordToggleDirective } from './directives/password.toggle';
import { FormValidatorDirective } from './directives/form.validator';
import { LoaderComponent } from './shared/loader/loader.component';
import { apiInterceptor } from './intercepters/api.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    LoginComponent,
    SignupComponent,
    ForgotpasswordComponent,
    ResetpasswordComponent,
    NotfoundComponent,
    SidemenuComponent,
    TopmenuComponent,
    PasswordToggleDirective,
    FormValidatorDirective,
    LoaderComponent,
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      closeButton: true,
      progressBar: true,
    }),
    SharedModule,
  ],
  providers: [
    // Remove ApiService if you're using direct HttpClient
    provideHttpClient(
      withInterceptors([
        apiInterceptor,
      ])
    ),
  ],
  bootstrap: [AppComponent],
  schemas: [NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    LoaderComponent
  ]
})
export class AppModule { }