import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericGridComponent } from './generic-grid.component';

describe('GenericGridComponent', () => {
  let component: GenericGridComponent;
  let fixture: ComponentFixture<GenericGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenericGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenericGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
