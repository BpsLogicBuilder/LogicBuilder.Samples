import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DisplayDropdownValueComponent } from './display-dropdown-value.component';

describe('DisplayDropdownValueComponent', () => {
  let component: DisplayDropdownValueComponent;
  let fixture: ComponentFixture<DisplayDropdownValueComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DisplayDropdownValueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayDropdownValueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
