import { Component, OnInit } from '@angular/core';
import { User } from './../user/user.model'
import { Track } from './track.model'
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router'
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
  constructor(private router: Router, private cookieService: CookieService) { }

  async ngOnInit() {
    if (this.cookieService.get("spotifyId") == null || this.cookieService.get("spotifyId") == "") {
      this.router.navigate([''])
    }
  }

}
