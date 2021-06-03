import {Component, OnInit, ChangeDetectionStrategy, Inject} from '@angular/core';
import {TranslateService} from "@ngx-translate/core";
import {Router} from "@angular/router";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-profile-card',
  templateUrl: './device-type.component.html',
  styleUrls: ['./device-type.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DeviceTypeComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DeviceTypeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
  ngOnInit(): void {
  }
}
