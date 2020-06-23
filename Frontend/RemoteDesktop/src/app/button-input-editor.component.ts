﻿import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ViewCell} from "ng2-smart-table";

@Component({
  selector: 'button-view',
  template: `
    <button (click)="onClick()" width="20px">Connect</button>
  `,
})
export class ButtonInputEditorComponent implements ViewCell, OnInit {
  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  @Output() save: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.renderValue = this.value.toString().toUpperCase();
  }

  onClick() {
    this.save.emit(this.rowData);
  }
}
