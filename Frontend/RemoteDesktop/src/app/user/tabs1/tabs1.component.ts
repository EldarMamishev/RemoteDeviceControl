import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'app-tabs1',
  templateUrl: './tabs1.component.html',
  styleUrls: ['./tabs1.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Tabs1Component implements OnInit {

  constructor(private translate: TranslateService) { }

  ngOnInit(): void {
  }

}
