import { CommonModule } from "@angular/common";
import { provideHttpClient } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { FormErrorComponent } from "./form-errors/form-error.component";
import { IncomeTaxCalculatorComponent } from "./income-tax-calculator/income-tax-calculator.component";
import { LandingPageComponent } from "./landing-page/landing-page.component";
import { MainContentWrapperComponent } from "./main-content-wrapper/main-content-wrapper.component";

@NgModule({
    declarations: [
        MainContentWrapperComponent,
        IncomeTaxCalculatorComponent,
        LandingPageComponent,
        FormErrorComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        RouterModule,
        FormsModule,
        BrowserModule,
    ],
    exports: [
        MainContentWrapperComponent
    ],
    providers: [provideHttpClient()]
})

export class ControlsModule {}