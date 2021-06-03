import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Person} from "../view-models/people-viewmodel";
import {CONNECTION_PATH} from "../constants";
import {ProfileCardComponent} from "../profile/profile-card/profile-card.component";
import {NbDialogService} from "@nebular/theme";
import {LocationDialogComponent} from "./admin-devices-per-buildings-accordion/location-dialog/location-dialog.component";
import {TranslateService} from "@ngx-translate/core";
import {MatDialog} from "@angular/material/dialog";
import {FieldListComponent} from "./device-type/add-device-type-dialog/fields/field-list.component";

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminComponent implements OnInit {

  constructor(private http: HttpClient, public dialog: MatDialog) { }

  backup(){
    var methodUrl = CONNECTION_PATH + "/Admin/Backup";
    var k;

    this.http.post(methodUrl, httpOptions).subscribe(data => k = data);
  }

  ngOnInit(): void {
  }

  open() {
    const dialogRef = this.dialog.open(FieldListComponent, {
      width: 'auto'
    });
  }
}
