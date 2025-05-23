import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import {
  CalculateIncomeTaxCommand,
  IncomeTaxClient,
} from '../../api/income-tax-api';
import { TaxBreakdownConverter } from '../../core/converters/tax-breakdown-converter';
import { TaxBreakdown } from '../../core/models/tax-breakdown';
import { IIncomeTaxService } from './income-tax.service.interface';

// This annotation is necessary so that
// this service can be used in the component as a dependency injection
@Injectable({ providedIn: 'root' })
export class IncomeTaxService implements IIncomeTaxService {

  constructor(
    private _incomeTaxClient: IncomeTaxClient,
    private _taxBreakdownConverter: TaxBreakdownConverter
  ) {}

  // Returning this as an observable because the incomeTaxClient method
  // is asynchronous, if treated synchronously the return value would be null/undefined
  public calculateIncomeTax(salary: number): Observable<TaxBreakdown> {
    let command = new CalculateIncomeTaxCommand();
    command.annualSalaryAmount = salary;

    return this._incomeTaxClient
      .calculate(command)
      .pipe(map((result) => this._taxBreakdownConverter.FromDto(result)));
  }
}
