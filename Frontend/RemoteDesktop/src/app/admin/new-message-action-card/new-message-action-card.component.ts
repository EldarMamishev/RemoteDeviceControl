import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-new-message-action-card',
  templateUrl: './new-message-action-card.component.html',
  styleUrls: ['./new-message-action-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NewMessageActionCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
