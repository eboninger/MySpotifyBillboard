import { Component, OnInit, Input } from '@angular/core';
import { URLSearchParams, RequestOptions } from '@angular/http'
import { KeyService } from './../../key.service'
import { Track } from '../track.model'
import { SerializeTracksService } from './../serialize-tracks.service'
import { HttpClient } from '@angular/common/http'

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

  @Input() spotifyId: string;
  @Input() timeFrame: string;


  constructor(private http: HttpClient, private keyService: KeyService, 
    private serializeTracksService: SerializeTracksService) { }

  async ngOnInit() {

  }

  async ngOnChanges() {
    await this.getRecords();
  }

  async getRecords() {
    if (this.spotifyId != null && this.timeFrame != null) {
      await this.http.get(this.keyService.getSingleKey('API-URL') + 'list/records/' + this.spotifyId + '/' + this.timeFrame)
        .subscribe(
        res => {
          if (res == null) {
            return;
          }
          this.longNumberOnes = this.serializeTracksService.separate(res, "LongestNumberOne");
          this.longTimes = this.serializeTracksService.separate(res, "LongestTimeInChart");
          this.longNumberOneCons = this.serializeTracksService.separate(res, "LongestNumberOneCons");
          this.longTimeCons = this.serializeTracksService.separate(res, "LongestTimeInChartCons");

        },
        err => {
          // error message - separate page?
        });
    }
  }

}
