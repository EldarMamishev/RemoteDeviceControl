import {Component, OnInit, ChangeDetectionStrategy, Inject} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {Router} from '@angular/router';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {RegisterFields} from '../../../../view-models/register-fields';
import {FieldModel} from '../../../../view-models/field-model';

@Component({
  selector: 'app-field-list',
  templateUrl: './field-list.component.html',
  styleUrls: ['./field-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FieldListComponent implements OnInit {

  constructor() {}

  data: any;

  fields: FieldModel = new FieldModel();
  fieldsLength: number;

  ngOnInit(): void {
    this.data = new Array<FieldModel>();
    this.data.push(new FieldModel());
  }

  initializeFields(count: number): void {
    this.data = new FieldModel[count];

  }

  trackByIndex(index: number, obj: any): any {
    return index;
  }

  addField(): any {
    debugger;
    this.data.push(new FieldModel());
  }
}
