import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-admin-buildings-filter-card',
  templateUrl: './admin-buildings-filter-card.component.html',
  styleUrls: ['./admin-buildings-filter-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminBuildingsFilterCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
