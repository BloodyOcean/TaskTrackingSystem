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
  //Status:boolean;

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

  checkInputOnAdd(): boolean {
    if (this.Title == undefined || this.Description == undefined) {
      this.toastr.error('Some fields are empty', 'Error');
      return false;
    }

    if (this.creationd == undefined || this.closured == undefined || this.creationd > this.closured) {
      this.toastr.error('Time is incorrect', 'Error');
      return false;
    }

    if (this.project == undefined) {
      this.toastr.error('Please choose project', 'Error');
      return false;
    }

    return true;
  }

  addAssignment() {
    if (!this.checkInputOnAdd()) {
      return;
    }

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
