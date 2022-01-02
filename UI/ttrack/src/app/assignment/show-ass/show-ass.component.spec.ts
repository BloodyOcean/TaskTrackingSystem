import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAssComponent } from './show-ass.component';

describe('ShowAssComponent', () => {
  let component: ShowAssComponent;
  let fixture: ComponentFixture<ShowAssComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowAssComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowAssComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
