import { TestBed, inject } from '@angular/core/testing';

import { UiNotificationService } from './ui-notification.service';

describe('UiNotificationService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UiNotificationService]
    });
  });

  it('should be created', inject([UiNotificationService], (service: UiNotificationService) => {
    expect(service).toBeTruthy();
  }));
});
