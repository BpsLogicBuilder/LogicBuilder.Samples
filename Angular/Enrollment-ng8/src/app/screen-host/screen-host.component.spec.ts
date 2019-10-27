import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScreenHostComponent } from './screen-host.component';

describe('ScreenHostComponent', () => {
  let component: ScreenHostComponent;
  let fixture: ComponentFixture<ScreenHostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScreenHostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScreenHostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
