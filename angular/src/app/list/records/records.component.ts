import { Component, OnInit } from '@angular/core';
import { Http, URLSearchParams, RequestOptions } from '@angular/http'
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router'
import { CookieService } from 'ngx-cookie-service'
import { KeyService } from './../../key.service'
import { Track } from '../track.model'
import { SerializeTracksService } from './../serialize-tracks.service'

@Component({
  selector: 'app-records',
  templateUrl: './records.component.html',
  styleUrls: ['./records.component.css']
})
export class RecordsComponent implements OnInit {
  longNumberOnes: Track[]
  longTimes: Track[]
  longNumberOneCons: Track[]
  longTimeCons: Track[]

  constructor(private http: Http, private cookieService: CookieService,
    private activatedRoute: ActivatedRoute, private keyService: KeyService,
    private serializeTracksService: SerializeTracksService, private router: Router) {
    router.events.subscribe((event) => {
      if ((event instanceof NavigationEnd) && (this.cookieService.get("spotifyId") != null)) {
        this.getRecords();
      }
    })
  }

  async ngOnInit() {
    await this.getRecords();
  }

  async getRecords() {
    let params = new URLSearchParams();
    params.set('spotifyId', this.cookieService.get("spotifyId"));
    params.set('timeFrame', this.activatedRoute.snapshot.queryParams["timeFrame"])
    let options = new RequestOptions();
    // options.withCredentials = true;
    options.params = params;
    await this.http.get(this.keyService.getSingleKey('API-URL') + 'list/records', options)
      .subscribe(
      res => {
        if (res == null) {
          return;
        }
        let recordsJson = res.json();
        this.longNumberOnes = this.serializeTracksService.separate(recordsJson, "LongestNumberOne");
        this.longTimes = this.serializeTracksService.separate(recordsJson, "LongestTimeInChart");
        this.longNumberOneCons = this.serializeTracksService.separate(recordsJson, "LongestNumberOneCons");
        this.longTimeCons = this.serializeTracksService.separate(recordsJson, "LongestTimeInChartCons");

      },
      err => {
        // error message - separate page?
      });
  }

}
