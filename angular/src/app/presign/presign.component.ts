import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { KeyService } from '../key.service';

@Component({
  selector: 'app-presign',
  templateUrl: './presign.component.html',
  styleUrls: ['./presign.component.css']
})
export class PresignComponent implements OnInit {
  authorizeUri: string
  isSignedIn: boolean

  constructor(private keyService: KeyService, private activatedRoute: ActivatedRoute,
               private router: Router) { }

  ngOnInit() {
    if (this.activatedRoute.snapshot.queryParams["spotifyId"] == null) {
      this.isSignedIn = false;
      var authorizationParams = this.keyService.getKeys();
      var state = "a23984kjh9a8khqawdcxmnbw9"
      this.authorizeUri = this.constructUri(authorizationParams, state);
    } else {
      this.isSignedIn = true;
      this.router.navigate(['home'], { queryParams: { "spotifyId": this.activatedRoute.snapshot.queryParams["spotifyId"], "time_frame": "long" }})
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

}
