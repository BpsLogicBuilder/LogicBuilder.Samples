import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { GridColumnDropdownFilterComponent } from './grid-column-dropdown-filter.component';

describe('GridColumnDropdownFilterComponent', () => {
  let component: GridColumnDropdownFilterComponent;
  let fixture: ComponentFixture<GridColumnDropdownFilterComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ GridColumnDropdownFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GridColumnDropdownFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
