import { ThrowStmt } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";


@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly BaseURI = 'https://localhost:44356/api';

  constructor(private fb:FormBuilder, private http:HttpClient) { }

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
    return this.http.post(this.BaseURI + '/accounts/logon', formData, {responseType: 'text'});
  }
}
