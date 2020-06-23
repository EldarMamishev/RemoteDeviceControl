﻿import {Component} from "@angular/core";
import {DefaultEditor} from "ng2-smart-table";


@Component({
  selector: 'input-editor',
  template: `
    <input type="number"
           [name]="cell.getId()"
           [placeholder]="cell.getTitle()"
           [disabled]="!cell.isEditable()"
           (click)="onClick.emit($event)"
           (keydown.enter)="onEdited.emit($event)"
           (keydown.esc)="onStopEditing.emit()">
    `,
})
export class NumberInputEditorComponent extends DefaultEditor {

  constructor() {
    super();
  }
}
