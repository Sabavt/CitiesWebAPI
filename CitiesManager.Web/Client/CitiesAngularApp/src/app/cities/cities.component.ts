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

  ngOnInit()
  {
    this.citiesService.getCities().subscribe({
      next : (response: City[]) =>
    {
      this.cities = response;
    },
      error : (error: any) => {
      console.log(error)
    },
      complete: () => { }
    });
  }
}
