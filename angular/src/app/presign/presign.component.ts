import { Component, OnInit } from '@angular/core';
import { KeyService } from '../key.service';

@Component({
  selector: 'app-presign',
  templateUrl: './presign.component.html',
  styleUrls: ['./presign.component.css']
})
export class PresignComponent implements OnInit {
  authorizeUri: string

  constructor(private keyService: KeyService) { }

  ngOnInit() {
    var authorizationParams = this.keyService.getKeys();
    var state = "a23984kjh9a8khqawdcxmnbw9"
    console.log(authorizationParams);
    this.authorizeUri = this.constructUri(authorizationParams, state);
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
