import { Injectable } from '@angular/core';
import { City } from "../models/city"

@Injectable({
  providedIn: 'root'
})
export class CityService {

  cities: City[] = [];

  constructor() {
    this.cities = [
      new City("200", "New York"),
      new City("201", "London"),
      new City("202", "Berlin"),
      new City("203", "Hong kong")
    ];

  }

  public getCities(): City[] {
    return this.cities;
  }
}
