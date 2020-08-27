import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { Tabs1Component } from './tabs1/tabs1.component';
import { UserDevicesPerBuildingsAccordionComponent } from './user-devices-per-buildings-accordion/user-devices-per-buildings-accordion.component';
import { SharedModule } from '../shared/shared.module';
import {MissingTranslationHandler, TranslateLoader, TranslateModule} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";


@NgModule({
  declarations: [UserComponent, Tabs1Component, UserDevicesPerBuildingsAccordionComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }
