import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: [
  ]
})
export class RegistrationComponent implements OnInit {
  
  constructor(public service:UserService, private toastr: ToastrService) { }

  ngOnInit(): void {

    this.service.formModel.reset();
  }

  onSubmit() {
    this.service.register().subscribe({
      complete: () => {
        this.service.formModel.reset();
        this.toastr.success('New user created!', 'Registration successful.');
      }, 
      error: () => {
        this.toastr.error('Username is already taken','Registration failed.');
      }  
    }
    );

  }

}
