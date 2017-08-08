import { Component, OnInit, Input } from '@angular/core';
import { Track } from './../track.model'
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router'
import { SerializeTracksService } from '../serialize-tracks.service'
import { CookieService } from 'ngx-cookie-service'
import { TopTracksService } from './top-tracks.service'

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

  constructor(private activatedRoute: ActivatedRoute, private serializeTracksService: SerializeTracksService,
    private router: Router, private cookieService: CookieService, private topTracksService: TopTracksService) {

    router.events.subscribe((event) => {
      if ((event instanceof NavigationEnd) && (this.cookieService.get("spotifyId") != null)) {
        this.getTracksFromService();
      }})
    }
      

  async ngOnInit() {
    this.getTracksFromService();
  }

  async getTracksFromService() {
    this.busy = await this.topTracksService.getTopTracks(this.cookieService.get("spotifyId"), this.activatedRoute.snapshot.queryParams["timeFrame"])
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
