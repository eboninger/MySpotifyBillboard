import { Component, OnInit } from '@angular/core';
import { User } from './../user/user.model'
import { Track } from './track.model'
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router'
import { UserDataService } from './../user-data.service'
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
  userData: User
  recentlyPlayed: Track[] = []


  constructor(private activatedRoute: ActivatedRoute, private userDataService: UserDataService,
               private http: Http, private serializeTracksService: SerializeTracksService,
               private router: Router, private cookieService: CookieService,
               private keyService: KeyService ) 
  { 
    router.events.subscribe((event) => {
      if ((event instanceof NavigationEnd) && (this.userData != null)) {
        this.getTopTracks();
      }
    })
  }

  async ngOnInit() {
    if (this.cookieService.get("spotifyId") == null || this.cookieService.get("spotifyId") == "") {
      this.router.navigate([''])
    }

    await this.userDataService
      .getUser(this.cookieService.get("spotifyId"))
      .subscribe(
        res =>  {
          if (res == null) {
            return;
          }
          this.userData = this.userDataService.serializeUser(res.json()["value"]);
          this.getTopTracks();
        },
        // the user has revoked token access
        err => {
          this.cookieService.deleteAll();
          this.router.navigate(['']);
        });
  }

  async getTopTracks() {
    let params = new URLSearchParams();
    params.set('spotifyId', this.userData.SpotifyId);
    params.set('timeFrame', this.activatedRoute.snapshot.queryParams["time_frame"])
    await this.http.get(this.keyService.getSingleKey('API-URL') + 'spotify/top_tracks', { search: params })
      .subscribe(res => {
        if (res == null) {
          return;
        }
        this.recentlyPlayed = this.serializeTracksService.separate(res.json());
      });
  }
}
