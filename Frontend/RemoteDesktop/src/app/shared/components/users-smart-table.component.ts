import { Component, Input, AfterViewInit, ChangeDetectorRef, ChangeDetectionStrategy, Output, EventEmitter } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import {Person} from "../../view-models/people-viewmodel";
import {CONNECTION_PATH} from "../../constants";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {__await} from "tslib";
import {BuildingViewmodel} from "../../view-models/building-viewmodel";
import {SingleIdViewmodel} from "../../view-models/singleId-viewmodel";

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };

@Component({
  selector: 'users-smart-table',
  template: `
    <ng2-smart-table [settings]="settings"
                     [source]="dataSource"
                     (editConfirm)="onEdit($event)"
                     (deleteConfirm)="onDelete($event)"
                     (userRowSelect)="onSelect($event)"></ng2-smart-table>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})

// (createConfirm)="onCreate($event)"

export class UsersSmartTableComponent implements AfterViewInit {

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
  // @Output() create = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();
  @Output() select = new EventEmitter<any>();

  constructor(private cd: ChangeDetectorRef, private http: HttpClient) {
  }

  ngAfterViewInit() {
    /**
     * There is an issue with change detection in ng2-smart-table's.
     * We need to trigger it manually for the correct first time render.
     */
    Promise.resolve().then(() => this.cd.detectChanges());
  }

  // onCreate(value) {
  //   debugger;
  //   var person = new Person();
  //   person.userName = value.newData.userName;
  //   person.firstName = value.newData.firstName;
  //   person.lastName = value.newData.lastName;
  //   person.email = value.newData.email;
  //   person.id = 0;
  //   var methodUrl = CONNECTION_PATH + "/Admin/AddNewUser"
  //   this.http.post<Person>(methodUrl, person, httpOptions).subscribe(data =>
  //   {
  //     person.id = data.id;
  //     this._source.add(person);
  //     this._source.refresh();
  //     value.confirm.reject();
  //   });
  // }

  onEdit(value) {
    debugger;
    var person = new Person();
    person.firstName = value.newData.firstName;
    person.id = value.newData.id;
    person.lastName = value.newData.lastName;
    person.userName = value.newData.userName;
    person.email = value.newData.email;
    var methodUrl = CONNECTION_PATH + "/Admin/UpdateUser"
    this.http.post<BuildingViewmodel>(methodUrl, person, httpOptions).subscribe(data => person.id = data.id);
    value.confirm.resolve();
    this._source.refresh();
    this.edit.emit(value.data);;
    this._source.refresh();
  }

  onDelete(value) {
    debugger;
    var deviceId = new SingleIdViewmodel();
    deviceId.id = value.data.id;
    var methodUrl = CONNECTION_PATH + "/Admin/DeleteUser"
    var subscriber;
    this.http.post<any>(methodUrl, deviceId, httpOptions).subscribe(data => subscriber = data);
    value.confirm.resolve();
    this.delete.emit(value.data);
    this._source.refresh();
  }

  onSelect(value) {
    this.select.emit(value.data);
  }
}
