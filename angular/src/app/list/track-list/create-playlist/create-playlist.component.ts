import { Component, OnInit, Input } from '@angular/core';
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
  @Input() spotifyId: string;
  @Input() timeFrame: string;

  constructor(private activatedRoute: ActivatedRoute, 
    private createPlaylistService: CreatePlaylistService) { }

  ngOnInit() {

  }

  async export() {
    await this.createPlaylistService.createPlaylist(this.spotifyId, this.timeFrame)
      .subscribe(res => {
        if (res == null) {
          return;
        }
        this.linkToPlaylist = res["value"];
        this.buttonText = "Open Playlist In Spotify";
      },
      // the user has revoked token access
      err => {
        this.buttonText = "Error Creating Playlist";
      });
  }

}
