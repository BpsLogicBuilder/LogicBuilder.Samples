import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { GenericDetailComponent } from './generic-detail.component';

describe('GenericDetailComponent', () => {
  let component: GenericDetailComponent;
  let fixture: ComponentFixture<GenericDetailComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ GenericDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenericDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
