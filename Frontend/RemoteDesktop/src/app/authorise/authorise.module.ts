import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateUserFormComponent } from './create-user-form/create-user-form.component';
import {SharedModule} from "../shared/shared.module";
import { AuthoriseRoutingModule } from './authorise-routing.module';
import { AuthoriseTabsComponent } from './authorise-tabs/authorise-tabs.component';
import {AuthoriseComponent} from "./authorise.component";
import {LoginCardComponent} from "./login-card/login-card.component";
import {FormsModule} from "@angular/forms";

@NgModule({
  declarations: [AuthoriseTabsComponent, CreateUserFormComponent, LoginCardComponent, AuthoriseComponent],
    imports: [
        CommonModule,
        AuthoriseRoutingModule,
        SharedModule,
        FormsModule
    ]
})
export class AuthoriseModule { }
