import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-add-role',
  templateUrl: './add-role.component.html',
  styles: [
  ]
})
export class AddRoleComponent implements OnInit {

  constructor(private service:SharedService, private toastr:ToastrService) { }

  @Input() acc:any;
  roles:any;

  ngOnInit(): void {
  }

  addRoles() {

    let val = {
      Email: this.acc.Email,
      Roles: this.roles.split(', ')
    }

    this.service.addRoles(val).subscribe(res => {this.toastr.success("Roles added succsessfuly", "Success")});
  }

}
