import { Component, Inject } from '@angular/core';
import { ChartsModule } from '@progress/kendo-angular-charts';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string) {
  }

  public periods = [
    {id: 1, name: "Last week"},
    {id: 2, name: "Last month"}
  ];

  public granularities = [
    {id: 0, name: "Daily"},
    {id: 1, name: "Intraday"},
  ];

  public symbol = "";
  public period = 1;
  public granularity = 0;

  public loading = false;
  public loaded = false;

  public isError = false;
  public errorMessage = "";

  public performance: PerformanceComparison;

  public getPerformance() {
    this.loading = true;
    this.loaded = false;
    this.isError = false;
    this.http.get<PerformanceComparisonResult>(this.apiUrl + 'stock/performance-comparison?symbol=' + 
      this.symbol + '&period=' + 
      this.period + '&granularity=' + 
      this.granularity)
    .subscribe(result => {
      if(result.error) {
        this.isError = true;
        this.errorMessage = result.error.errorMessage;
      } else {
        var response = result.response;
        if(response.dates.length > 7) {
          response.dates = [];
        } else {
          var dates = [];
          response.dates.forEach(date => {
            var parsedDate = new Date(parseInt(date) * 1000);
            dates.push(parsedDate.toDateString());
         });

         response.dates = dates;
        }
        this.performance = result.response;
        this.loaded = true;
      }
      this.loading = false;
    }, error => { 
      console.error(error);
      this.loading = false;
    });
  }
}

interface PerformanceComparisonResult {
  response: PerformanceComparison;
  error: Error
}

interface PerformanceComparison {
  dates: Array<string>,
  performanceResults: Array<Performance>
}

interface Performance {
  symbol: string,
  results: Float64Array
}

interface Error {
  errorMessage: string
}