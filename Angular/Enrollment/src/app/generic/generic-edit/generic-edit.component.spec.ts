import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { GenericEditComponent } from './generic-edit.component';

describe('GenericEditComponent', () => {
  let component: GenericEditComponent;
  let fixture: ComponentFixture<GenericEditComponent>;

  beforeEach(waitForAsync(() => {
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
