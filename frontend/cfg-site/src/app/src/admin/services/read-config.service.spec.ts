import { TestBed } from '@angular/core/testing';

import { ConfigDataService } from './read-config.service';

describe('ReadConfigService', () => {
  let service: ConfigDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConfigDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
