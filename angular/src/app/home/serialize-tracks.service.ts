import { Injectable } from '@angular/core';
import { Track } from './track.model'
import { Artist } from './artist.model'

@Injectable()
export class SerializeTracksService {

  constructor() { }

  separate(receivedData: any) {
    let tracks: Track[] = []
    let asObj = (JSON.parse(receivedData))["items"]

    asObj.forEach(track => {
      tracks.push(new Track(track["album"]["name"], 
                            track["album"]["id"], 
                            track["album"]["external_urls"]["spotify"],
                            new Artist(track["artists"][0]["id"],
                                       track["artists"][0]["name"],
                                       track["artists"][0]["external_urls"]["spotify"]),
                            track["id"],
                            track["album"]["images"][0]["url"],
                            track["album"]["images"][1]["url"],
                            track["name"],
                            track["external_urls"]["spotify"],
                            track["album"]["images"][2]["url"]))
    });

    return tracks;
  }

}
