import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {RegisterFields} from "../../view-models/register-fields";
import {CONNECTION_PATH} from "../../constants";
import {PersonLocalStorage} from "../../view-models/person-local-storage";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {TranslateService} from "@ngx-translate/core";
import {Router} from "@angular/router";

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

@Component({
  selector: 'app-create-user-form',
  templateUrl: './create-user-form.component.html',
  styleUrls: ['./create-user-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateUserFormComponent implements OnInit {

  constructor(private http: HttpClient, private translate: TranslateService, private router: Router) { }

  fields = new RegisterFields()

  ngOnInit(): void {
  }

  async onRegister(){
    localStorage.clear();
    debugger;

    var methodUrl = CONNECTION_PATH + "/Authentication/register"
    await this.http.post<PersonLocalStorage>(methodUrl, this.fields, httpOptions).subscribe(data =>
    {
      localStorage.setItem('rdc_user', data.id.toString());
      localStorage.setItem('rdc_role', data.role);
      localStorage.setItem('rdc_token', data.token);
      this.router.navigate([localStorage.getItem('rdc_role')]);
    });

  }

}
