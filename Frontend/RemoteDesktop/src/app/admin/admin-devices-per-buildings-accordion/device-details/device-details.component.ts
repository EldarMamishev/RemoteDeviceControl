import {Component, Inject, Input, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FieldModel} from '../../../view-models/field-model';
import {CONNECTION_PATH} from '../../../constants';
import {DeviceDetailsModel} from '../../../view-models/device-details.model';
import {DeviceFieldListModel} from '../../../view-models/device-field-list.model';

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
@Component({
  selector: 'app-device-details',
  templateUrl: './device-details.component.html',
  styleUrls: ['./device-details.component.scss']
})
export class DeviceDetailsComponent implements OnInit {
  data: DeviceDetailsModel;

  constructor(
    private http: HttpClient,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<DeviceDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public id: any) {}

  ngOnInit(): void {
    this.load();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  load() {
    const methodUrl = CONNECTION_PATH + '/Device/GetAddDeviceFieldsData';
    let params = new HttpParams();
    params = params.append('deviceId', this.id);

    this.http.get<DeviceDetailsModel>(methodUrl, {
      params: params
    }).subscribe(data => this.data = data);
  }

  save() {
    console.log(this.data);
    const methodUrl = CONNECTION_PATH + '/Device/AddDeviceFields';
    let k;
    const request = new DeviceFieldListModel;
    request.deviceId = this.data.id;
    request.fields = this.data.fields;

    this.http.post(methodUrl, request, httpOptions).subscribe(data => k = data);
    this.dialogRef.close();
  }
}
