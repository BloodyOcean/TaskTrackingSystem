import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SharedService } from 'src/app/shared/shared.service';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-show-acc',
  templateUrl: './show-acc.component.html',
  styles: [
  ]
})
export class ShowAccComponent implements OnInit {

  ActivateAddEditProjComp:boolean = false;
  ModalTitle:string;
  acc:any;

  modalType:boolean; //true - assigment, false - role

  userRole: string[];

  constructor(private service:SharedService, private uservice:UserService, private toastr:ToastrService) { 
    this.userRole = uservice.getUser(localStorage.getItem('token')).role;
  }

  AccountsList:any=[];

  ngOnInit(): void {
    this.refreshAccountList();
  }

  addClick(dataItem) {
    this.modalType = true;
    this.acc = dataItem.UserId;
    this.ModalTitle = "Add new assignment";
    this.ActivateAddEditProjComp = true;
  }

  roleClick(dataItem) {
    this.acc = dataItem;
    this.modalType = false;
    this.ModalTitle = "Add new role to user";
    this.ActivateAddEditProjComp = true;
  }

  delClick(dataItem) {
    this.service.deleteAccount(dataItem.UserId).subscribe(() => {this.toastr.success('Employee removed successfuly', 'Success!'); this.refreshAccountList();});
  }

  closeClick() {
    this.ActivateAddEditProjComp = false;
    this.refreshAccountList();
  }

  refreshAccountList() {
    this.service.getAccountsList().subscribe(data => {this.AccountsList = data;})
  }

}
