import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-add-edit-proj',
  templateUrl: './add-edit-proj.component.html',
  styleUrls: ['./add-edit-proj.component.css']
})
export class AddEditProjComponent implements OnInit {

  @Input() proj:any;
  Id:string;
  Title:string;
  Description:string;
  Creation:string;
  Closure: string;
  Status:boolean;
  creationd:any;
  closured:any;

  constructor(private service:SharedService, private toastr: ToastrService) { }

  ngOnInit(): void {
    console.log('here');
    this.Id=this.proj.Id;
    this.Title=this.proj.Title;
    this.Description = this.proj.Description;
  }

  checkInputOnAdd(): boolean {
    if (this.Title == '' || this.Description == '') {
      this.toastr.error('Some fields are empty', 'Error');
      return false;
    }

    if (this.creationd == undefined || this.closured == undefined || this.creationd > this.closured) {
      this.toastr.error('Time is incorrect', 'Error');
      return false;
    }

    return true;
  }

  checkInputOnEdit(): boolean {
    if (this.Status == undefined) {
      this.toastr.error('Some fields are empty', 'Error');
      return false;
    }

    return this.checkInputOnAdd();
  }

  addProject() {
    if (!this.checkInputOnAdd()) {
      return;
    }

    var item = {
      // Id: this.Id,
      Title: this.Title,
      Description: this.Description,
      CreationDate: this.creationd,
      ClosureDate: this.closured,
      Status: true
    }

    this.service.addProject(item).subscribe(res => {this.toastr.success("Item added succsessfuly", "Success")});
  }

  updateProject() {
    
    if (!this.checkInputOnEdit()) {
      return;
    }

    var item = {
      Id: this.Id,
      Title: this.Title,
      Description: this.Description,
      CreationDate: this.creationd,
      ClosureDate: this.closured,
      Status: this.Status
    }

    this.service.updateProject(item).subscribe(res => {this.toastr.success("Item updated succsessfuly", "Success")});
  }

}
