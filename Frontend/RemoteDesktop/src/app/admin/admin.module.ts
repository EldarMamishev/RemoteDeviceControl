import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { TabsComponent } from './tabs/tabs.component';
import { UsersTableComponent } from './users-table/users-table.component';
import { AdminDevicesPerBuildingsAccordionComponent } from './admin-devices-per-buildings-accordion/admin-devices-per-buildings-accordion.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [AdminComponent, TabsComponent, UsersTableComponent, AdminDevicesPerBuildingsAccordionComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
