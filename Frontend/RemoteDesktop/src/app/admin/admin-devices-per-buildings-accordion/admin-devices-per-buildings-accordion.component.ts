import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Dictionary } from "lodash";
import {BuildingViewmodel} from "../../view-models/building-viewmodel";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {CONNECTION_PATH} from "../../constants";
import {Person} from "../../view-models/people-viewmodel";
import {LocationViewmodel} from "../../view-models/location-viewmodel";

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

@Component({
  selector: 'app-admin-devices-per-buildings-accordion',
  templateUrl: './admin-devices-per-buildings-accordion.component.html',
  styleUrls: ['./admin-devices-per-buildings-accordion.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminDevicesPerBuildingsAccordionComponent implements OnInit {
  settings =   {
    "columns": {
      "id": {
        "title": "ID",
        "filter": true
      },
      "name": {
        "title": "Name"
      },
      "type": {
        "title": "Type",
        "editor": {
          "type": "list",
          "config": {
            "selectText": "Select ...",
            list: [
              { value: 'lock', title: 'Lock' },
              { value: 'lift', title: 'Lift' },
            ],
          }
        },
        "filter": {
          "type": "list",
          "config": {
            "selectText": "Select ...",
            list: [
              { value: 'lock', title: 'Lock' },
              { value: 'lift', title: 'Lift' },
            ],
          }
        }
      }
    },
    "delete": {
      "confirmDelete": true
    },
    "add": {
      "confirmCreate": true
    },
    "edit": {
      "confirmSave": true
    },
    "actions": {
      "add": true,
      "edit": true,
      "delete": true
    },
    "mode": "internal"
  };
  source =   [
  ];

  data : KeyValuePair<string, BuildingViewmodel[]>[];
  locations : LocationViewmodel[];

  constructor(private http: HttpClient) {
    this.getDevices();
  }

  ngOnInit(): void {
    debugger;
    this.getDevices();

  }

  getDevices() {
    debugger;
    var methodUrl = CONNECTION_PATH + "/Admin/GetAllDevicesPerLocations"
    this.http.get<KeyValuePair<string, BuildingViewmodel[]>[]>(methodUrl).subscribe(data => this.data = data);
  }

  getLocations() {
    debugger;
    var methodUrl = CONNECTION_PATH + "/Admin/GetLocations"
    this.http.get<LocationViewmodel[]>(methodUrl).subscribe(data => this.locations = data);
  }

  addDevice(value, locationId) {
    debugger;
    var device = new BuildingViewmodel();
    device.name = value.newData.name;
    device.type = value.newData.type;
    device.locationId = locationId;
    var methodUrl = CONNECTION_PATH + "/Admin/AddNewDevice"
    this.http.post<Person>(methodUrl, device, httpOptions).subscribe(data => device.id = data.id);
  }
}

export class KeyValuePair<T, U>{
  Key: T;
  Value: U;

  constructor(key: T, value: U) {
    this.Key = key;
    this.Value = value;
  }
}
