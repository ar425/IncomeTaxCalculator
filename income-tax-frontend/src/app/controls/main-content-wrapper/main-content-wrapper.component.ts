import { Component } from '@angular/core';
import { IMainContentWrapperComponent } from './main-content-wrapper.component.interface';

@Component({
  selector: 'app-main-content-wrapper',
  templateUrl: './main-content-wrapper.component.html',
  styleUrl: './main-content-wrapper.component.scss'
})
// This is used as a way to re-use the basic template, making code more maintainable
export class MainContentWrapperComponent implements IMainContentWrapperComponent {
}
