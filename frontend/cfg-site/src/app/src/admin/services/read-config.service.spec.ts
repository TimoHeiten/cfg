import { TestBed } from '@angular/core/testing';

import { ReadConfigService } from './read-config.service';

describe('ReadConfigService', () => {
  let service: ReadConfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReadConfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
