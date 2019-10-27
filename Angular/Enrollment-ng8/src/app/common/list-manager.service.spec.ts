import { TestBed, inject } from '@angular/core/testing';

import { ListManagerService } from './list-manager.service';

describe('ListManagerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ListManagerService]
    });
  });

  it('should be created', inject([ListManagerService], (service: ListManagerService) => {
    expect(service).toBeTruthy();
  }));
});
