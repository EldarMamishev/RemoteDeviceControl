import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { TabsComponent } from './tabs/tabs.component';
import { UsersTableComponent } from './users-table/users-table.component';
import { AdminBuildingsFilterCardComponent } from './admin-buildings-filter-card/admin-buildings-filter-card.component';
import { AdminDevicesPerBuildingsAccordionComponent } from './admin-devices-per-buildings-accordion/admin-devices-per-buildings-accordion.component';
import { LoginCardComponent } from './login-card/login-card.component';
import { CreateUserFormComponent } from './create-user-form/create-user-form.component';
import { NewMessageActionCardComponent } from './new-message-action-card/new-message-action-card.component';
import { NewTasksActionCardComponent } from './new-tasks-action-card/new-tasks-action-card.component';
import { MessageActionCardComponent } from './message-action-card/message-action-card.component';
import { SystemActionCardComponent } from './system-action-card/system-action-card.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [AdminComponent, TabsComponent, UsersTableComponent, AdminBuildingsFilterCardComponent, AdminDevicesPerBuildingsAccordionComponent, LoginCardComponent, CreateUserFormComponent, NewMessageActionCardComponent, NewTasksActionCardComponent, MessageActionCardComponent, SystemActionCardComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
