import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styles: [
  ]
})
export class DetailsComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() detproj:number;
  HistoryList:any = [];
  AssignmentList:any =[];
  CurrentAssignment:any;

  ngOnInit(): void {
    //console.log(this.detproj);
    this.refreshHistoryList();
  }

  refreshHistoryList() {
    console.log(this.detproj);
    this.service.getHistoryList(this.detproj).subscribe(data => {
      this.HistoryList = data;
      for (let item of this.HistoryList) {
        this.service.getAssignmentById(item.AssignmentId).subscribe(p => {
          console.log(p); 
          this.CurrentAssignment = p; 
          item.Title = this.CurrentAssignment.Title;
        });

        this.service.getProjectByAssignmentId(item.AssignmentId).subscribe(p => {
          item.Project = p.Title;
        });

        this.service.getAccountByAssignmentId(item.AssignmentId).subscribe(p => {
          item.Employee = p.Name;
        });

      }
    })
  }

}
