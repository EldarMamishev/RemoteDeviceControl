import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from 'ng2-smart-table';
import {CONNECTION_PATH} from '../constants';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {TranslateService} from '@ngx-translate/core';
import {MatDialog} from '@angular/material/dialog';
import {ConnectionViewmodel} from '../view-models/connection-viewmodel';
import {DeviceDetailsComponent} from '../admin/admin-devices-per-buildings-accordion/device-details/device-details.component';

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
@Component({
  selector: 'app-connect-button-input-editor',
  templateUrl: './add-fields-button-editor.component.html',
  styleUrls: ['./add-fields-button-editor.component.scss']
})
export class AddFieldsButtonEditorComponent implements ViewCell, OnInit {
  renderValue: string;

  constructor(private http: HttpClient, private translate: TranslateService, public dialog: MatDialog) {
  }

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() save: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    this.save.emit(this.rowData);
    if (this.rowData.id == null) {
      alert('Data is not set.');
    }
    const dialogRef = this.dialog.open(DeviceDetailsComponent, {
      data: this.rowData.id,
      width: 'auto'
    });
  }
}
