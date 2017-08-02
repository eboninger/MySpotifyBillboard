import { Component, OnInit } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';
import { ActivatedRoute, Router } from '@angular/router';
import { KeyService } from '../../key.service';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../../user/user.model';
import 'rxjs/add/operator/map'

@Component({
  selector: 'app-finish-auth',
  templateUrl: './finish-auth.component.html',
  styleUrls: ['./finish-auth.component.css']
})
export class FinishAuthComponent implements OnInit {
  recentlyPlayed = {};

  constructor(private activatedRoute: ActivatedRoute,
    private keyService: KeyService, private router: Router,
    private cookieService: CookieService, private http: Http) { }

  ngOnInit() {
    let code = this.activatedRoute.snapshot.queryParams['code'];
    let params: URLSearchParams = new URLSearchParams();
    let spotifyId = ""
    params.set('redirecturi', this.keyService.getSingleKey("RedirectURI"));
    params.set('code', code);
    params.set('scope', this.keyService.getSingleKey("Scope"));

    var response = this.http.get(this.keyService.getSingleKey('API-URL') + 'spotify/token', {
      search: params
    }).map(res => res.json())
      .subscribe(
      data => {
        this.cookieService.set('spotifyId', data["value"]["spotifyId"]);
        this.router.navigate(['home'], { queryParams: { "spotifyId": data["value"]["spotifyId"], "time_frame": "long" } })
      },
      err => {
        this.cookieService.deleteAll();
        this.router.navigate(['']);
      });
  }
}
