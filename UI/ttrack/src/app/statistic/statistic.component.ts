import { Component, OnInit } from '@angular/core';
import { SharedService } from '../shared/shared.service';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styles: [
  ]
})
export class StatisticComponent implements OnInit {

  constructor(private service:SharedService) { }

  StatisticList:any = [];

  ngOnInit(): void {
    this.refreshProjectList();
  }

  refreshProjectList() {
    this.service.getStatisticList().subscribe(data => {this.StatisticList = data; console.log(this.StatisticList);})
  }


}
