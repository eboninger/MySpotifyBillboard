import { Component, OnInit, Input } from '@angular/core';
import { Track } from './../track.model'
import { fadeInAnimation } from './track-list.animations'
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router'
import { Http, URLSearchParams } from '@angular/http'
import { KeyService } from '../../key.service'
import { SerializeTracksService } from '../serialize-tracks.service'
import { CookieService } from 'ngx-cookie-service'

import { Subscription } from 'rxjs'

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css'],
  // animations: [fadeInAnimation],
  // host: {'[@fadeInAnimation]': ''}
})
export class TrackListComponent implements OnInit {
  busy: Subscription
  tracks: Track[]

  constructor(private activatedRoute: ActivatedRoute, private http: Http,
    private keyService: KeyService, private serializeTracksService: SerializeTracksService,
    private router: Router, private cookieService: CookieService) {

    router.events.subscribe((event) => {
      if ((event instanceof NavigationEnd) && (this.cookieService.get("spotifyId") != null)) {
        this.getTopTracks();
      }
    })
  }

  async ngOnInit() {
    await this.getTopTracks();
  }

  async getTopTracks() {
    let params = new URLSearchParams();
    params.set('spotifyId', this.cookieService.get("spotifyId"));
    params.set('timeFrame', this.activatedRoute.snapshot.queryParams["time_frame"])
    this.busy = await this.http.get(this.keyService.getSingleKey('API-URL') + 'spotify/top_tracks', { search: params })
      .subscribe(
      res => {
        if (res == null) {
          return;
        }
        this.tracks = this.serializeTracksService.separate(res.json(), "Tracks");
      },
      err => {
        // error message - separate page?
      });
  }

}
