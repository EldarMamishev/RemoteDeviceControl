import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from 'ng2-smart-table';
import {CONNECTION_PATH} from '../constants';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {TranslateService} from '@ngx-translate/core';
import {MatDialog} from '@angular/material/dialog';
import {ConnectionViewmodel} from '../view-models/connection-viewmodel';
import {LogViewModel} from '../view-models/log-viewmodel';
import {DeviceDetailsComponent} from '../admin/admin-devices-per-buildings-accordion/device-details/device-details.component';
import {ConnectionModalComponent} from './connection-modal/connection-modal.component';

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
@Component({
  selector: 'app-connect-button-input-editor',
  templateUrl: './connect-button-input-editor.component.html',
  styleUrls: ['./connect-button-input-editor.component.scss']
})
export class ConnectButtonInputEditorComponent implements ViewCell, OnInit {
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
    if (this.rowData.name == null) {
      alert('Data is not set.');
    }

    const dialogRef = this.dialog.open(ConnectionModalComponent, {
      data: this.rowData.id,
      width: 'auto'
    });
  }
}
