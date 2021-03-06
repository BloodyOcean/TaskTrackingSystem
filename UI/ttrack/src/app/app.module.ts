import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

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
import { SharedService } from './shared/shared.service';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './user/login/login.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { AccountComponent } from './account/account.component';
import { ShowAccComponent } from './account/show-acc/show-acc.component';
import { AddEditAccComponent } from './account/add-edit-acc/add-edit-acc.component';
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatInputModule } from "@angular/material/input";
import { MatNativeDateModule } from "@angular/material/core";
import { AddRoleComponent } from './account/add-role/add-role.component';
import { HistoryComponent } from './history/history.component';
import { StatisticComponent } from './statistic/statistic.component';
import { DetailsComponent } from './project/details/details.component';

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
    RegistrationComponent,
    LoginComponent,
    HomeComponent,
    AccountComponent,
    ShowAccComponent,
    AddEditAccComponent,
    AddRoleComponent,
    HistoryComponent,
    StatisticComponent,
    DetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    FormsModule
  ],
  providers: [UserService,{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
