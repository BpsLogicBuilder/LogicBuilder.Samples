import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericEditComponent } from './generic-edit.component';

describe('GenericEditComponent', () => {
  let component: GenericEditComponent;
  let fixture: ComponentFixture<GenericEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenericEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenericEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
