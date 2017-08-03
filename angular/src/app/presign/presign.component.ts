import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { KeyService } from '../key.service';
import { CookieService } from 'ngx-cookie-service'

@Component({
  selector: 'app-presign',
  templateUrl: './presign.component.html',
  styleUrls: ['./presign.component.css']
})
export class PresignComponent implements OnInit {
  authorizeUri: string
  isSignedIn: boolean

  constructor(private keyService: KeyService, private activatedRoute: ActivatedRoute,
    private router: Router, private cookieService: CookieService) { }

  ngOnInit() {
    // if (this.activatedRoute.snapshot.queryParams["spotifyId"] == null) {
    let spotifyId: string = this.cookieService.get('spotifyId')
    if (spotifyId == null || spotifyId == "") {
      this.isSignedIn = false;
      var authorizationParams = this.keyService.getKeys();
      var state = this.makeState();
      this.authorizeUri = this.constructUri(authorizationParams, state);
    } else {
      this.isSignedIn = true;
      this.router.navigate(['home'], { queryParams: { "spotifyId": spotifyId, "timeFrame": "long" } })
    }
  }

  private constructUri(params: any, state: any) {
    return "https://accounts.spotify.com/authorize/?client_id=" +
      params.ClientID +
      "&response_type=code&redirect_uri=" +
      params.RedirectURI +
      "&scope=" + this.keyService.getSingleKey("Scope") + "&state=" +
      state;
  }

  makeState(): string {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 20; i++)
      text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;

  }

}
