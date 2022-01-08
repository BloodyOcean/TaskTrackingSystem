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
    return this.http.get<any>(this.BaseURI + '/assignments/employee')
  }

  addAssignment(val:any)
  {
    return this.http.post(this.BaseURI + '/assignments', val);
  }

  updateAssignment(val:any)
  {
    return this.http.put(this.BaseURI + '/assignments', val);
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

  getAccountsList():Observable<any[]> {
    return this.http.get<any>(this.BaseURI + '/accounts');
  }

}
