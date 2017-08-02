import { Component, OnInit } from '@angular/core';
import { KeyService } from '../../../key.service'
import { CookieService } from 'ngx-cookie-service'
import { Http, URLSearchParams } from '@angular/http'
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-create-playlist',
  templateUrl: './create-playlist.component.html',
  styleUrls: ['./create-playlist.component.css']
})
export class CreatePlaylistComponent implements OnInit {
  linkToPlaylist: string
  buttonText: string = "Export Playlist To Spotify"

  constructor(private activatedRoute: ActivatedRoute, 
    private cookieService: CookieService, private keyService: KeyService,
    private http: Http) { }

  ngOnInit() {
  }

  export() {
    let params = new URLSearchParams();
    params.set('spotifyId', this.cookieService.get("spotifyId"));
    params.set('timeFrame', this.activatedRoute.snapshot.queryParams["time_frame"])
    this.http.get(this.keyService.getSingleKey('API-URL') + 'spotify/playlist', { search: params })
      .subscribe(res => {
        if (res == null) {
          return;
        }
        this.linkToPlaylist = res.json()["value"];
        this.buttonText = "Open Playlist In Spotify";
      },
      // the user has revoked token access
      err => {
        this.buttonText = "Error Creating Playlist";
      });
  }

}
