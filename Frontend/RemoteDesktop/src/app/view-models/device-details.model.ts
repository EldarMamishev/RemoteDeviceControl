import {DeviceTypeModel} from './device-type-model';
import {DeviceFieldModel} from './device-field.model';

export class DeviceDetailsModel {
  id?: number;
  name: string;
  type: string;
  fields: DeviceFieldModel[];
}
