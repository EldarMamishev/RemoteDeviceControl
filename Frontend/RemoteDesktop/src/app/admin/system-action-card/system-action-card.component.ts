import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-system-action-card',
  templateUrl: './system-action-card.component.html',
  styleUrls: ['./system-action-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SystemActionCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
