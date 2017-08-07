import { TestBed, inject } from '@angular/core/testing';

import { CreatePlaylistService } from './create-playlist.service';

describe('CreatePlaylistService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CreatePlaylistService]
    });
  });

  it('should be created', inject([CreatePlaylistService], (service: CreatePlaylistService) => {
    expect(service).toBeTruthy();
  }));
});
