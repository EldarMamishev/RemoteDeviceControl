import { Component, OnInit } from '@angular/core';
import {AddDeviceTypeComponent} from './add-device-type-dialog/add-device-type.component';
import {HttpClient} from '@angular/common/http';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-device-type',
  templateUrl: './device-type.component.html',
  styleUrls: ['./device-type.component.scss']
})
export class DeviceTypeComponent implements OnInit {

  constructor(private http: HttpClient, public dialog: MatDialog) { }

  ngOnInit() {
  }

  open() {
    const dialogRef = this.dialog.open(AddDeviceTypeComponent, {
      width: 'auto'
    });
  }

}
