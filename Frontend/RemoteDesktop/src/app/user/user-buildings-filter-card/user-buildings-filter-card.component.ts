import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-user-buildings-filter-card',
  templateUrl: './user-buildings-filter-card.component.html',
  styleUrls: ['./user-buildings-filter-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserBuildingsFilterCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
