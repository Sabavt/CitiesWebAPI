import { Component } from '@angular/core';
import { City } from '../models/city';
import { CityService } from '../services/city.service';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent {
  cities: City[] = [];
  postCityForm: FormGroup;
  isPostCityFormSubmitted: boolean = false;

  putCityForm: FormGroup;

  editCityID: string | null = null;

  constructor(private citiesService: CityService) {
    this.postCityForm = new FormGroup({
      cityName: new FormControl(null, [Validators.required])
    });

    this.putCityForm = new FormGroup({
      cities: new FormArray([])
    });
  }

  ngOnInit() {
    this.loadCities();
  }

  get putCityFormArray() : FormArray {
    return this.putCityForm.get("cities") as FormArray;
  }

  private loadCities() {
    this.citiesService.getCities().subscribe({
      next: (response: City[]) => {
        this.cities = response;

        this.cities.forEach(city => {
          this.putCityFormArray.push(new FormGroup({
            cityID: new FormControl(city.cityID, [Validators.required]),
            cityName: new FormControl({ value: city.cityName, disabled: true}, [Validators.required]),
          }));
        });
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
