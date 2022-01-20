import { Component, OnInit } from '@angular/core';
import { ChartType } from 'angular-google-charts';
import { CovidService } from './Services/covid.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'UdemySignalRAngular';
  c = ChartType.AreaChart;
  columnNames = [
    'Tarih',
    'Istanbul',
    'Ankara',
    'Izmir',
    'Eskisehir',
    'Diyarbakir',
    'Trabzon',
  ];
  options: any = { legend: { position: 'Bottom' } };

  constructor(public covidService: CovidService) {}

  ngOnInit(): void {
    this.covidService.startConnection();
    this.covidService.startListener();
  }
}
