   import { NgModule } from '@angular/core';
   import { CommonModule } from '@angular/common';
import { AccountService } from './account.service';

   @NgModule({
     declarations: [],
     imports: [
       CommonModule
     ],
     providers: [
       AccountService,
     ],
     exports: []
   })
   export class SharedModule { }
   