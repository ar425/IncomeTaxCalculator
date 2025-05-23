import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TaxBreakdown } from '../../core/models/tax-breakdown';
import { IncomeTaxService } from '../services/income-tax.service';
import { IIncomeTaxCalculatorComponent } from './income-tax-calculator.component.interface';

@Component({
  selector: 'app-income-tax-calculator',
  templateUrl: './income-tax-calculator.component.html',
  styleUrl: './income-tax-calculator.component.scss',
})
export class IncomeTaxCalculatorComponent implements IIncomeTaxCalculatorComponent, OnInit {
  public inputForm!: FormGroup;
  public taxBreakdown: TaxBreakdown | undefined = undefined;

  public showResults: boolean = true;

  // thought about putting a translator pipe in to the html
  //but as it wasn't in the original spec I haven't done so

  constructor(
    private _incomeTaxService: IncomeTaxService,
    private _formBuilder: FormBuilder,
    private _router: Router
  ) {}

  public ngOnInit(): void {
    this.inputForm = this._formBuilder.group({
      salaryAmount: this._formBuilder.control(null, [Validators.required]),
    });
  }

  // For calls that take a longer time I would add a spinner
  // indicating to the user that their request is being processed
  public onSubmit(): void {
    this.inputForm.markAllAsTouched();
    if (this.inputForm.invalid) {
      return;
    }

    const salaryAmount = this.inputForm.value.salaryAmount;

    this._incomeTaxService.calculateIncomeTax(salaryAmount).subscribe({
      next: (result) => (this.taxBreakdown = result),
      error: (err) => console.error(err),
    });
  }

  public goBackToStart(): void {
    this._router.navigate(['/']);
  }

  public clearData(): void {
    this.taxBreakdown = undefined;
    this.inputForm.reset();
  }
}
