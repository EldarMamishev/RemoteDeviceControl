import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Dictionary } from "lodash";
import {BuildingViewmodel} from "../../view-models/building-viewmodel";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {CONNECTION_PATH} from "../../constants";
import {Person} from "../../view-models/people-viewmodel";
import {LocationViewmodel} from "../../view-models/location-viewmodel";
import {ButtonInputEditorComponent} from "../../button-input-editor.component";
import {TranslateService} from "@ngx-translate/core";

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

@Component({
  selector: 'app-admin-devices-per-buildings-accordion',
  templateUrl: './admin-devices-per-buildings-accordion.component.html',
  styleUrls: ['./admin-devices-per-buildings-accordion.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminDevicesPerBuildingsAccordionComponent implements OnInit {
  actionsTitle :string;
  nameTitle :string;
  typeTitle :string;
  buttonTitle :string;
  lockTitle :string;
  liftTitle :string;
  selectTitle :string;
  addTitle :string;
  createTitle :string;
  editTitle :string;
  saveTitle :string;
  cancelTitle :string;
  deleteTitle :string;

  actionsSubscr = this.translate.get('Actions').subscribe((res: string) => {
    this.actionsTitle = res
  });
  nameSubscr = this.translate.get('T.Buildings').subscribe((res: string) => {
    this.nameTitle = res
  });
  typeSubscr = this.translate.get('Type').subscribe((res: string) => {
    this.typeTitle = res
  });
  lockSubscr = this.translate.get('Lock').subscribe((res: string) => {
    this.lockTitle = res
  });
  liftSubscr = this.translate.get('Lift').subscribe((res: string) => {
    this.liftTitle = res
  });
  selectSubscr = this.translate.get('Select').subscribe((res: string) => {
    this.selectTitle = res
  });
  addSubscr = this.translate.get('AddNew').subscribe((res: string) => {
    this.addTitle = res
  });
  createSubscr = this.translate.get('Create').subscribe((res: string) => {
    this.createTitle = res
  });
  editSubscr = this.translate.get('Edit').subscribe((res: string) => {
    this.editTitle = res
  });
  saveSubscr = this.translate.get('Save').subscribe((res: string) => {
    this.saveTitle = res
  });
  cancelSubscr = this.translate.get('Cancel').subscribe((res: string) => {
    this.cancelTitle = res
  });
  deleteSubscr = this.translate.get('Delete').subscribe((res: string) => {
    this.deleteTitle = res
  });

  settings =   {
    "columns": {
      "id": {
        "title": "ID",
        "filter": true
      },
      "name": {
        "title": this.nameTitle
      },
      "type": {
        "title": this.typeTitle,
        "editor": {
          "type": "list",
          "config": {
            "selectText": "Select ...",
            list: [
              { value: 'lock', title: this.liftTitle },
              { value: 'lift', title: this.lockTitle },
            ],
          }
        },
        "filter": {
          "type": "list",
          "config": {
            "selectText": this.selectTitle + '...',
            list: [
              { value: 'lock', title: this.liftTitle },
              { value: 'lift', title: this.lockTitle },
            ],
          }
        }
      }
    },
    "delete": {
      "confirmDelete": true,
      "deleteButtonContent": this.deleteTitle
    },
    "add": {
      "confirmCreate": true,
      "cancelButtonContent": this.cancelTitle,
      "addButtonContent": this.addTitle,
      "createButtonContent": this.createTitle
    },
    "edit": {
      "confirmSave": true,
      "cancelButtonContent": this.cancelTitle,
      "editButtonContent": this.editTitle,
      "saveButtonContent": this.saveTitle
    },
    "actions": {
      "columnTitle": this.actionsTitle,
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

  constructor(private http: HttpClient, private translate: TranslateService) {
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
