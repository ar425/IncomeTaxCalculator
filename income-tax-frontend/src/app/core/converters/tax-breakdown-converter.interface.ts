import { TaxBreakdownDto } from "../../api/income-tax-api";
import { TaxBreakdown } from "../models/tax-breakdown";

export interface ITaxBreakdownConverter {
    FromDto(dto: TaxBreakdownDto): TaxBreakdown;
}