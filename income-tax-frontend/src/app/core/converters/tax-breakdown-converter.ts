import { Injectable } from "@angular/core";
import { TaxBreakdownDto } from "../../api/income-tax-api";
import { TaxBreakdown } from "../models/tax-breakdown";
import { ITaxBreakdownConverter } from "./tax-breakdown-converter.interface";

@Injectable({ providedIn: 'root' })
export class TaxBreakdownConverter implements ITaxBreakdownConverter {

    // Translating the dto to an angular object
    public FromDto(dto: TaxBreakdownDto): TaxBreakdown{
        var taxBreakdown = new TaxBreakdown();
        taxBreakdown.annualTaxPaid = dto.annualTaxPaid;
        taxBreakdown.grossAnnualSalary = dto.grossAnnualSalary;
        taxBreakdown.grossMonthlySalary = dto.grossMonthlySalary;
        taxBreakdown.monthlyTaxPaid = dto.monthlyTaxPaid;
        taxBreakdown.netAnnualSalary = dto.netAnnualSalary;
        taxBreakdown.netMonthlySalary = dto.netMonthlySalary;
        return taxBreakdown;
    }
}