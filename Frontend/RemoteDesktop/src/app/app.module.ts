import { CommonModule } from '@angular/common';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { FormsModule }   from '@angular/forms';

import {
  NbThemeModule,
  NbLayoutModule,
  NbSidebarModule,
  NbMenuModule,
  NbIconModule,
  NbButtonModule,
  NbDatepickerModule,
  NbMediaBreakpoint,
} from '@nebular/theme';


import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { LIGHT_THEME, DARK_THEME } from './theme';
import {MissingTranslationHandler, TranslateLoader, TranslateModule} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {MissingTranslationService} from "./missing-translation-service";
import { ConnectionModalComponent } from './connect-button-input-editor/connection-modal/connection-modal.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDialogModule} from '@angular/material/dialog';
import { LogsModalComponent } from './logs-button-input-editor/logs-modal/logs-modal.component';

const mediaBreakpoints: NbMediaBreakpoint[] = [
  {
    name: 'xs',
    width: 0,
  },
  {
    name: 'sm',
    width: 320,
  },
  {
    name: 'md',
    width: 480,
  },
  {
    name: 'lg',
    width: 768,
  },
  {
    name: 'xl',
    width: 1024,
  },
];

export function HttpLoaderFactory(http: HttpClient): TranslateLoader {
  return new TranslateHttpLoader(http, './assets/locale/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    ConnectionModalComponent,
    LogsModalComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CommonModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
      missingTranslationHandler: {provide: MissingTranslationHandler, useClass: MissingTranslationService},
      useDefaultLang: false,
    }),
    NbThemeModule.forRoot({name: 'light'}, [LIGHT_THEME], mediaBreakpoints),
    NbLayoutModule,
    NbSidebarModule.forRoot(),
    NbMenuModule.forRoot(),
    NbDatepickerModule.forRoot(),
    NbEvaIconsModule,
    NbIconModule,
    SharedModule,
    AppRoutingModule,
    NbButtonModule,
    MatFormFieldModule,
    MatDialogModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
