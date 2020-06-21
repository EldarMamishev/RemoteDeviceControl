import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-message-action-card',
  templateUrl: './message-action-card.component.html',
  styleUrls: ['./message-action-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MessageActionCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
