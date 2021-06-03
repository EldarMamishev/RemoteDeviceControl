import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from 'ng2-smart-table';
import {LocationDialogComponent} from '../admin/admin-devices-per-buildings-accordion/location-dialog/location-dialog.component';
import {NewlocationViewmodel} from '../view-models/newlocation-viewmodel';
import {CONNECTION_PATH} from '../constants';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {TranslateService} from '@ngx-translate/core';
import {MatDialog} from '@angular/material/dialog';
import {LogViewModel} from '../view-models/log-viewmodel';

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

@Component({
  selector: 'app-logs-button-input-editor',
  templateUrl: './logs-button-input-editor.component.html',
  styleUrls: ['./logs-button-input-editor.component.scss']
})
export class LogsButtonInputEditorComponent implements ViewCell, OnInit {
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

    const methodUrl = CONNECTION_PATH + '/Log/GetLogsForDevice';
    let subscriber: string;
    this.http.post<LogViewModel>(methodUrl, this.rowData.id, httpOptions).subscribe(data => {
      subscriber = data.log;
      alert(data.log);
    });


    // const dialogRef = this.dialog.open(LocationDialogComponent, {
    //   width: 'auto'
    // });
    //
    // dialogRef.afterClosed().subscribe(result => {
    //   console.log('The dialog was closed');
    //   var methodUrl = CONNECTION_PATH + "/Device/GetLogsForDevice"
    //   var subscriber;
    //   this.http.post<String>(methodUrl, this.rowData.id, httpOptions).subscribe(data => subscriber = data);
    // });
  }
}
