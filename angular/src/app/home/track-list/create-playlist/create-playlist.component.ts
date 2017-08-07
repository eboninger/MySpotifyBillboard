import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service'
import { ActivatedRoute } from '@angular/router'
import { CreatePlaylistService } from './create-playlist.service'

@Component({
  selector: 'app-create-playlist',
  templateUrl: './create-playlist.component.html',
  styleUrls: ['./create-playlist.component.css']
})
export class CreatePlaylistComponent implements OnInit {
  linkToPlaylist: string
  buttonText: string = "Export Playlist To Spotify"

  constructor(private activatedRoute: ActivatedRoute, 
    private cookieService: CookieService, private createPlaylistService: CreatePlaylistService) { }

  ngOnInit() {
  }

  async export() {
    await this.createPlaylistService.createPlaylist(this.cookieService.get("spotifyId"), this.activatedRoute.snapshot.queryParams["timeFrame"])
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
