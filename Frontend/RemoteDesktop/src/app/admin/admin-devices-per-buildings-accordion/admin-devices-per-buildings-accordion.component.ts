import {Component, OnInit, ChangeDetectionStrategy, Inject} from '@angular/core';
import { Dictionary } from 'lodash';
import {BuildingViewmodel} from '../../view-models/building-viewmodel';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {CONNECTION_PATH} from '../../constants';
import {Person} from '../../view-models/people-viewmodel';
import {LocationViewmodel} from '../../view-models/location-viewmodel';
import {TranslateService} from '@ngx-translate/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {LocationDialogComponent} from './location-dialog/location-dialog.component';
import {NewlocationViewmodel} from '../../view-models/newlocation-viewmodel';
import {StateButtonInputEditorComponent} from '../../state-button-input-editor/state-button-input-editor.component';
import {ConnectButtonInputEditorComponent} from '../../connect-button-input-editor/connect-button-input-editor.component';
import {LogsButtonInputEditorComponent} from '../../logs-button-input-editor/logs-button-input-editor.component';
import {AddFieldsButtonEditorComponent} from '../../add-fields-button-editor/add-fields-button-editor.component';

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

@Component({
  selector: 'app-admin-devices-per-buildings-accordion',
  templateUrl: './admin-devices-per-buildings-accordion.component.html',
  styleUrls: ['./admin-devices-per-buildings-accordion.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminDevicesPerBuildingsAccordionComponent implements OnInit {
  actionsTitle: string;
  nameTitle: string;
  typeTitle: string;
  stateTitle: string;
  connectTitle: string;
  logsTitle: string;
  lockTitle: string;
  liftTitle: string;
  selectTitle: string;
  addTitle: string;
  createTitle: string;
  editTitle: string;
  saveTitle: string;
  cancelTitle: string;
  deleteTitle: string;

  actionsSubscr = this.translate.get('Actions').subscribe((res: string) => {
    this.actionsTitle = res;
  });
  nameSubscr = this.translate.get('Name').subscribe((res: string) => {
    this.nameTitle = res;
  });
  typeSubscr = this.translate.get('Type').subscribe((res: string) => {
    this.typeTitle = res;
  });
  lockSubscr = this.translate.get('Lock').subscribe((res: string) => {
    this.lockTitle = res;
  });
  liftSubscr = this.translate.get('Lift').subscribe((res: string) => {
    this.liftTitle = res;
  });
  selectSubscr = this.translate.get('Select').subscribe((res: string) => {
    this.selectTitle = res;
  });
  addSubscr = this.translate.get('AddNew').subscribe((res: string) => {
    this.addTitle = res;
  });
  createSubscr = this.translate.get('Create').subscribe((res: string) => {
    this.createTitle = res;
  });
  editSubscr = this.translate.get('Edit').subscribe((res: string) => {
    this.editTitle = res;
  });
  saveSubscr = this.translate.get('Save').subscribe((res: string) => {
    this.saveTitle = res;
  });
  cancelSubscr = this.translate.get('Cancel').subscribe((res: string) => {
    this.cancelTitle = res;
  });
  deleteSubscr = this.translate.get('Delete').subscribe((res: string) => {
    this.deleteTitle = res;
  });
  stateSubscr = this.translate.get('State').subscribe((res: string) => {
    this.stateTitle = res;
  });
  connectSubscr = this.translate.get('Connect').subscribe((res: string) => {
    this.connectTitle = res;
  });
  logsSubscr = this.translate.get('Logs').subscribe((res: string) => {
    this.logsTitle = res;
  });

  settings =   {
    'columns': {
      'id': {
        'title': 'ID',
        'filter': true,
        'editable': false,
        'addable': false,
      },
      'name': {
        'title': this.nameTitle
      },
      'type': {
        'title': this.typeTitle,
        'editable': false,
        'editor': {
          'type': 'list',
          'valuePrepareFunction': (cell, row) => row,
          'config': {
            'selectText': 'Select ...',
            'list': this.getDeviceTypesFilter()
          }
        },
        'filter': {
          'type': 'list',
          'config': {
            'selectText': this.selectTitle + '...',
            'list': this.getDeviceTypesFilter()
          }
        }
      },
      'connect': {
        'title': '',
        'type': 'custom',
        'width': '150px',
        'renderComponent': ConnectButtonInputEditorComponent,
        'filter': false,
        'editable': false,
        'addable': false,
        sort: false
      },
      'addFields': {
        'title': '',
        'type': 'custom',
        'width': '150px',
        'renderComponent': AddFieldsButtonEditorComponent,
        'editable': false,
        'addable': false,
        'filter': false,
        sort: false
      },
      'currentState': {
        'title': '',
        'value': '',
        'type': 'custom',
        'width': '170px',
        'renderComponent': StateButtonInputEditorComponent,
        'editable': false,
        'addable': false,
        'filter': false,
        sort: false
      },
      'logs': {
        'title': '',
        'type': 'custom',
        'width': '130px',
        'renderComponent': LogsButtonInputEditorComponent,
        'editable': false,
        'addable': false,
        'filter': false,
        sort: false
      }
    },
    'delete': {
      'confirmDelete': true,
      'deleteButtonContent': this.deleteTitle
    },
    'add': {
      'confirmCreate': true,
      'cancelButtonContent': this.cancelTitle,
      'addButtonContent': this.addTitle,
      'createButtonContent': this.createTitle
    },
    'edit': {
      'confirmSave': true,
      'cancelButtonContent': this.cancelTitle,
      'editButtonContent': this.editTitle,
      'saveButtonContent': this.saveTitle
    },
    'actions': {
      'columnTitle': this.actionsTitle,
      'add': true,
      'edit': true,
      'delete': true
    },
    'mode': 'internal'
  };

  source =   [
  ];

  typeList: any;
  data: KeyValuePair<string, BuildingViewmodel[]>[];
  locations: LocationViewmodel[];
  newLocation: string;

  constructor(private http: HttpClient, private translate: TranslateService, public dialog: MatDialog) {
    this.getDevices();
  }

  ngOnInit(): void {
    this.getDevices();
    this.getDeviceTypesFilter();
    this.settings.columns.type.editor.config.list = this.typeList.deviceTypes;
    this.settings.columns.type.filter.config.list = this.typeList.deviceTypeFilters;
  }

  getDevices() {
    this.getDeviceTypesFilter();
    const methodUrl = CONNECTION_PATH + '/Device/GetAllDevicesPerLocations';
    this.http.get<KeyValuePair<string, BuildingViewmodel[]>[]>(methodUrl).subscribe(data => this.data = data);
  }

  getLocations() {
    const methodUrl = CONNECTION_PATH + '/Location/GetLocations';
    this.http.get<LocationViewmodel[]>(methodUrl).subscribe(data => this.locations = data);
  }

  addDevice(value, locationId) {
    debugger;
    const device = new BuildingViewmodel();
    device.name = value.newData.name;
    device.typeId = value.newData.type;
    device.locationId = locationId;
    device.userId = Number(localStorage.getItem('rdc_user'));
    device.userName = localStorage.getItem('rdc_userName');
    const methodUrl = CONNECTION_PATH + '/Device/AddNewDevice';
    this.http.post<BuildingViewmodel>(methodUrl, device, httpOptions).subscribe(data => device.id = data.id);
  }

  addLocation() {
    const dialogRef = this.dialog.open(LocationDialogComponent, {
      width: 'auto'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.newLocation = result;
      const viewModel = new NewlocationViewmodel();
      viewModel.name = this.newLocation;
      const methodUrl = CONNECTION_PATH + '/Location/AddLocation';
      let subscriber;
      this.http.post<String>(methodUrl, viewModel, httpOptions).subscribe(data => subscriber = data);
    });
  }

  async getDeviceTypesFilter() {
    debugger;
    const methodUrl = CONNECTION_PATH + '/DeviceType/GetDeviceTypesFilter';
    this.http.get<any>(methodUrl).subscribe(data => this.typeList = data);
    // this.typeList = subscriber;
    this.settings.columns.type.editor.config.list = this.typeList.deviceTypes;
    this.settings.columns.type.filter.config.list = this.typeList.deviceTypeFilters;
    this.settings = Object.assign({}, this.settings);
    return this.typeList;
  }

  reload() {
    this.settings = Object.assign({}, this.settings);
  }

}


export class KeyValuePair<T, U> {
  Key: T;
  Value: U;

  constructor(key: T, value: U) {
    this.Key = key;
    this.Value = value;
  }
}
export class GridFilterModel {
  value: any;
  title: string;
}
