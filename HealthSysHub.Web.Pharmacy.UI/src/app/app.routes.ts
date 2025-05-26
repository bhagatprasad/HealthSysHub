// app.routes.ts
import { Routes } from '@angular/router';
import { NotfoundComponent } from './shared/notfound/notfound.component';
import { AdminComponent } from './dashboard/admin/admin.component';
import { SignupComponent } from './shared/signup/signup.component';
import { ForgotpasswordComponent } from './shared/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from './shared/resetpassword/resetpassword.component';
import { AuthGuard } from './gurds/auth.guard';
import { OrdersListComponent } from './orders/list.component';
import { SalesListComponent } from './sales/list.component';
import { UpdateAccountComponent } from './setting/update-account.component';
import { PasswordChangeComponent } from './setting/password-change.component';
import { UsersListComponent } from './users/list.component';
import { MedicinesListComponent } from './medicines/medicines-list.component';
import { LoginComponent } from './shared/login/login.component';
import { DetailsComponent } from './pharmcy/details.component';
import { OrderRequestListComponent } from './orders/order-request-list.component';
import { AddOrderComponent } from './pos/add-order.component';

export const routes: Routes = [
  { 
    path: 'landing', 
    component: AdminComponent,
    canActivate: [AuthGuard] 
  },
   { 
    path: 'sales', 
    component: SalesListComponent,
    canActivate: [AuthGuard] 
  },
   { 
    path: 'settings', 
    component: UpdateAccountComponent,
    canActivate: [AuthGuard] 
  },
   { 
    path: 'changepassword', 
    component: PasswordChangeComponent,
    canActivate: [AuthGuard] 
  },
   { 
    path: 'users', 
    component: UsersListComponent,
    canActivate: [AuthGuard] 
  },
   { 
    path: 'medicines', 
    component: MedicinesListComponent,
    canActivate: [AuthGuard] 
  },
  { 
    path: 'pharmcy', 
    component: DetailsComponent,
    canActivate: [AuthGuard] 
  },
  { 
    path: 'orders', 
    component: OrderRequestListComponent,
    canActivate: [AuthGuard] 
  },
  { 
    path: 'addorder', 
    component: AddOrderComponent,
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