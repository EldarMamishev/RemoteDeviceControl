import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {Router} from '@angular/router';
import {DeviceDetailsModel} from '../../view-models/device-details.model';
import {CONNECTION_PATH} from '../../constants';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {DeviceFieldListModel} from '../../view-models/device-field-list.model';
import {UserDetailsModel} from '../../view-models/user-details.model';

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
@Component({
  selector: 'app-profile-card',
  templateUrl: './profile-card.component.html',
  styleUrls: ['./profile-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProfileCardComponent implements OnInit {
  data: UserDetailsModel;
  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.load();
  }

  load() {
    const methodUrl = CONNECTION_PATH + '/Person/GetUserInfo';
    const id = localStorage.getItem('rdc_user');
    let params = new HttpParams();
    params = params.append('userId', id);

    this.http.get<UserDetailsModel>(methodUrl, {
      params: params
    }).subscribe(data => this.data = data);
  }

  save() {
    console.log(this.data);
    const methodUrl = CONNECTION_PATH + '/Person/UpdateUser';
    let k;

    this.http.post(methodUrl, this.data, httpOptions).subscribe(data => k = data);

    const role = localStorage.getItem('rdc_role');
    if (role != undefined && role != '') {
      this.router.navigate(['./profile']);
    }
  }

  onExitClicked(): void {
    localStorage.removeItem('rdc_role');
    localStorage.removeItem('rdc_token');
    localStorage.removeItem('rdc_user');
    localStorage.removeItem('rdc_userName');
    this.router.navigate(['./']);
  }

}
