import { Component, OnInit } from '@angular/core';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-department-managment',
  templateUrl: './department-managment.component.html',
  styleUrls: ['./department-managment.component.css']
})
export class DepartmentManagmentComponent {
  departments: any[] = [];
  newDepartment = { name: '', managerId: null }; // Placeholder for new department data
  selectedDepartment: any;
  constructor(private departmentService: DepartmentService) {}

   loadDepartments() {
    this.departmentService.getAllDepartments().subscribe(
      data => {
        this.departments = data;
      },
      error => {
        console.error('There was an error!', error);
      }
    );
  }
  addNewDepartment() {
    this.departmentService.addDepartment(this.newDepartment).subscribe(
      (response) => {
        console.log(response);
        this.loadDepartments();  
      },
      (error) => console.error(error)
    );
  }

  deleteDepartment(id: number) {
    this.departmentService.deleteDepartment(id).subscribe(
      () => {
        console.log("Department deleted successfully!");
        this.loadDepartments();  
      },
      (error) => console.error(error)
    );
  }
  selectDepartmentForUpdate(dept: any) {
  
    this.selectedDepartment = { ...dept };
  }
  updateDepartment() {
    if(!this.selectedDepartment) return; // Ensure there's a department selected
    
    this.departmentService.updateDepartment(this.selectedDepartment.id, this.selectedDepartment).subscribe(
      () => {
        console.log("Department updated successfully!");
        this.loadDepartments();  // Reload the departments to reflect the changes
        this.selectedDepartment = null;  
      },
      (error) => console.error(error)
    );
  }
}
