import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly BaseURI = 'https://localhost:44356/api';

  constructor(private http:HttpClient) { }

  getAssignmentsList():Observable<any[]>
  {
    return this.http.get<any>(this.BaseURI + '/assignments/employee');
  }

  getAllAssignments():Observable<any[]> {
    return this.http.get<any>(this.BaseURI + '/assignments');
  }

  getAssignmentById(val:any):Observable<any[]> {
    return this.http.get<any>(this.BaseURI + '/assignments/' + val);
  }

  getStatusList():Observable<any[]> {
    return this.http.get<any>(this.BaseURI + '/statuses')
  }

  getEmployeeHistoryList():Observable<any[]> {
    return this.http.get<any>(this.BaseURI + '/histories/employee/project')
  }

  getManagerHistoryList():Observable<any[]> {
    return this.http.get<any>(this.BaseURI + '/histories/manager/project')
  }

  addRoles(val:any)
  {
    return this.http.post(this.BaseURI + '/accounts/AssignUserToRole', val);
  }

  addAssignment(val:any)
  {
    return this.http.post(this.BaseURI + '/assignments', val);
  }

  updateAssignment(val:any) {
    return this.http.put(this.BaseURI + '/assignments', val);
  }

  addHistory(val:any) {
    return this.http.post(this.BaseURI + '/histories', val);
  }

  updateProject(val:any)
  {
    return this.http.put(this.BaseURI + '/projects', val);
  }

  addProject(val: any) {
    return this.http.post(this.BaseURI + '/projects', val);
  }

  deleteAssignment(val:any)
  {
    return this.http.delete(this.BaseURI + '/assignments/' + val);
  }

  getProjectsList():Observable<any[]>
  {
    return this.http.get<any>(this.BaseURI + '/projects/employee');
  }

  deleteAccount(val:any) {
    return this.http.delete(this.BaseURI + '/accounts?id=' + val);
  }

  getAccountsList():Observable<any[]> {
    return this.http.get<any>(this.BaseURI + '/accounts');
  }

  getStatisticList() {
    return this.http.get<any>(this.BaseURI + '/statistics/manager');
  }

  getHistoryList(val:any) {
    return this.http.get<any>(this.BaseURI + '/histories/project/' + val);
  }

  getProjectByAssignmentId(val):Observable<any> {
    return this.http.get<any>(this.BaseURI + '/projects/assignment/' + val);
  }

  getAccountByAssignmentId(val):Observable<any> {
    return this.http.get<any>(this.BaseURI + '/accounts/assignment/' + val);
  }


}
