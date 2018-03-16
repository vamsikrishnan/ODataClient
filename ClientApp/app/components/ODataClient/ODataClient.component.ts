import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'ODataClient',
    templateUrl: './ODataClient.component.html'
})
export class ODataClientComponent {
    public forecasts: WeatherForecast[];
    public filter:Number ;
    public resultsLimit:Number;
    public filterValue:Number;
    public jResult:string;
    
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string){
    }

    QueryData(event:any){
        var url = this.baseUrl+ 'api/ODataClient?results='+ this.resultsLimit+'&filter=' + this.filter+'&filterValue=' + this.filterValue;    
        this.http.get(url).subscribe(result => {
            this.jResult = result.json() ;
        }, error => console.error(error));
    };

    };

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}