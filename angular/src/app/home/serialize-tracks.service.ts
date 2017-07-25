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
      tracks.push(new Track(track["track"]["album"]["name"], 
                            track["track"]["album"]["id"], 
                            new Artist(track["track"]["artists"][0]["id"],
                                       track["track"]["artists"][0]["name"],
                                       track["track"]["artists"][0]["external_urls"]["spotify"]),
                            track["track"]["id"],
                            track["track"]["album"]["images"][0]["url"],
                            track["track"]["album"]["images"][1]["url"],
                            track["track"]["name"],
                            track["track"]["external_urls"]["spotify"],
                            track["track"]["album"]["images"][2]["url"]))
    });

    return tracks;
  }

}
