import { Routes } from '@angular/router';
import { IncomeTaxCalculatorComponent } from './controls/income-tax-calculator/income-tax-calculator.component';
import { LandingPageComponent } from './controls/landing-page/landing-page.component';

export const routes: Routes = [
    {
        path: '',
        component: LandingPageComponent
    },
    {
        path: 'calculate',
        component: IncomeTaxCalculatorComponent
    }
];
