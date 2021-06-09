import {Component, Inject, OnInit} from '@angular/core';
import {MaintenanceModel} from '../../view-models/maintenance.model';
import {HttpClient, HttpParams} from '@angular/common/http';
import {FormBuilder} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CONNECTION_PATH} from '../../constants';
import {ConnectionViewmodel} from '../../view-models/connection-viewmodel';
import {LogViewModel} from '../../view-models/log-viewmodel';
import {LogListModel} from '../../view-models/log-list.model';

@Component({
  selector: 'app-logs-modal',
  templateUrl: './logs-modal.component.html',
  styleUrls: ['./logs-modal.component.scss']
})
export class LogsModalComponent implements OnInit {
  data: LogListModel = new LogListModel();

  constructor(
    private http: HttpClient,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<LogListModel>,
    @Inject(MAT_DIALOG_DATA) public id: any) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    const methodUrl = CONNECTION_PATH + '/Log/GetLogsForDevice';
    let params = new HttpParams();
    params = params.append('deviceId', this.id);

    this.http.get<LogListModel>(methodUrl, {
      params: params
    }).subscribe(data => {
      this.data = data;
    });
  }
}
