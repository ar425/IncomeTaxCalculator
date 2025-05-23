import { Observable } from "rxjs";
import { TaxBreakdown } from "../../core/models/tax-breakdown";

export interface IIncomeTaxService {
    calculateIncomeTax(salary: number): Observable<TaxBreakdown>;
}