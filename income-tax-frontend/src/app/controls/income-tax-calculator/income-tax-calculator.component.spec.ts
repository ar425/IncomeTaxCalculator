import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import { IncomeTaxCalculatorComponent } from './income-tax-calculator.component';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Component, Input } from '@angular/core';
import { By } from '@angular/platform-browser';
import { createSpyFromClass, Spy } from 'jasmine-auto-spies';
import { IncomeTaxService } from '../services/income-tax.service';
import { TaxBreakdown } from '../../core/models/tax-breakdown';
import { of } from 'rxjs';

// Dummy component
@Component({
  selector: 'app-main-content-wrapper',
  template: '<ng-content></ng-content>',
})
class DummyMainContentWrapperComponent {}

@Component({ selector: 'app-form-error', template: '' })
class DummyFormErrorComponent {
  @Input() form: any;
}

describe('Component: IncomeTaxCalculatorComponent', () => {
  let fixture: ComponentFixture<IncomeTaxCalculatorComponent>;
  let component: IncomeTaxCalculatorComponent;
  let incomeTaxServiceSpy: Spy<IncomeTaxService>;
  let routerSpy: Spy<Router>;

  beforeEach(async () => {
    let mockTaxBreakdown = new TaxBreakdown();
    mockTaxBreakdown.grossAnnualSalary = 10000;
    incomeTaxServiceSpy = createSpyFromClass(IncomeTaxService);
    incomeTaxServiceSpy.calculateIncomeTax.and.returnValue(
      of(mockTaxBreakdown)
    );

    routerSpy = createSpyFromClass(Router);

    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      declarations: [
        IncomeTaxCalculatorComponent,
        DummyMainContentWrapperComponent,
        DummyFormErrorComponent,
      ],
      providers: [
        { provide: IncomeTaxService, useValue: incomeTaxServiceSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomeTaxCalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should call calculateIncomeTax with form value', fakeAsync(() => {
    component.inputForm.setValue({ salaryAmount: 75000 });
    component.onSubmit();
    tick();
    expect(incomeTaxServiceSpy.calculateIncomeTax).toHaveBeenCalledWith(75000);
    expect(component.taxBreakdown).toEqual(
      jasmine.objectContaining({ grossAnnualSalary: 10000 })
    );
  }));

  it('should navigate to root when goBackToStart is called', () => {
    component.goBackToStart();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/']);
  });

  it('should render app-main-content-wrapper and app-form-error', () => {
    const wrapper = fixture.debugElement.query(By.css('app-main-content-wrapper'));
    const error = fixture.debugElement.query(By.css('app-form-error'));
    expect(wrapper).toBeTruthy();
    expect(error).toBeTruthy();
  });
});
