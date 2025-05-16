// app.routes.ts
import { Routes } from '@angular/router';
import { NotfoundComponent } from './shared/notfound/notfound.component';
import { AdminComponent } from './dashboard/admin/admin.component';
import { LoginComponent } from './shared/login/login.component';
import { SignupComponent } from './shared/signup/signup.component';
import { ForgotpasswordComponent } from './shared/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from './shared/resetpassword/resetpassword.component';
import { AuthGuard } from './gurds/auth.guard';

export const routes: Routes = [
  { 
    path: 'landing', 
    component: AdminComponent,
    canActivate: [AuthGuard] 
  },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'forgotpassword', component: ForgotpasswordComponent },
  { path: 'resetpassword', component: ResetpasswordComponent },
  { 
    path: '', 
    redirectTo: 'login', 
    pathMatch: 'full' 
  },
  { 
    path: '**', 
    component: NotfoundComponent,
    canActivate: [AuthGuard] 
  }
];