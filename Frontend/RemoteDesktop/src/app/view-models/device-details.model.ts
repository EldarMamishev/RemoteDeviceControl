import {DeviceTypeModel} from './device-type-model';
import {DeviceFieldModel} from './device-field.model';

export class DeviceDetailsModel {
  id?: number;
  busy: boolean;
  name: string;
  type: string;
  location: string;
  fields: DeviceFieldModel[];
}
