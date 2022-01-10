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

  addProject() {
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

    console.log(this.Status);

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
