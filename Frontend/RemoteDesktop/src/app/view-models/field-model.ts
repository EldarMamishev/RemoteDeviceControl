import {PossibleValueModel} from "./possible-value-model";

export class FieldModel {
  id? : number;
  name : string;
  type : string;
  possibleValues: PossibleValueModel[];
}
