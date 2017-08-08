import { Component, OnInit } from '@angular/core';
import { DeauthorizeUserService } from './deauthorize-user.service'
import { CookieService } from 'ngx-cookie'
import { Router } from '@angular/router'

@Component({
  selector: 'app-deauthorize',
  templateUrl: './deauthorize.component.html',
  styleUrls: ['./deauthorize.component.css']
})
export class DeauthorizeComponent implements OnInit {
  clearDataText: string

  constructor(private deauthorizeUserService: DeauthorizeUserService,
    private cookieService: CookieService, private router: Router) { }

  ngOnInit() {
    this.clearDataText = "By clicking the above button, you are removing all of your information from our stores.  If you decided to begin using MySpotifyBillboard again, your record tracking will begin at zero days."
  }

  deleteUser() {
    let spotifyId = this.cookieService.get("spotifyId");

    if (spotifyId == "" || spotifyId == null) { return; }
    
    this.deauthorizeUserService.deauthorizeUser(spotifyId);
    this.cookieService.removeAll();
    this.clearDataText = "Done!"
    this.router.navigate(['']);
  }

  clearCookies() {
    this.cookieService.removeAll();
    console.log(this.cookieService.get("spotifyId"))
    this.router.navigate(['']);
  }

}