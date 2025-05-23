import { Component, Input } from "@angular/core";
import { UntypedFormGroup } from "@angular/forms";
import { IFormErrorComponent } from "./form-error.component.interface";

@Component({
    selector: 'app-form-error',
    templateUrl: './form-error.component.html',
    styleUrl: './form-error.component.scss',

  })

  // general class that can be used throughout the application 
  // for creating custom form errors
  export class FormErrorComponent implements IFormErrorComponent {
    @Input() form!: UntypedFormGroup;
    @Input() controlName!: string;
    @Input() errorType!: string;
    @Input() message!: string;
  
    hasError(): boolean {
      const control = this.form.get(this.controlName);
      return !!(control?.hasError(this.errorType) && control.touched);
    }
  }
  