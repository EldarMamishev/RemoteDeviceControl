import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from "ng2-smart-table";

@Component({
  selector: 'app-connect-button-input-editor',
  templateUrl: './connect-button-input-editor.component.html',
  styleUrls: ['./connect-button-input-editor.component.scss']
})
export class ConnectButtonInputEditorComponent implements ViewCell, OnInit {
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() save: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    this.save.emit(this.rowData);

    alert(`${this.rowData.name} connected!`)
    
  }
}
