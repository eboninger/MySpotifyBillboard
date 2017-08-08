import { Injectable } from '@angular/core';
import { URLSearchParams, RequestOptions } from '@angular/http';
import { KeyService } from './../../../key.service';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CreatePlaylistService {

  constructor(private http: HttpClient, private keyService: KeyService) { }

  createPlaylist(spotifyId: string, timeFrame: string ) {
    return this.http.post(this.keyService.getSingleKey('API-URL') + 'list/playlist/' + spotifyId + '/' + timeFrame, null)
  }

}
