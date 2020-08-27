import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {TranslateService} from "@ngx-translate/core";
import {Router} from "@angular/router";

@Component({
  selector: 'app-profile-card',
  templateUrl: './profile-card.component.html',
  styleUrls: ['./profile-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProfileCardComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onExitClicked(): void {
    localStorage.removeItem('rdc_role');
    localStorage.removeItem('rdc_token');
    localStorage.removeItem('rdc_user');
    this.router.navigate(['./']);
  }

}
