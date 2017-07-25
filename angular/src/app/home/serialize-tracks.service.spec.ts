import { TestBed, inject } from '@angular/core/testing';

import { SerializeTracksService } from './serialize-tracks.service';

describe('SerializeTracksService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SerializeTracksService]
    });
  });

  it('should be created', inject([SerializeTracksService], (service: SerializeTracksService) => {
    expect(service).toBeTruthy();
  }));
});
