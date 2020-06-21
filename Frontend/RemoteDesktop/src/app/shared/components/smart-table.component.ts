import { Component, Input, AfterViewInit, ChangeDetectorRef, ChangeDetectionStrategy, Output, EventEmitter } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';

@Component({
  selector: 'app-smart-table',
  template: `
    <ng2-smart-table [settings]="settings"
                     [source]="dataSource"
                     (createConfirm)="onCreate($event)"
                     (editConfirm)="onEdit($event)"
                     (deleteConfirm)="onDelete($event)"
                     (userRowSelect)="onSelect($event)"></ng2-smart-table>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SmartTableComponent implements AfterViewInit {

  private _source = new LocalDataSource([]);
  get dataSource(): LocalDataSource {
    return this._source;
  }

  @Input() settings;
  @Input() set source(data: any[]) {
    /**
     * There is an issue with change detection in ng2-smart-table's.
     * We need to trigger it manually for the correct render.
     */
    this._source.load(data).then(() => this.cd.detectChanges());
  }

  @Output() edit = new EventEmitter<any>();
  @Output() create = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();
  @Output() select = new EventEmitter<any>();

  constructor(private cd: ChangeDetectorRef) {
  }

  ngAfterViewInit() {
    /**
     * There is an issue with change detection in ng2-smart-table's.
     * We need to trigger it manually for the correct first time render.
     */
    Promise.resolve().then(() => this.cd.detectChanges());
  }

  onCreate(value) {
    value.confirm.resolve();
    this.create.emit(value.data);
  }

  onEdit(value) {
    value.confirm.resolve();
    this.edit.emit(value.data);
  }

  onDelete(value) {
    value.confirm.resolve();
    this.delete.emit(value.data);
  }

  onSelect(value) {
    this.select.emit(value.data);
  }
}
