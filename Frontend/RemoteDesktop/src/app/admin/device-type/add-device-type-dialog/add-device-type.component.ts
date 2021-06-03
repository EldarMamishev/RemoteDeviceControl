import {Component, OnInit, ChangeDetectionStrategy, Inject} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {Router} from '@angular/router';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FieldModel} from '../../../view-models/field-model';
import {FormArray, FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {CONNECTION_PATH} from '../../../constants';
import {HttpClient, HttpHeaders} from '@angular/common/http';

const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
@Component({
  selector: 'app-add-device-type',
  templateUrl: './add-device-type.component.html',
  styleUrls: ['./add-device-type.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AddDeviceTypeComponent implements OnInit {

  form: FormGroup;
  formArray: FormArray;
  formControl: FormControl;

  constructor(
    private http: HttpClient,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<AddDeviceTypeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string) {}

  ngOnInit(): void {
    const field = new FieldModel();
    field.name = 'test';
    field.type = 'int';
    this.form = this.formBuilder.group({
      name: this.formBuilder.control('name'),
      fields: this.formBuilder.array([
        this.formBuilder.group({
          name: this.formBuilder.control('First'),
          type: this.formBuilder.control('int'),
          possibleValues: this.formBuilder.array([])
        })
      ])
    });
    this.formArray = this.form.get('fields') as FormArray;
    this.formControl = this.form.get('name') as FormControl;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  add() {
    this.formArray.push(
      this.formBuilder.group({
        name: this.formBuilder.control('sectest'),
        type: this.formBuilder.control('int'),
        possibleValues: this.formBuilder.array([])
      })
    );
  }

  save() {
    console.log(this.form.value);
    const methodUrl = CONNECTION_PATH + '/DeviceType/AddDeviceType';
    let k;

    this.http.post(methodUrl, this.form.value, httpOptions).subscribe(data => k = data);
    this.dialogRef.close();
  }

  getVariants(q) {
    return q.get('possibleValues').controls as FormArray;
  }
}
