import { Component, OnInit, Input } from '@angular/core';
import { Track } from './../track.model'
import { SerializeTracksService } from '../serialize-tracks.service'
import { TopTracksService } from './top-tracks.service'

import { Subscription } from 'rxjs'

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css'],
})
export class TrackListComponent implements OnInit {
  busy: Subscription
  tracks: Track[]
  hasData: Boolean = false;
  @Input() spotifyId: string
  @Input() timeFrame: string

  constructor(private serializeTracksService: SerializeTracksService,
    private topTracksService: TopTracksService) { }


  async ngOnInit() {

  }

  async ngOnChanges() {
    this.getTracksFromService();
  }

  async getTracksFromService() {
    if (this.spotifyId != null && this.timeFrame != null) {
      this.busy = await this.topTracksService.getTopTracks(this.spotifyId, this.timeFrame)
        .subscribe(
        res => {
          if (res == null) {
            return;
          }
          this.hasData = true;
          this.tracks = this.serializeTracksService.separate(res, "Tracks");
        },
        err => {
          this.hasData = false;
          // error message - separate page?
        });
    }
  }


}
