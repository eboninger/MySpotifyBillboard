import { Injectable } from '@angular/core';
import { Track } from './track.model'
import { Artist } from './artist.model'

@Injectable()
export class SerializeTracksService {

  constructor() { }

  separate(receivedData: any) {
    // let tracks: Track[] = []
    // let asObj = (JSON.parse(receivedData))["items"]

    // asObj.forEach(track => {
    //   let artists: Artist[] = []
    //   track["artists"].forEach(artist => {
    //     artists.push(new Artist(artist["id"], artist["name"], artist["external_urls"]["spotify"]))
    //   })
    //   tracks.push(new Track(track["album"]["name"], 
    //                         track["album"]["id"], 
    //                         track["album"]["external_urls"]["spotify"],
    //                         artists,
    //                         track["id"],
    //                         track["album"]["images"][0]["url"],
    //                         track["album"]["images"][1]["url"],
    //                         track["name"],
    //                         track["external_urls"]["spotify"],
    //                         track["album"]["images"][2]["url"]))
    // });

    let tracks: Track[] = []
    let asObj = receivedData["Tracks"]

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
