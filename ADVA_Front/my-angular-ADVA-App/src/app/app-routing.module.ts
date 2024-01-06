import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartmentManagmentComponent } from './Components/department-managment/department-managment.component';
import { EmployeeManagementComponent } from './Components/employee-management/employee-management.component';

const routes: Routes = [
  {path:"departments",component:DepartmentManagmentComponent},
  {path:"employees",component:EmployeeManagementComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
