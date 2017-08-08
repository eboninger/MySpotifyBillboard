import { Component, OnInit } from '@angular/core';
import { URLSearchParams, RequestOptions } from '@angular/http';
import { HttpClient } from '@angular/common/http';
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
    private cookieService: CookieService, private http: HttpClient) { }

  ngOnInit() {
    let code = this.activatedRoute.snapshot.queryParams['code'];
    const body = {redirecturi: this.keyService.getSingleKey("RedirectURI"), 
                  code: code, scope: this.keyService.getSingleKey("Scope")}

    var response = this.http.post(this.keyService.getSingleKey('API-URL') + 'list/token', body)
      .subscribe(
      data => {
        this.cookieService.set('spotifyId', data['value']['SpotifyId']);
        this.router.navigate(['list', data['value']['SpotifyId'], 'long'] )
      },
      err => {
        this.cookieService.deleteAll();
        this.router.navigate(['']);
      });
  }
}
