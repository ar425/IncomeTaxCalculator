import { UntypedFormGroup } from "@angular/forms";

  export interface IFormErrorComponent {
    form: UntypedFormGroup;
    controlName: string;
    errorType: string;
    message: string;
  
    hasError(): boolean;
  }
  