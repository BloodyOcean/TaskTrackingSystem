import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProjectComponent } from './project/project.component';
import { ShowProjComponent } from './project/show-proj/show-proj.component';
import { AddEditProjComponent } from './project/add-edit-proj/add-edit-proj.component';
import { AssignmentComponent } from './assignment/assignment.component';
import { ShowAssComponent } from './assignment/show-ass/show-ass.component';
import { AddEditAssComponent } from './assignment/add-edit-ass/add-edit-ass.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserService } from './shared/user.service';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    ProjectComponent,
    ShowProjComponent,
    AddEditProjComponent,
    AssignmentComponent,
    ShowAssComponent,
    AddEditAssComponent,
    UserComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true
    })
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
