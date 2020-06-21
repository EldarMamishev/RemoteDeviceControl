import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-new-tasks-action-card',
  templateUrl: './new-tasks-action-card.component.html',
  styleUrls: ['./new-tasks-action-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NewTasksActionCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
