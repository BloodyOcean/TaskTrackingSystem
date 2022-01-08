import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-show-ass',
  templateUrl: './show-ass.component.html',
  styleUrls: ['./show-ass.component.css']
})
export class ShowAssComponent implements OnInit {

  constructor(private service:SharedService) { }

  ngOnInit(): void {
    this.refreshAssignmentList();
  }

  AssignmentList:any=[];

  refreshAssignmentList() {
    this.service.getAssignmentsList().subscribe(data => {this.AssignmentList = data;})
  }

}
