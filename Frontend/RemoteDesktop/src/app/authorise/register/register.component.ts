import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {RegisterFields} from "../../view-models/register-fields";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
