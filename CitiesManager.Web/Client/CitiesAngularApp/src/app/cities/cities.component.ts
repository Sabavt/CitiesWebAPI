import { Component } from '@angular/core';
import { City } from '../models/city';
import { CityService } from '../services/city.service';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent {
  cities: City[] = [];

  constructor(private citiesService: CityService)
  {

  }

  onInit()
  {
    this.cities = this.citiesService.getCities();
  }
}
