import { Injectable } from '@angular/core';
import { Http, URLSearchParams, RequestOptions } from '@angular/http';
import { KeyService } from './../../../key.service'

@Injectable()
export class CreatePlaylistService {

  constructor(private http: Http, private keyService: KeyService) { }

  createPlaylist(spotifyId: string, timeFrame: string ) {
    // let params = new URLSearchParams();
    // params.set('spotifyId', spotifyId);
    // params.set('timeFrame', timeFrame);
    let options = new RequestOptions();
    // options.withCredentials = true;
    const body = {spotifyId: spotifyId, timeFrame: timeFrame}
    return this.http.post(this.keyService.getSingleKey('API-URL') + 'list/playlist', body, options)
  }

}
