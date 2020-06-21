import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Person } from 'src/app/view-models/people-viewmodel';
import {CONNECTION_PATH} from "../../constants";
import {HttpHeaders, HttpClient} from "@angular/common/http";

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UsersTableComponent implements OnInit {
  private root = CONNECTION_PATH + '/admin/';
  settings =   {
    "columns": {
      "id": {
        "title": "ID",
        "filter": true
      },
      "userName": {
        "title": "User Name",
        "filter": true
      },
      "email": {
        "title": "Email",
        "filter": true
      },
      "firstName": {
        "title": "First Name",
        "filter": true
      },
      "lastName": {
        "title": "Last Name",
        "filter": true
      },
      "gender": {
        "title": "Gender",
        "filter": {
          "type": "list",
          "config": {
            "selectText": "Select ...",
            "list": []
          }
        }
      },
      "birthday": {
        "title": "Birthday"
      }
    },
    "delete": {
      "confirmDelete": true
    },
    "add": {
      "confirmCreate": true
    },
    "edit": {
      "confirmSave": true
    },
    "actions": {
      "add": true,
      "edit": true,
      "delete": true
    },
    "mode": "internal"
  };
  source;

/*    [
     {
       "id": 1,
       "email": "danielle_91@example.com",
       "userName": "Danielle Kennedy"
     },
     {
       "id": 2,
       "email": "russell_88@example.com",
       "userName": "Russell Payne"
     },
     {
       "id": 3,
       "email": "brenda97@example.com",
       "userName": "Brenda Hanson"
     },
     {
       "id": 4,
       "email": "nathan-85@example.com",
       "userName": "Nathan Knight"
     }
   ];*/

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    debugger;
    this.source =
      this.http.get<Person[]>(this.root + "users", httpOptions);
  }

}
