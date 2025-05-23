import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ILandingPageComponent } from './landing-page.component.interface';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss'
})
export class LandingPageComponent implements ILandingPageComponent {

  constructor(private _router: Router){
  }
  
  public goToCalculator(): void{
     this._router.navigate(['/calculate']);
  }
}
