import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProjectModel } from 'src/app/models/project.model';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-add-edit-acc',
  templateUrl: './add-edit-acc.component.html',
  styles: [
  ]
})
export class AddEditAccComponent implements OnInit {

  @Input() acc:number;
  Id:string;
  Title:string;
  Description:string;
  Creation:string;
  Closure: string;
  Status:boolean;

  ProjectList:any=[];
  project:any;

  creationd:any;
  closured:any;

  constructor(private service:SharedService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.refreshProjectList();
  }

  refreshProjectList(): void {
    this.service.getProjectsList().subscribe(data => {this.ProjectList = data;});
  }

  addAssignment() {

    console.log(this.project);
    console.log(this.creationd);
    console.log(this.closured);

    var item = {
      Title: this.Title,
      Description: this.Description,
      AssignmentStatusId: 1, //new
      EmployeeId: this.acc.toString(),
      ProjectID: this.project,
      CreationDate: this.creationd,
      ClosureDate: this.closured
    }

    this.service.addAssignment(item).subscribe(res => {this.toastr.success("Task added succsessfuly", "Success")});
  }

}
