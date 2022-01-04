import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent implements OnInit {

  formModel = {
    Email: '',
    Password: ''
  }

  constructor(private service: UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') != null)
    {
      this.router.navigateByUrl('/home');
    }
  }

  onSubmit(form:NgForm): void {

    this.service.login(form.value).subscribe(response=>{
      localStorage.setItem('token', response);
      this.router.navigateByUrl('/home');
   });

  }

}
