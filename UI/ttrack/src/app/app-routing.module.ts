import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssignmentComponent } from './assignment/assignment.component';
import { ProjectComponent } from "./project/project.component";
import { AuthGuard } from './auth/auth.guard';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';
import { AccountComponent } from './account/account.component';
import { HasRoleGuard } from './has-role.guard';

const routes: Routes = [
  {
    path:'',
    redirectTo:'/user/login',
    pathMatch:'full'
  },
  {
    path:'user',
    component:UserComponent, 
    children: [
      {path:'registration', component: RegistrationComponent},
      {path:'login', component: LoginComponent}
    ]
  },
  {
    path:'home', 
    component:HomeComponent,
    children: [
      {path:'assignments', component: AssignmentComponent},
      {path:'projects', component: ProjectComponent, canActivate: [HasRoleGuard], data: {role: ['manager', 'admin']}},
      {path:'accounts', component: AccountComponent, canActivate: [HasRoleGuard], data: {role: ['admin', 'manager']}}
    ], 
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
