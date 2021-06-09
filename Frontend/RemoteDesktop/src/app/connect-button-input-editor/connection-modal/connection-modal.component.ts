import {Component, Inject, OnInit} from '@angular/core';
import {DeviceDetailsModel} from '../../view-models/device-details.model';
import {HttpClient, HttpParams} from '@angular/common/http';
import {FormBuilder} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {MaintenanceModel} from '../../view-models/maintenance.model';
import {CONNECTION_PATH} from '../../constants';
import {DeviceFieldListModel} from '../../view-models/device-field-list.model';
import {ConnectionViewmodel} from '../../view-models/connection-viewmodel';
import {LogViewModel} from '../../view-models/log-viewmodel';

@Component({
  selector: 'app-connection-modal',
  templateUrl: './connection-modal.component.html',
  styleUrls: ['./connection-modal.component.scss']
})
export class ConnectionModalComponent implements OnInit {
  data: MaintenanceModel = new MaintenanceModel();
  info: DeviceDetailsModel = new DeviceDetailsModel();

  constructor(
    private http: HttpClient,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<MaintenanceModel>,
    @Inject(MAT_DIALOG_DATA) public id: any) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    const methodUrl = CONNECTION_PATH + '/Connection/StartConnection';
    const body = new ConnectionViewmodel();
    let params = new HttpParams();
    params = params.append('personId', localStorage.getItem('rdc_user'));
    params = params.append('deviceId', this.id);
    this.http.get<DeviceDetailsModel>(methodUrl, {params: params}).subscribe(data => {
      console.log(data);
      this.info = data;
    });
  }

  save() {
    if (this.data.time > 0) {
      const maintenanceUrl = CONNECTION_PATH + '/Connection/StartMaintenance';
      let k;
      this.data.deviceId = this.id;
      this.data.personId = Number(localStorage.getItem('rdc_user'));
      console.log(this.data);
      this.http.post(maintenanceUrl, this.data).subscribe(response => k = response);
    }
    this.dialogRef.close();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
