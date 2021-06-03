import {FieldModel} from "./field-model";

export class DeviceTypeModel {
  id? : number;
  name : string;
  fields: FieldModel[];
}
