import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NbSelectModule, NbInputModule, NbTabsetModule, NbCardModule, NbButtonModule, NbIconModule, NbAccordionModule, NbDatepickerModule } from '@nebular/theme';
import { SmartTableComponent } from './components/smart-table.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import {TranslateModule} from "@ngx-translate/core";
import {UsersSmartTableComponent} from "./components/users-smart-table.component";
import {NumberInputEditorComponent} from "../number-input-editor.component";
import { ButtonInputEditorComponent } from '../button-input-editor.component';
import {BuildingsSmartTableComponent} from "./components/buildings-smart-table.component";



@NgModule({
  declarations: [SmartTableComponent, UsersSmartTableComponent, BuildingsSmartTableComponent, NumberInputEditorComponent, ButtonInputEditorComponent],
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
  exports: [TranslateModule, NbSelectModule, NbInputModule, NbTabsetModule, NbCardModule, NbButtonModule, NbIconModule, NbAccordionModule, ButtonInputEditorComponent, NumberInputEditorComponent, SmartTableComponent, UsersSmartTableComponent, BuildingsSmartTableComponent, Ng2SmartTableModule, NbDatepickerModule]
})
export class SharedModule { }
