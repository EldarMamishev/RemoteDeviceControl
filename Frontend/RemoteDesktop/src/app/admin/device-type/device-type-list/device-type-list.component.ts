import {AfterViewInit, ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {LocalDataSource} from 'ng2-smart-table';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {BuildingViewmodel} from '../../../view-models/building-viewmodel';
import {CONNECTION_PATH} from '../../../constants';
import {SingleIdViewmodel} from '../../../view-models/singleId-viewmodel';
import {ConnectButtonInputEditorComponent} from '../../../connect-button-input-editor/connect-button-input-editor.component';
import {StateButtonInputEditorComponent} from '../../../state-button-input-editor/state-button-input-editor.component';
import {LogsButtonInputEditorComponent} from '../../../logs-button-input-editor/logs-button-input-editor.component';
import {TranslateService} from '@ngx-translate/core';
import {MatDialog} from '@angular/material/dialog';
import {KeyValuePair} from '../../admin-devices-per-buildings-accordion/admin-devices-per-buildings-accordion.component';
import {DeviceTypeModel} from '../../../view-models/device-type-model';
import {DeviceTypeGridModel} from '../../../view-models/device-type-grid.model';

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };

@Component({
  selector: 'app-device-type-list',
  templateUrl: './device-type-list.component.html',
  styleUrls: ['./device-type-list.component.scss']
})
export class DeviceTypeListComponent implements OnInit, AfterViewInit {

private _source = new LocalDataSource([]);
  get dataSource(): LocalDataSource {
    return this._source;
  }
  data: DeviceTypeGridModel[];
  actionsTitle: string;
  nameTitle: string;
  fieldsTitle: string;

  actionsSubscr = this.translate.get('Actions').subscribe((res: string) => {
    this.actionsTitle = res;
  });
  nameSubscr = this.translate.get('Name').subscribe((res: string) => {
    this.nameTitle = res;
  });
  fieldsSubscr = this.translate.get('Fields').subscribe((res: string) => {
    this.fieldsTitle = res;
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
        'title': this.nameTitle,
        'filter': true,
        'editable': false,
        'addable': false,
      },
      'fields': {
        'title': this.fieldsTitle,
        'editable': false,
        'addable': false,
      }
    },
    'actions': false,
    'mode': 'internal',
    'attr': {
      'class': 'table table-bordered'
    },
  };
@Input() set source(data: any[]) {
    /**
     * There is an issue with change detection in ng2-smart-table's.
     * We need to trigger it manually for the correct render.
     */
    this._source.load(data).then(() => this.cd.detectChanges());
  }

  constructor(private cd: ChangeDetectorRef, private http: HttpClient, private translate: TranslateService, public dialog: MatDialog) {
  }

  ngAfterViewInit() {
    /**
     * There is an issue with change detection in ng2-smart-table's.
     * We need to trigger it manually for the correct first time render.
     */
    Promise.resolve().then(() => this.cd.detectChanges());
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    const methodUrl = CONNECTION_PATH + '/DeviceType/GetDeviceTypes';
    this.http.get<DeviceTypeGridModel[]>(methodUrl).subscribe(data => this.data = data);
  }
}
