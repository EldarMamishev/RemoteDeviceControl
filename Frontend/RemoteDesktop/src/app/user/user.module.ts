import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { Tabs1Component } from './tabs1/tabs1.component';
import { UserBuildingsFilterCardComponent } from './user-buildings-filter-card/user-buildings-filter-card.component';
import { UserDevicesPerBuildingsAccordionComponent } from './user-devices-per-buildings-accordion/user-devices-per-buildings-accordion.component';
import { UserDevicesTable1Component } from './user-devices-table1/user-devices-table1.component';
import { ProfileCardComponent } from './profile-card/profile-card.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [UserComponent, Tabs1Component, UserBuildingsFilterCardComponent, UserDevicesPerBuildingsAccordionComponent, UserDevicesTable1Component, ProfileCardComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }
