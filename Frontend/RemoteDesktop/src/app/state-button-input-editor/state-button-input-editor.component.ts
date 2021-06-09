import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from 'ng2-smart-table';
import {CONNECTION_PATH} from '../constants';
import {LogViewModel} from '../view-models/log-viewmodel';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {TranslateService} from '@ngx-translate/core';
import {MatDialog} from '@angular/material/dialog';
const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
@Component({
  selector: 'app-state-button-input-editor',
  templateUrl: './state-button-input-editor.component.html',
  styleUrls: ['./state-button-input-editor.component.scss']
})
export class StateButtonInputEditorComponent implements ViewCell, OnInit {
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() save: EventEmitter<any> = new EventEmitter();
  constructor(private http: HttpClient, private translate: TranslateService, public dialog: MatDialog) {

  }
  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    this.save.emit(this.rowData);
    if (this.rowData.name == null) {
      alert('Data is not set.');
    }
    const methodUrl = CONNECTION_PATH + '/Device/GetCurrentState';
    let subscriber: string;
    this.http.post<LogViewModel>(methodUrl, this.rowData.id, httpOptions).subscribe(data => {
      subscriber = data.log;
      alert(data.log);
    });
  }
}
