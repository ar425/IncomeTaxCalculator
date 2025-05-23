import { IncomeTaxService } from './income-tax.service';
import { TestBed } from '@angular/core/testing';
import { CalculateIncomeTaxCommand, IncomeTaxClient, TaxBreakdownDto } from '../../api/income-tax-api';
import { TaxBreakdownConverter } from '../../core/converters/tax-breakdown-converter';
import { TaxBreakdown } from '../../core/models/tax-breakdown';
import { of, throwError } from 'rxjs';

describe('Service: IncomeTaxService', () => {
    let service: IncomeTaxService;
    let mockIncomeTaxClient: jasmine.SpyObj<IncomeTaxClient>;
    let mockTaxBreakdownConverter: jasmine.SpyObj<TaxBreakdownConverter>;
  
    beforeEach(() => {
      const incomeTaxClientSpy = jasmine.createSpyObj('IncomeTaxClient', ['calculate']);
      const taxBreakdownConverterSpy = jasmine.createSpyObj('TaxBreakdownConverter', ['FromDto']);
  
      TestBed.configureTestingModule({
        providers: [
          IncomeTaxService,
          { provide: IncomeTaxClient, useValue: incomeTaxClientSpy },
          { provide: TaxBreakdownConverter, useValue: taxBreakdownConverterSpy }
        ]
      });
  
      service = TestBed.inject(IncomeTaxService);
      mockIncomeTaxClient = TestBed.inject(IncomeTaxClient) as jasmine.SpyObj<IncomeTaxClient>;
      mockTaxBreakdownConverter = TestBed.inject(TaxBreakdownConverter) as jasmine.SpyObj<TaxBreakdownConverter>;
    });
  
    describe('calculateIncomeTax', () => {

    
      it('should call calculate on IncomeTaxClient and convert the result using TaxBreakdownConverter', (done) => {
        const mockSalary = 60000;
        const mockCommand = new CalculateIncomeTaxCommand();
        mockCommand.annualSalaryAmount = mockSalary;
        let taxBreakdown = new TaxBreakdown();
        let taxBreakdownDto = new TaxBreakdownDto();
  
        const mockDtoResponse = taxBreakdownDto;
        const mockConvertedResult: TaxBreakdown =  taxBreakdown;
  
        mockIncomeTaxClient.calculate.and.returnValue(of(mockDtoResponse));
        mockTaxBreakdownConverter.FromDto.and.returnValue(mockConvertedResult);
  
        service.calculateIncomeTax(mockSalary).subscribe(result => {
          expect(mockIncomeTaxClient.calculate).toHaveBeenCalledWith(jasmine.objectContaining({
            annualSalaryAmount: mockSalary
          }));
          expect(mockTaxBreakdownConverter.FromDto).toHaveBeenCalledWith(mockDtoResponse);
          expect(result).toEqual(mockConvertedResult);
          done();
        });
      });


      it('should handle errors from IncomeTaxClient gracefully', (done) => {
        const mockSalary = 50000;
        const error = new Error('Network error');
      
        mockIncomeTaxClient.calculate.and.returnValue(throwError(() => error));
      
        service.calculateIncomeTax(mockSalary).subscribe({
          next: () => fail('Expected an error, but got a result'),
          error: err => {
            expect(err).toBe(error);
            done();
          }
        });
      });
    });
  });
