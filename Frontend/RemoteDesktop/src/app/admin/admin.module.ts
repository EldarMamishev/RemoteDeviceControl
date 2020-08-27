import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { TabsComponent } from './tabs/tabs.component';
import { UsersTableComponent } from './users-table/users-table.component';
import {
  AdminDevicesPerBuildingsAccordionComponent
} from './admin-devices-per-buildings-accordion/admin-devices-per-buildings-accordion.component';
import { SharedModule } from '../shared/shared.module';
import { LocationDialogComponent } from './admin-devices-per-buildings-accordion/location-dialog/location-dialog.component';
import {MatInputModule} from "@angular/material/input";
import {FormsModule} from "@angular/forms";
import {MatDialogModule} from "@angular/material/dialog";


@NgModule({
  declarations: [AdminComponent, TabsComponent, UsersTableComponent, AdminDevicesPerBuildingsAccordionComponent, LocationDialogComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule,
    MatInputModule,
    FormsModule,
    MatDialogModule
  ]
})
export class AdminModule { }
