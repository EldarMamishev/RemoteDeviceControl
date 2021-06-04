import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {AuthoriseTabsComponent} from '../authorise-tabs/authorise-tabs.component';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {TranslateService} from '@ngx-translate/core';
import {Person} from '../../view-models/people-viewmodel';
import {LoginViewModel} from '../../view-models/login-viewmodel';
import {CONNECTION_PATH} from '../../constants';
import {PersonLocalStorage} from '../../view-models/person-local-storage';
import {Router} from '@angular/router';
import {NgModel} from '@angular/forms';

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

@Component({
  selector: 'app-login-card',
  templateUrl: './login-card.component.html',
  styleUrls: ['./login-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginCardComponent implements OnInit {

  constructor(private http: HttpClient, private translate: TranslateService, private router: Router) { }

  loginValues = new LoginViewModel();

  ngOnInit(): void {
    debugger;
  }

  async login() {
    localStorage.clear();
    debugger;

    let methodUrl = CONNECTION_PATH + '/Authentication/login';
    await this.http.post<PersonLocalStorage>(methodUrl, this.loginValues, httpOptions).subscribe(data => {
      localStorage.setItem('rdc_user', data.id.toString());
      localStorage.setItem('rdc_userName', data.userName.toString());
      localStorage.setItem('rdc_role', data.role);
      localStorage.setItem('rdc_token', data.token);
      this.router.navigate([localStorage.getItem('rdc_role')]);
    });

  }
}
