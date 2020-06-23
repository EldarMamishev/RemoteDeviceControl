import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileCardComponent } from './profile-card/profile-card.component';
import {ProfileComponent} from "./profile.component";
import {SharedModule} from "../shared/shared.module";
import {MissingTranslationHandler, TranslateLoader, TranslateModule} from "@ngx-translate/core";
import {HttpClient} from "@angular/common/http";
import {MissingTranslationService} from "../missing-translation-service";
import {HttpLoaderFactory} from "../app.module";

@NgModule({
  declarations: [ProfileCardComponent, ProfileComponent],
  imports: [
    ProfileRoutingModule,
    SharedModule
  ]
})
export class ProfileModule { }
