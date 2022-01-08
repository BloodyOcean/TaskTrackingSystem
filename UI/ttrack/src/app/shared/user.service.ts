import { ThrowStmt } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { UserModel } from '../models/user.model';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly BaseURI = 'https://localhost:44356/api';
  user: UserModel;

  constructor(private fb:FormBuilder, private http:HttpClient, private router:Router) {

  }

  formModel = this.fb.group({
    Email:['', [Validators.email, Validators.required]],
    Name:['', Validators.required],
    Passwords: this.fb.group({
      Password:['', [Validators.required, Validators.minLength(5)]],
      PasswordConfirm:['', [Validators.required, Validators.minLength(5)]], 
    })
  })

  register() {
    var body = {
      Name: this.formModel.value.Name,
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password,
      PasswordConfirm: this.formModel.value.Passwords.PasswordConfirm
    };
    
    return this.http.post(this.BaseURI + '/accounts/register', body);
  }

  login(formData) {
    return this.http.post(this.BaseURI + '/accounts/logon', formData, {responseType: 'text'}).subscribe(response=>{
      localStorage.setItem('token', response);
      this.user = this.getUser(response);
      this.router.navigateByUrl('/home');
   });;
  }

  public getUser(token:string):UserModel {
    let a = JSON.parse(atob(token.split('.')[1]))["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    let b = JSON.parse(atob(token.split('.')[1]))["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]
    return new UserModel(a, b);    
  }
}
