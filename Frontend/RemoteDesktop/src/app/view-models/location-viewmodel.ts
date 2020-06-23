export class LocationViewmodel {
  id : number;
  city : string;
  country : string;
  name : string;

  getKey()
  {
    return this.id.toString().concat(":", this.name);
  }
}
