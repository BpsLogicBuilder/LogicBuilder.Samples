import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormFieldDropdownComponent } from './form-field-dropdown.component';

describe('FormFieldDropdownComponent', () => {
  let component: FormFieldDropdownComponent;
  let fixture: ComponentFixture<FormFieldDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormFieldDropdownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormFieldDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
