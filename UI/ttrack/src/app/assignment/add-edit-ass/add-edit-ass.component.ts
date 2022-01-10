import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-add-edit-ass',
  templateUrl: './add-edit-ass.component.html',
  styleUrls: ['./add-edit-ass.component.css']
})
export class AddEditAssComponent implements OnInit {

  @Input() ass:any;
  status:any;
  comment:any;
  changed:any;

  constructor(private service:SharedService, private toastr:ToastrService) { }
  
  ngOnInit(): void {
    this.refreshStatusList();
  }

  StatusList:any=[];

  refreshStatusList() {
    this.service.getStatusList().subscribe(data => {this.StatusList = data;})
  }

  change() {
    console.log(this.status);
    console.log(this.ass);
    console.log(this.comment);
    
    this.ass.AssignmentStatusId = this.status;
    let val = {
      ChangeDate:this.changed,
      AssignmentId: this.ass.Id,
      Comment: this.comment
    }

    this.service.addHistory(val).subscribe(data => {this.toastr.success('Changes added!', 'Success');})
    this.service.updateAssignment(this.ass).subscribe(data => { this.toastr.success('Assignment changed!', 'Success');});

  }

}
