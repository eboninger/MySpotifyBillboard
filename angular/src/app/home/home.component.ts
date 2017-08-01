import { Component, OnInit } from '@angular/core';
import { User } from './../user/user.model'
import { Track } from './track.model'
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router'
import { Http, URLSearchParams } from '@angular/http'
import { SerializeTracksService } from './serialize-tracks.service'
import { CookieService } from 'ngx-cookie-service'
import { KeyService } from './../key.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  recentlyPlayed: Track[] = []


  constructor(private activatedRoute: ActivatedRoute,
    private http: Http, private serializeTracksService: SerializeTracksService,
    private router: Router, private cookieService: CookieService,
    private keyService: KeyService) {
    router.events.subscribe((event) => {
      if ((event instanceof NavigationEnd) && (this.cookieService.get("spotifyId") != null)) {
        this.getTopTracks();
      }
    })
  }

  async ngOnInit() {
    if (this.cookieService.get("spotifyId") == null || this.cookieService.get("spotifyId") == "") {
      this.router.navigate([''])
    }

    await this.getTopTracks();
  }

  async getTopTracks() {
    let params = new URLSearchParams();
    params.set('spotifyId', this.cookieService.get("spotifyId"));
    params.set('timeFrame', this.activatedRoute.snapshot.queryParams["time_frame"])
    await this.http.get(this.keyService.getSingleKey('API-URL') + 'spotify/top_tracks', { search: params })
      .subscribe(
      res => {
        if (res == null) {
          return;
        }
        this.recentlyPlayed = this.serializeTracksService.separate(res.json());
      },
      err => {
        this.cookieService.deleteAll();
        this.router.navigate(['']);
      });
  }
}
