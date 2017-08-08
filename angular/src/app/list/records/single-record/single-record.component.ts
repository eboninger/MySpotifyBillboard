import { Component, OnInit, Input } from '@angular/core';
import { Track } from '../../track.model'

@Component({
  selector: 'app-single-record',
  templateUrl: './single-record.component.html',
  styleUrls: ['./single-record.component.css']
})
export class SingleRecordComponent implements OnInit {
  @Input() tracks: Track[];
  @Input() headline: string;
  @Input() trackField: string;

  constructor() { }

  ngOnInit() {
  }

}
