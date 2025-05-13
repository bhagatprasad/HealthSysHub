import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotfoundComponent } from './shared/notfound/notfound.component';
import { AdminComponent } from './dashboard/admin/admin.component';

const routes: Routes = [
  { path: 'landing', component: AdminComponent },
  { path: '', redirectTo: '/landing', pathMatch: 'full' },
  { path: '**', component: NotfoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
