import { Injectable } from '@angular/core';
import { Track } from './../track.model'
import { KeyService } from './../../key.service'
import { Http, URLSearchParams } from '@angular/http'
import { SerializeTracksService } from './../serialize-tracks.service'

@Injectable()
export class TopTracksService {
  tracks: Track[]

  constructor(private http: Http, private keyService: KeyService,
    private serializeTracksService: SerializeTracksService) { }


  getTopTracks(spotifyId: string, timeFrame: string) {
    let params = new URLSearchParams();
    params.set('spotifyId', spotifyId);
    params.set('timeFrame', timeFrame);
    return this.http.get(this.keyService.getSingleKey('API-URL') + 'spotify/top_tracks', { search: params });
      

  }
}
