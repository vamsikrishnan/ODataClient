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
    public Odatares: string;
    public Odataresults: string;
    public OdataserverResults: string;
    public SearchName: string;
    
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string){
    }

    public getRecords()
    {
        this.http.get(this.baseUrl + 'api/ODataClient/GetPeople').subscribe(result => {
            this.OdataserverResults = JSON.stringify(result) as string;
        }, error => console.error(error));

    }

    public getAllRecords() {
        this.http.get("http://services.odata.org/v4/TripPinServiceRW/People").subscribe(result => {
            this.Odataresults = JSON.stringify(result) as string;
        }, error => console.error(error));
    }

    public getOdata() {
        this.http.get("http://services.odata.org/v4/TripPinServiceRW/People('"+ this.SearchName  +"')").subscribe(result => {
            this.Odatares = JSON.stringify(result) as string;
        }, error => console.error(error));
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