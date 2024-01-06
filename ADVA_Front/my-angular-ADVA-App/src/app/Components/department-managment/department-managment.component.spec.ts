import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentManagmentComponent } from './department-managment.component';

describe('DepartmentManagmentComponent', () => {
  let component: DepartmentManagmentComponent;
  let fixture: ComponentFixture<DepartmentManagmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DepartmentManagmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DepartmentManagmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
