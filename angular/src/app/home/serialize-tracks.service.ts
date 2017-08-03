import { Injectable } from '@angular/core';
import { Track } from './track.model'
import { Artist } from './artist.model'

@Injectable()
export class SerializeTracksService {

  constructor() { }

  separate(receivedData: any, outerField: string) {
    let tracks: Track[] = []
    let asObj = receivedData[outerField]

    if (asObj == null) {
      return null;
    }

    asObj.forEach(track => {
      let artists: Artist[] = []
      track["Artists"].forEach(artist => {
        artists.push(new Artist(artist["Id"], artist["Name"], artist["OpenInSpotify"]))
      })
      tracks.push(new Track(track["AlbumName"], track["AlbumId"], track["AlbumOpenInSpotify"],
                            artists, track["Id"], track["LargeImage"], track["MediumImage"],
                            track["Name"], track["OpenInSpotify"], track["PreviousPosition"],
                             track["SmallImage"], track["TimeOnChart"], track["TimeAtNumberOne"])) 
    })

    return tracks;
  }

}
