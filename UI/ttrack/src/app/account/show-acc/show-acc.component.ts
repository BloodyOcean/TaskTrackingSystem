import { Component, OnInit } from '@angular/core';
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

  userRole: string[];

  constructor(private service:SharedService, private uservice:UserService) { 
    this.userRole = uservice.getUser(localStorage.getItem('token')).role;
  }

  AccountsList:any=[];

  ngOnInit(): void {
    this.refreshProjectList();
  }

  addClick(dataItem) {
    this.acc = dataItem.UserId;

    this.ModalTitle = "Add new assignment";
    this.ActivateAddEditProjComp = true;
  }

  closeClick() {
    this.ActivateAddEditProjComp = false;
    this.refreshProjectList();
  }

  refreshProjectList() {
    this.service.getAccountsList().subscribe(data => {this.AccountsList = data;})
  }

}
