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
  private dialog: HTMLDialogElement;

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
  buttonSubscr = this.translate.get('Button').subscribe((res: string) => {
    this.buttonTitle = res
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
      },
      "button": {
        "title": this.buttonTitle,
        "type": 'custom',
        "renderComponent": ButtonInputEditorComponent,
        onComponentInitFunction(instance) {
          instance.save.subscribe(row => {
            alert(`${row.name} connected!`)
          });
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

  logId : number;

  log : string;

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
