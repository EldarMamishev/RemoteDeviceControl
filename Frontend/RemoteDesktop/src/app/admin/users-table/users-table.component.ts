import {Component, OnInit, ChangeDetectionStrategy} from '@angular/core';
import {Person} from 'src/app/view-models/people-viewmodel';
import {CONNECTION_PATH} from '../../constants';
import {HttpHeaders, HttpClient} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {showWarningOnce} from 'tslint/lib/error';
import { DefaultEditor } from 'ng2-smart-table';
import {NumberInputEditorComponent} from '../../number-input-editor.component';
import {TranslateService} from '@ngx-translate/core';

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UsersTableComponent implements OnInit {
  actionsTitle: string;
  userNameTitle: string;
  emailTitle: string;
  lastNameTitle: string;
  firstNameTitle: string;
  genderTitle: string;
  selectTitle: string;
  maleTitle: string;
  femaleTitle: string;
  editTitle: string;
  saveTitle: string;
  cancelTitle: string;
  deleteTitle: string;

  actionsSubscr = this.translate.get('Actions').subscribe((res: string) => {
    this.actionsTitle = res;
  });
  nameSubscr = this.translate.get('Username').subscribe((res: string) => {
    this.userNameTitle = res;
  });
  firstNameSubscr = this.translate.get('FirstName').subscribe((res: string) => {
    this.firstNameTitle = res;
  });
  lastNameSubscr = this.translate.get('LastName').subscribe((res: string) => {
    this.lastNameTitle = res;
  });
  emailSubscr = this.translate.get('Email').subscribe((res: string) => {
    this.emailTitle = res;
  });
  genderSubscr = this.translate.get('Gender').subscribe((res: string) => {
    this.genderTitle = res;
  });
  selectSubscr = this.translate.get('Select').subscribe((res: string) => {
    this.selectTitle = res;
  });
  maleSubscr = this.translate.get('Male').subscribe((res: string) => {
    this.maleTitle = res;
  });
  femaleSubscr = this.translate.get('Female').subscribe((res: string) => {
    this.femaleTitle = res;
  });
  editSubscr = this.translate.get('Edit').subscribe((res: string) => {
    this.editTitle = res;
  });
  saveSubscr = this.translate.get('Save').subscribe((res: string) => {
    this.saveTitle = res;
  });
  cancelSubscr = this.translate.get('Cancel').subscribe((res: string) => {
    this.cancelTitle = res;
  });
  deleteSubscr = this.translate.get('Delete').subscribe((res: string) => {
    this.deleteTitle = res;
  });

  private root = CONNECTION_PATH + '/person/';
  settings = {
    'columns': {
      'id': {
        title: 'ID',
        filter: true,
        // type: 'number',
        editable: false,
        addable: false,
        /*editor: {
          type: 'custom',
          component: NumberInputEditorComponent,
        },*/
      },
      'userName': {
        'title': this.userNameTitle,
        'filter': true
      },
      'discriminator': {
        'title': 'Role',
        'filter': true
      },
      'email': {
        'title': this.emailTitle,
        'filter': true
      },
      'firstName': {
        'title': this.firstNameTitle,
        'filter': true
      },
      'lastName': {
        'title': this.lastNameTitle,
        'filter': true
      }
    },
    'delete': {
      'confirmDelete': true,
      'deleteButtonContent': this.deleteTitle
    },
    'edit': {
      'confirmSave': true,
      'cancelButtonContent': this.cancelTitle,
      'editButtonContent': this.editTitle,
      'saveButtonContent': this.saveTitle
    },
    'actions': {
      'columnTitle': this.actionsTitle,
      'add': false,
      'edit': false,
      'delete': false
    },
    'mode': 'internal'
  };
  source = [
  ];

  constructor(
    private http: HttpClient, private translate: TranslateService
  ) {
  }

  onCreate() {

  }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    const methodUrl = this.root + 'GetAllUsers';
    this.http.get<Person[]>(methodUrl).subscribe(data => this.source = data);
  }

}

