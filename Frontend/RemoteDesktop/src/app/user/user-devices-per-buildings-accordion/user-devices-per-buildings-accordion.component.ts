import { Component, OnInit, ChangeDetectionStrategy, OnChanges } from '@angular/core';
import {TranslateService} from "@ngx-translate/core";
import {ButtonInputEditorComponent} from "../../button-input-editor.component";
import {CONNECTION_PATH} from "../../constants";
import {BuildingViewmodel} from "../../view-models/building-viewmodel";
import {LocationViewmodel} from "../../view-models/location-viewmodel";
import {Person} from "../../view-models/people-viewmodel";
import {HttpClient, HttpHeaders} from "@angular/common/http";

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

@Component({
  selector: 'app-user-devices-per-buildings-accordion',
  templateUrl: './user-devices-per-buildings-accordion.component.html',
  styleUrls: ['./user-devices-per-buildings-accordion.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserDevicesPerBuildingsAccordionComponent implements OnInit {
  nameTitle :string;
  typeTitle :string;
  nameSubscr = this.translate.get('T.Buildings').subscribe((res: string) => {
    this.nameTitle = res
  });

  typeSubscr = this.translate.get('Type').subscribe((res: string) => {
    this.typeTitle = res
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
      },
      "button": {
        "title": 'Button',
        "type": 'custom',
        "renderComponent": ButtonInputEditorComponent,
        onComponentInitFunction(instance) {
          instance.save.subscribe(row => {
            alert(`${row.name} saved!`)
          });
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

  data : KeyValuePair<string, BuildingViewmodel[]>[];

  constructor(private http: HttpClient, private translate: TranslateService) {
    this.getDevices();
  }

  ngOnInit(): void {
  }

  getDevices() {
    debugger;
    var methodUrl = CONNECTION_PATH + "/Person/GetAllDevicesPerLocations"
    this.http.get<KeyValuePair<string, BuildingViewmodel[]>[]>(methodUrl).subscribe(data => this.data = data);
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
