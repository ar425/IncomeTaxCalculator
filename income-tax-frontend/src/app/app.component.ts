import { Component } from '@angular/core';
import { IAppComponent } from './app.component.interface';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements IAppComponent {
}
