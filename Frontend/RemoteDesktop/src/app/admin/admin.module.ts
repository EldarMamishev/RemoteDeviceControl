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
import {NbDialogService} from "@nebular/theme";
import {DeviceTypeComponent} from "./device-type/add-device-type-dialog/device-type.component";
import {FieldListComponent} from "./device-type/add-device-type-dialog/fields/field-list.component";
import {FieldComponent} from "./device-type/add-device-type-dialog/fields/field/field.component";
import {MatListModule} from "@angular/material/list";


@NgModule({
  declarations: [AdminComponent, DeviceTypeComponent, TabsComponent, UsersTableComponent, AdminDevicesPerBuildingsAccordionComponent, LocationDialogComponent, FieldListComponent, FieldComponent],
    imports: [
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
