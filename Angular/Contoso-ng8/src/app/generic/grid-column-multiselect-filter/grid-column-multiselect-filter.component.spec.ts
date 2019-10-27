import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GridColumnMultiselectFilterComponent } from './grid-column-multiselect-filter.component';

describe('GridColumnMultiselectFilterComponent', () => {
  let component: GridColumnMultiselectFilterComponent;
  let fixture: ComponentFixture<GridColumnMultiselectFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GridColumnMultiselectFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GridColumnMultiselectFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
