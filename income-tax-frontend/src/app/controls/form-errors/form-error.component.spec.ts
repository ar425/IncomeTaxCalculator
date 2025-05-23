import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormErrorComponent } from './form-error.component';
import { ReactiveFormsModule, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

describe('Component: FormErrorComponent', () => {
  let component: FormErrorComponent;
  let fixture: ComponentFixture<FormErrorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FormErrorComponent],
      imports: [ReactiveFormsModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormErrorComponent);
    component = fixture.componentInstance;

    // Setup a test form
    component.form = new UntypedFormGroup({
      testControl: new UntypedFormControl('', [Validators.required])
    });
    component.controlName = 'testControl';
    component.errorType = 'required';
    component.message = 'This field is required';

    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should return false if control is valid', () => {
    component.form.get('testControl')?.setValue('Valid Input');
    component.form.get('testControl')?.markAsTouched();
    expect(component.hasError()).toBeFalse();
  });

  it('should return false if control is untouched', () => {
    component.form.get('testControl')?.setValue('');
    // not touched
    expect(component.hasError()).toBeFalse();
  });

  it('should return true if control is touched and has the expected error', () => {
    const control = component.form.get('testControl');
    control?.setValue('');
    control?.markAsTouched();
    expect(component.hasError()).toBeTrue();
  });

});
