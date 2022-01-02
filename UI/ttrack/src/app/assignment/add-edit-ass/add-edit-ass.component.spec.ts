import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditAssComponent } from './add-edit-ass.component';

describe('AddEditAssComponent', () => {
  let component: AddEditAssComponent;
  let fixture: ComponentFixture<AddEditAssComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditAssComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditAssComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
