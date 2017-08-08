import { Injectable } from '@angular/core';
import { Track } from './../track.model'
import { KeyService } from './../../key.service'
import { URLSearchParams, RequestOptions } from '@angular/http'
import { SerializeTracksService } from './../serialize-tracks.service'
import { HttpClient } from '@angular/common/http'

@Injectable()
export class TopTracksService {
  tracks: Track[]

  constructor(private http: HttpClient, private keyService: KeyService,
    private serializeTracksService: SerializeTracksService) { }


  getTopTracks(spotifyId: string, timeFrame: string) {
    let params = new URLSearchParams();
    params.set('spotifyId', spotifyId);
    params.set('timeFrame', timeFrame);
    let options = new RequestOptions();
    // options.withCredentials = true;
    options.params = params;
    return this.http.get(this.keyService.getSingleKey('API-URL') + 'list/top_tracks/' + spotifyId + '/' + timeFrame);
      

  }
}
