import { TestBed, inject } from '@angular/core/testing';

import { TopTracksService } from './top-tracks.service';

describe('TopTracksService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TopTracksService]
    });
  });

  it('should be created', inject([TopTracksService], (service: TopTracksService) => {
    expect(service).toBeTruthy();
  }));
});
