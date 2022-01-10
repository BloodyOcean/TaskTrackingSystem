import { Component, OnInit } from '@angular/core';
import { SharedService } from '../shared/shared.service';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styles: [
  ]
})
export class HistoryComponent implements OnInit {
  userRole: string[];
  CurrentAssignment:any;

  constructor(private service:SharedService, private uservice:UserService) {
    this.userRole = this.uservice.getUser(localStorage.getItem('token')).role;
   }

  HistoryList:any=[];
  AssignmentList:any =[];
  

  ngOnInit(): void {
    this.userRole = this.uservice.getUser(localStorage.getItem('token')).role;
    this.refreshHistory();
  }



  refreshHistory() {
    if (this.userRole == undefined) {
      this.service.getEmployeeHistoryList().subscribe(data => {
        this.HistoryList = data;
        for (let item of this.HistoryList) {
          this.service.getAssignmentById(item.AssignmentId).subscribe(p => {; 
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
      });
    }
    else {
      this.service.getManagerHistoryList().subscribe(data => {
        this.HistoryList = data;
        for (let item of this.HistoryList) {
          this.service.getAssignmentById(item.AssignmentId).subscribe(p => {
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

}
