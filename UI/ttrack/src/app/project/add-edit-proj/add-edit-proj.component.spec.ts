import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditProjComponent } from './add-edit-proj.component';

describe('AddEditProjComponent', () => {
  let component: AddEditProjComponent;
  let fixture: ComponentFixture<AddEditProjComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditProjComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditProjComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
