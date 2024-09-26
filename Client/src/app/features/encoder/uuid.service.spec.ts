import { TestBed } from '@angular/core/testing';

import { UuidService } from '../../core/utilities/uuid.service';

describe('UuidService', () => {
  let service: UuidService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UuidService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
