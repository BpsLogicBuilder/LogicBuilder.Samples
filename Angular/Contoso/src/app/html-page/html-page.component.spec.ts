import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { HtmlPageComponent } from './html-page.component';

describe('HtmlPageComponent', () => {
  let component: HtmlPageComponent;
  let fixture: ComponentFixture<HtmlPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ HtmlPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HtmlPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
