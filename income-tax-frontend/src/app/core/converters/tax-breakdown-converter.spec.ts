import { TaxBreakdownConverter } from './tax-breakdown-converter';
import { TaxBreakdownDto } from '../../api/income-tax-api';
import { TaxBreakdown } from '../models/tax-breakdown';

describe('Converter: TaxBreakdownConverter', () => {
  let service: TaxBreakdownConverter;

  beforeEach(() => {
    service = new TaxBreakdownConverter();
  });

  it('should create the service', () => {
    expect(service).toBeTruthy();
  });

  it('should convert TaxBreakdownDto to TaxBreakdown correctly', () => {
    const dto = {
      annualTaxPaid: 12000,
      grossAnnualSalary: 60000,
      grossMonthlySalary: 5000,
      monthlyTaxPaid: 1000,
      netAnnualSalary: 48000,
      netMonthlySalary: 4000,
    } as TaxBreakdownDto;

    const result: TaxBreakdown = service.FromDto(dto);

    expect(result).toEqual(jasmine.any(TaxBreakdown));
    expect(result.annualTaxPaid).toBe(dto.annualTaxPaid);
    expect(result.grossAnnualSalary).toBe(dto.grossAnnualSalary);
    expect(result.grossMonthlySalary).toBe(dto.grossMonthlySalary);
    expect(result.monthlyTaxPaid).toBe(dto.monthlyTaxPaid);
    expect(result.netAnnualSalary).toBe(dto.netAnnualSalary);
    expect(result.netMonthlySalary).toBe(dto.netMonthlySalary);
  });

  it('should handle empty dto gracefully', () => {
    const dto = {
      annualTaxPaid: 0,
      grossAnnualSalary: 0,
      grossMonthlySalary: 0,
      monthlyTaxPaid: 0,
      netAnnualSalary: 0,
      netMonthlySalary: 0,
    } as TaxBreakdownDto;

    const result = service.FromDto(dto);

    expect(result.annualTaxPaid).toBe(0);
    expect(result.netMonthlySalary).toBe(0);
  });
});
