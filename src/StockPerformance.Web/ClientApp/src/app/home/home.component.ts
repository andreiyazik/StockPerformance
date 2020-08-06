import { Component } from '@angular/core';
import { ChartsModule } from '@progress/kendo-angular-charts';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public periods: Array<string> = ['Week', 'Day', 'Month'];
  public granularities: Array<string> = ['Daily', 'Hourly'];
}
