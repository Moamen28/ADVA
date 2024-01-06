import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms'; // Import FormsModule here

import { DepartmentManagmentComponent } from './Components/department-managment/department-managment.component';
import { EmployeeManagementComponent } from './Components/employee-management/employee-management.component';

@NgModule({
  declarations: [
    AppComponent,
    DepartmentManagmentComponent,
    EmployeeManagementComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
