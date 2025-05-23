import { FormGroup, } from '@angular/forms';
import { TaxBreakdown } from '../../core/models/tax-breakdown';


export interface IIncomeTaxCalculatorComponent {

  inputForm: FormGroup;
  taxBreakdown: TaxBreakdown | undefined;

  showResults: boolean;

  onSubmit(): void;
  goBackToStart(): void;
  clearData(): void;
}
