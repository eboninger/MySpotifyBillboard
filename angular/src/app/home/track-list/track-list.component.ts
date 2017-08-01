import { Component, OnInit, Input } from '@angular/core';
import { Track } from './../track.model'
import { fadeInAnimation } from './track-list.animations'

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css'],
  // animations: [fadeInAnimation],
  // host: {'[@fadeInAnimation]': ''}
})
export class TrackListComponent implements OnInit {

  @Input() tracks: Track[]

  constructor() { }

  ngOnInit() {

  }

}
