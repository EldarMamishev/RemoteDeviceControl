import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from "ng2-smart-table";

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

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    this.save.emit(this.rowData);
    if (this.rowData.name == null)
      alert("Data is not set.")
    alert(this.rowData.name);

  }
}
