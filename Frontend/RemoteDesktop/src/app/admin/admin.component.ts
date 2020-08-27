import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Person} from "../view-models/people-viewmodel";
import {CONNECTION_PATH} from "../constants";

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminComponent implements OnInit {

  constructor(private http: HttpClient) { }

  backup(){
    var methodUrl = CONNECTION_PATH + "/Admin/Backup";
    var k;

    this.http.post(methodUrl, httpOptions).subscribe(data => k = data);
  }

  ngOnInit(): void {
  }

}
