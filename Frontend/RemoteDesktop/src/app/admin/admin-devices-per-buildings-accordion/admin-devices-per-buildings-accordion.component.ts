import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-admin-devices-per-buildings-accordion',
  templateUrl: './admin-devices-per-buildings-accordion.component.html',
  styleUrls: ['./admin-devices-per-buildings-accordion.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminDevicesPerBuildingsAccordionComponent implements OnInit {
  settings =   {
    "columns": {
      "id": {
        "title": "ID",
        "filter": true
      },
      "name": {
        "title": "Name"
      },
      "type": {
        "title": "Type",
        "filter": {
          "type": "list",
          "config": {
            "selectText": "Select ...",
            "list": []
          }
        }
      }
    },
    "delete": {
      "confirmDelete": true
    },
    "add": {
      "confirmCreate": true
    },
    "edit": {
      "confirmSave": true
    },
    "actions": {
      "add": true,
      "edit": true,
      "delete": true
    },
    "mode": "internal"
  };
  source =   [
    {
      "id": 1,
      "email": "danielle_91@example.com",
      "name": "Danielle Kennedy",
      "type": "danielle.kennedy"
    },
    {
      "id": 2,
      "email": "russell_88@example.com",
      "name": "Russell Payne",
      "type": "russell.payne"
    },
    {
      "id": 3,
      "email": "brenda97@example.com",
      "name": "Brenda Hanson",
      "type": "brenda.hanson"
    },
    {
      "id": 4,
      "email": "nathan-85@example.com",
      "name": "Nathan Knight",
      "type": "nathan.knight"
    }
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
