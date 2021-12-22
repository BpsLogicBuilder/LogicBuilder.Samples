import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormFieldMultiselectComponent } from './form-field-multiselect.component';

describe('FormFieldMultiselectComponent', () => {
  let component: FormFieldMultiselectComponent;
  let fixture: ComponentFixture<FormFieldMultiselectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormFieldMultiselectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormFieldMultiselectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
