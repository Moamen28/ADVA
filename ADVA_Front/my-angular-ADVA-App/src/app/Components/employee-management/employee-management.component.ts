import { Component, OnInit } from '@angular/core';
import {EmployeeService} from 'src/app/services/employee.service';

@Component({
  selector: 'app-department-managment',
  templateUrl: './employee-management.component.html',
  styleUrls: ['./employee-management.component.css']
})
export class EmployeeManagementComponent  {
  employees: any[] = [];
  newEmployee = {
    name: '', 
    salary: null,
    managerId: null,
    departmentId: null
  };
  selectedEmployee: any = null;
  constructor(private employeeService: EmployeeService) {}

  loadEmployees() {
    this.employeeService.getAllEmployees().subscribe(
      data => this.employees = data,
      error => console.error('There was an error!', error)
    );
  }
  selectEmployeeForUpdate(employee: any) {
    this.selectedEmployee = {...employee}; 
  }
  addEmployee() {
    this.employeeService.addEmployee(this.newEmployee).subscribe(
      response => {
        console.log("Employee added!", response);
        this.loadEmployees(); // Reload employees to include the new one
      },
      error => console.error("Error adding employee!", error)
    );
  }
  deleteEmployee(id: number) {
    

    this.employeeService.deleteEmployee(id).subscribe(
      () => {
        console.log("Employee deleted successfully!");
      },
      error => console.error("Error deleting employee!", error)
    );
  }
}