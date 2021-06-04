import {BuildingViewmodel} from '../../view-models/building-viewmodel';

import { Component, Input, AfterViewInit, ChangeDetectorRef, ChangeDetectionStrategy, Output, EventEmitter } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import {Person} from '../../view-models/people-viewmodel';
import {CONNECTION_PATH} from '../../constants';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {__await} from 'tslib';
import {SingleIdViewmodel} from '../../view-models/singleId-viewmodel';
import {MatDialog} from '@angular/material/dialog';
import {LocationDialogComponent} from '../../admin/admin-devices-per-buildings-accordion/location-dialog/location-dialog.component';
import {NewlocationViewmodel} from '../../view-models/newlocation-viewmodel';
import {DeviceDetailsModel} from '../../view-models/device-details.model';
import {DeviceDetailsComponent} from '../../admin/admin-devices-per-buildings-accordion/device-details/device-details.component';

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };

@Component({
  selector: 'buildings-smart-table',
  template: `
    <ng2-smart-table [settings]="settings"
                     [source]="dataSource"
                     (createConfirm)="onCreate($event)"
                     (editConfirm)="onEdit($event)"
                     (deleteConfirm)="onDelete($event)"
                     (userRowSelect)="onSelect($event)"></ng2-smart-table>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BuildingsSmartTableComponent implements AfterViewInit {

  private _source = new LocalDataSource([]);
  get dataSource(): LocalDataSource {
    return this._source;
  }

  @Input() settings;
  @Input() set source(data: any[]) {
    /**
     * There is an issue with change detection in ng2-smart-table's.
     * We need to trigger it manually for the correct render.
     */
    this._source.load(data).then(() => this.cd.detectChanges());
  }

  @Output() edit = new EventEmitter<any>();
  @Output() create = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();
  @Output() select = new EventEmitter<any>();

  constructor(private cd: ChangeDetectorRef, private http: HttpClient, public dialog: MatDialog) {
  }

  data: DeviceDetailsModel;

  ngAfterViewInit() {
    /**
     * There is an issue with change detection in ng2-smart-table's.
     * We need to trigger it manually for the correct first time render.
     */
    Promise.resolve().then(() => this.cd.detectChanges());
  }

  async onCreate(value) {
    debugger;
    const device = new BuildingViewmodel();
    device.name = value.newData.name;
    device.typeId = value.newData.type;
    device.locationId = value.source.data[0].locationId;
    device.userId = Number(localStorage.getItem('rdc_user'));
    device.userName = localStorage.getItem('rdc_userName');
    // device.locationId = this. parent.name.split(':')[0];
    const methodUrl = CONNECTION_PATH + '/Device/AddNewDevice';
    // const t = await this.http.post<BuildingViewmodel>(methodUrl, device, httpOptions).toPromise()
    //   .then(resp => {
    //     device.id = resp.id;
    //     const dialogRefs = this.dialog.open(DeviceDetailsComponent, {
    //       data: resp.id,
    //       width: 'auto'
    //     });
    //   });
    // device.id = t.id;
    this.http.post<BuildingViewmodel>(methodUrl, device, httpOptions).subscribe(data => {
      device.id = data.id;
      // const dialogRef = this.dialog.open(DeviceDetailsComponent, {
      //   data: data.id,
      //   width: 'auto'
      // });
    });
    this._source.refresh();
    this._source.add(device);
    this._source.refresh();


    // dialogRef.afterClosed().subscribe(result => {
    //   console.log('The dialog was closed');
    //   this.data = result;
    //   const methodUrl = CONNECTION_PATH + '/Location/AddLocation';
    //   let subscriber;
    //   this.http.post<String>(methodUrl, this.data, httpOptions).subscribe(data => subscriber = data);
    // });
  }

  onEdit(value) {
    debugger;
    const device = new BuildingViewmodel();
    device.name = value.newData.name;
    device.id = value.newData.id;
    device.typeId = value.newData.type;
    device.locationId = value.source.data[0].locationId;
    const methodUrl = CONNECTION_PATH + '/Device/UpdateDevice';
    this.http.post<BuildingViewmodel>(methodUrl, device, httpOptions).subscribe(data => device.id = data.id);
    value.confirm.resolve();
    this._source.refresh();
    this.edit.emit(value.data);
    this._source.refresh();
  }

  onDelete(value) {
    debugger;
    const deviceId = new SingleIdViewmodel();
    deviceId.id = value.data.id;
    const methodUrl = CONNECTION_PATH + '/Device/DeleteDevice';
    let subscriber;
    this.http.post<any>(methodUrl, deviceId, httpOptions).subscribe(data => subscriber = data);
    value.confirm.resolve();
    this.delete.emit(value.data);
    this._source.refresh();
  }

  onSelect(value) {
    this.select.emit(value.data);
  }
}
