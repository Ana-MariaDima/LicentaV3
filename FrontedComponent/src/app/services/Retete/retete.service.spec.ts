import { TestBed } from '@angular/core/testing';

import { ReteteService } from './retete.service';

describe('ReteteService', () => {
  let service: ReteteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReteteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
