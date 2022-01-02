import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowProjComponent } from './show-proj.component';

describe('ShowProjComponent', () => {
  let component: ShowProjComponent;
  let fixture: ComponentFixture<ShowProjComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowProjComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowProjComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
