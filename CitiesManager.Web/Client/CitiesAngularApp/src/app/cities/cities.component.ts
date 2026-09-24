import { Component } from '@angular/core';
import { City } from '../models/city';
import { CityService } from '../services/city.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent {
  cities: City[] = [];
  postCityForm: FormGroup;
  isPostCityFormSubmitted: boolean = false;

  constructor(private citiesService: CityService) {
    this.postCityForm = new FormGroup({
      cityName: new FormControl(null, [Validators.required])
    });
  }

  ngOnInit() {
    this.loadCities();
  }

  private loadCities() {
    this.citiesService.getCities().subscribe({
      next: (response: City[]) => {
        this.cities = response;
      },
      error: (error: any) => {
        console.log(error);
      },
      complete: () => { }
    });
  }

  get postCity_CityNameControl(): any {
    return this.postCityForm.controls['cityName'];
  }

  public postCitySubmitted() {
    this.isPostCityFormSubmitted = true;

    this.citiesService.postCity(this.postCityForm.value).subscribe({
      next: (response: City) => {
        console.log(response);

        this.loadCities();

        this.postCityForm.reset();

        this.isPostCityFormSubmitted = false;
      },
      error: (error: any) => {
        console.log(error);
      },
      complete: () => { }
    });
  }
}
