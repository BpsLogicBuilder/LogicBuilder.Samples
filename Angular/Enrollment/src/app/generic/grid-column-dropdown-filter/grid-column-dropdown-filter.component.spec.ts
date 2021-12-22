import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GridColumnDropdownFilterComponent } from './grid-column-dropdown-filter.component';

describe('GridColumnDropdownFilterComponent', () => {
  let component: GridColumnDropdownFilterComponent;
  let fixture: ComponentFixture<GridColumnDropdownFilterComponent>;

  beforeEach(async(() => {
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
