import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  private apiBaseUrl = 'https://localhost:7273/api/Departments';

  constructor(private http: HttpClient) {}

  // Get all departments
  getAllDepartments(): Observable<any> {
    return this.http.get(`${this.apiBaseUrl}/GetAllDepartments`);
  }

  // Get department by id
  getDepartmentById(id: number): Observable<any> {
    return this.http.get(`${this.apiBaseUrl}/GetDepartmentById/${id}`);
  }

  // Add a new department
  addDepartment(department: any): Observable<any> {
    return this.http.post(`${this.apiBaseUrl}/AddDepartment`, department);
  }

  // Update a department
  updateDepartment(id: number, department: any): Observable<any> {
    return this.http.put(`${this.apiBaseUrl}/UpdateDepartmentById/${id}`, department);
  }

  // Delete a department
  deleteDepartment(id: number): Observable<any> {
    return this.http.delete(`${this.apiBaseUrl}/RemoveDepartmentById/${id}`);
  }
}
