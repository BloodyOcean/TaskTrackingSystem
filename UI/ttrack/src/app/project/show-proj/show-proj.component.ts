import { Component, OnInit } from '@angular/core';
import { SharedService } from "src/app/shared/shared.service";

@Component({
  selector: 'app-show-proj',
  templateUrl: './show-proj.component.html',
  styleUrls: ['./show-proj.component.css']
})
export class ShowProjComponent implements OnInit {

  constructor(private service:SharedService) { }

  ProjectList:any=[];
  
  ModalTitle:string;
  proj:any;
  detproj:number;
  ActivateAddEditProjComp:boolean = false;
  ActivatedDetails:boolean = false;

  ngOnInit(): void {
    this.refreshProjectList();
  }

  refreshProjectList() {
    this.service.getProjectsList().subscribe(data => {this.ProjectList = data;})
  }

  closeClick() {
    this.ActivateAddEditProjComp = false;
    this.ActivatedDetails = false;
    this.refreshProjectList();
  }

  editClick(dataItem) {
    this.ModalTitle = "Edit project";
    this.proj = dataItem;
    this.ActivateAddEditProjComp = true;
  }

  detailsClick(dataItem) {
    //console.log(dataItem);
    //this.ModalTitle = "Project History";
    this.detproj = dataItem.Id;
    console.log(this.detproj);
    this.ActivatedDetails = true;
  }

  addClick() {
    this.proj = {
      Id: 0,
      Title: "",
      Description: "",
      Status: "true"
    }
    this.ModalTitle = "Add project";
    this.ActivateAddEditProjComp = true;
  }

}
