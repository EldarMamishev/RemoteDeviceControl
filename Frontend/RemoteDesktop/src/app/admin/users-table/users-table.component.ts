import {Component, OnInit, ChangeDetectionStrategy} from '@angular/core';
import {Person} from 'src/app/view-models/people-viewmodel';
import {CONNECTION_PATH} from "../../constants";
import {HttpHeaders, HttpClient} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {showWarningOnce} from "tslint/lib/error";
import { DefaultEditor } from 'ng2-smart-table';
import {NumberInputEditorComponent} from "../../number-input-editor.component";

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UsersTableComponent implements OnInit {
  private root = CONNECTION_PATH + '/admin/';
  settings = {
    "columns": {
      "id": {
        title: "ID",
        filter: true,
        // type: 'number',
        editable: false,
        addable: false,
        /*editor: {
          type: 'custom',
          component: NumberInputEditorComponent,
        },*/
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
            list: [
              { value: 'Male', title: 'Male' },
              { value: 'Female', title: 'Female' },
            ],
          }
        },
        "editor": {
          "type": "list",
          "config": {
            "selectText": "Select ...",
            list: [
              { value: 'Male', title: 'Male' },
              { value: 'Female', title: 'Female' },
            ],
          }
        }
      },
      "birthday": {
        "title": "Birthday"
      },
    },
    "delete": {
      "confirmDelete": true
    },
    "add": {
      "confirmCreate": true
    },
    "actions": {
      "add": true,
      "edit": false,
      "delete": true
    },
    "mode": "internal"
  };
  source = [
  ];

  constructor(
    private http: HttpClient
  ) {
  }

  onCreate() {

  }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    debugger;
    var methodUrl = this.root + "GetAllUsers"
    this.http.get<Person[]>(methodUrl).subscribe(data => this.source = data);
  }

}

