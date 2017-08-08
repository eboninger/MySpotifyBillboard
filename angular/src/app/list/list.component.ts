import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  sub: any;
  spotifyId: string;
  timeFrame: string;

  constructor(private activatedRoute: ActivatedRoute) { }

  async ngOnInit() {
      this.sub = this.activatedRoute.params.subscribe(params => {
      this.spotifyId = params['spotifyId'];
      this.timeFrame = params['timeFrame'];
    })
  }

}
