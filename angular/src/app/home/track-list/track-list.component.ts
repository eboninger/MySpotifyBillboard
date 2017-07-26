import { Component, OnInit, Input } from '@angular/core';
import { Track } from './../track.model'

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css']
})
export class TrackListComponent implements OnInit {

  @Input() tracks: Track[]

  constructor() { }

  ngOnInit() {

  }

}
