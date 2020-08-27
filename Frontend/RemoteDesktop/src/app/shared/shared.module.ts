import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NbSelectModule, NbInputModule, NbTabsetModule, NbCardModule, NbButtonModule, NbIconModule, NbAccordionModule, NbDatepickerModule } from '@nebular/theme';
import { SmartTableComponent } from './components/smart-table.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import {TranslateModule} from "@ngx-translate/core";
import {UsersSmartTableComponent} from "./components/users-smart-table.component";
import {NumberInputEditorComponent} from "../number-input-editor.component";
import {BuildingsSmartTableComponent} from "./components/buildings-smart-table.component";
import { StateButtonInputEditorComponent } from '../state-button-input-editor/state-button-input-editor.component';
import {LogsButtonInputEditorComponent} from "../logs-button-input-editor/logs-button-input-editor.component";
import {ConnectButtonInputEditorComponent} from "../connect-button-input-editor/connect-button-input-editor.component";



@NgModule({
  declarations: [SmartTableComponent, UsersSmartTableComponent, BuildingsSmartTableComponent, NumberInputEditorComponent, StateButtonInputEditorComponent, LogsButtonInputEditorComponent, ConnectButtonInputEditorComponent],
  imports: [
    CommonModule,
    TranslateModule,
    NbSelectModule,
    NbInputModule,
    NbTabsetModule,
    NbCardModule,
    NbButtonModule,
    NbIconModule,
    NbAccordionModule,
    Ng2SmartTableModule,
    NbDatepickerModule
  ],
  exports: [TranslateModule, NbSelectModule, NbInputModule, NbTabsetModule, NbCardModule, NbButtonModule, NbIconModule, NbAccordionModule, StateButtonInputEditorComponent, LogsButtonInputEditorComponent, ConnectButtonInputEditorComponent, NumberInputEditorComponent, SmartTableComponent, UsersSmartTableComponent, BuildingsSmartTableComponent, Ng2SmartTableModule, NbDatepickerModule]
})
export class SharedModule { }
