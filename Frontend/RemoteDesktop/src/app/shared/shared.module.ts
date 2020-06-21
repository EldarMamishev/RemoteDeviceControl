import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NbSelectModule, NbInputModule, NbTabsetModule, NbCardModule, NbButtonModule, NbIconModule, NbAccordionModule, NbDatepickerModule } from '@nebular/theme';
import { SmartTableComponent } from './components/smart-table.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';



@NgModule({
  declarations: [SmartTableComponent],
  imports: [
    CommonModule,
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
  exports: [NbSelectModule, NbInputModule, NbTabsetModule, NbCardModule, NbButtonModule, NbIconModule, NbAccordionModule, SmartTableComponent, Ng2SmartTableModule, NbDatepickerModule]
})
export class SharedModule { }
