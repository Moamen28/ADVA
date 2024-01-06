import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiBaseUrl = 'https://localhost:7273/api/Employees';

  constructor(private http: HttpClient) {}

  // Get all employees
  getAllEmployees(): Observable<any> {
    return this.http.get(`${this.apiBaseUrl}/GetAllEmployees`);
  }

  // Add new employee
  addEmployee(employee: any): Observable<any> {
    return this.http.post(`${this.apiBaseUrl}/AddEmployee`, employee);
  }

  // Get employee by ID
  getEmployeeById(id: number): Observable<any> {
    return this.http.get(`${this.apiBaseUrl}/GetEmployeeById/${id}`);
  }

  // Update employee
  updateEmployee(id: number, employee: any): Observable<any> {
    return this.http.put(`${this.apiBaseUrl}/UpdateEmployee/${id}`, employee);
  }

  // Delete employee
  deleteEmployee(id: number): Observable<any> {
    return this.http.delete(`${this.apiBaseUrl}/RemoveEmployee/${id}`);
  }
}