import { Component, OnInit } from '@angular/core';
import { User } from './../user/user.model'
import { Track } from './track.model'
import { ActivatedRoute } from '@angular/router'
import { UserDataService } from './../user-data.service'
import { Http, URLSearchParams } from '@angular/http'
import { SerializeTracksService } from './serialize-tracks.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userData: User
  recentlyPlayed: Track[] = []


  constructor(private activatedRoute: ActivatedRoute, private userDataService: UserDataService,
               private http: Http, private serializeTracksService: SerializeTracksService ) { }

  async ngOnInit() {
    await this.userDataService
      .getUser(this.activatedRoute.snapshot.queryParams["spotifyId"])
      .subscribe(res =>  {
        if (res == null) {
          return;
        }
        this.userData = this.userDataService.serializeUser(res.json()["value"]);
        this.getRecentlyPlayed();
       } );
  }

  async getRecentlyPlayed() {
    let params = new URLSearchParams();
    params.set('spotifyId', this.userData.SpotifyId);
    await this.http.get('http://localhost:52722/api/spotify/top_all_time_tracks', { search: params })
      .subscribe(res => {
        if (res == null) {
          return;
        }
        this.recentlyPlayed = this.serializeTracksService.separate(res.json()["value"]);
      });
  }
}
