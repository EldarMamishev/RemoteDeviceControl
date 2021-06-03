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
import {MatInputModule} from '@angular/material/input';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatDialogModule} from '@angular/material/dialog';
import {NbDialogService} from '@nebular/theme';
import {AddDeviceTypeComponent} from './device-type/add-device-type-dialog/add-device-type.component';
import {MatListModule} from '@angular/material/list';
import { DeviceTypeListComponent } from './device-type/device-type-list/device-type-list.component';
import {DeviceTypeComponent} from './device-type/device-type.component';
import { DeviceDetailsComponent } from './admin-devices-per-buildings-accordion/device-details/device-details.component';


@NgModule({
    declarations: [AdminComponent, AddDeviceTypeComponent, TabsComponent, UsersTableComponent, AdminDevicesPerBuildingsAccordionComponent, LocationDialogComponent, DeviceTypeListComponent, DeviceTypeComponent, DeviceDetailsComponent],
    imports: [
      FormsModule,
      ReactiveFormsModule,
      CommonModule,
      AdminRoutingModule,
      SharedModule,
      MatInputModule,
      FormsModule,
      MatDialogModule,
      MatListModule
    ]
})
export class AdminModule { }
