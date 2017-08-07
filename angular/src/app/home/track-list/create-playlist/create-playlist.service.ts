import { Injectable } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';
import { KeyService } from './../../../key.service'

@Injectable()
export class CreatePlaylistService {

  constructor(private http: Http, private keyService: KeyService) { }

  createPlaylist(spotifyId: string, timeFrame: string ) {
    let params = new URLSearchParams();
    params.set('spotifyId', spotifyId);
    params.set('timeFrame', timeFrame);
    return this.http.get(this.keyService.getSingleKey('API-URL') + 'spotify/playlist', { search: params })
  }

}
