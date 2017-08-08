import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { CookieService } from 'ngx-cookie-service'

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  isSignedIn: boolean
  isCollapsed = false;
  activeTimeFrame: string;
  sub: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
               private cookieService: CookieService) {
    router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.updateLinks();
      }
    })
   }

  async ngOnInit() {
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.activeTimeFrame = params['timeFrame'];
    })
  }

  spotifyId() {
    let spotifyId = this.cookieService.get("spotifyId");
    if (spotifyId == null || spotifyId == "") {
      
    } else {
      return spotifyId;
    }
  }

  checkIfDisabled() {
    if (this.isSignedIn) {
      return []
    } else {
      return ['disabled']
    }
  }

  updateLinks() {
    if (this.cookieService.get("spotifyId") == null || this.cookieService.get("spotifyId") == "") {
      this.isSignedIn = false;
    } else {
      this.isSignedIn = true;
    }
  }

  activeStyling(timeFrame: string) {
    console.log(this.activatedRoute.params)
    console.log(this.activeTimeFrame)
    if ((this.activeTimeFrame == null || this.activeTimeFrame == "") && (timeFrame == "account")) {
      return {color: 'rgba(240,125,226,0.9)'};
    }

    if (this.activeTimeFrame == null || this.activeTimeFrame == "") {
      return {color: 'rgba(230,125,226,0.7)'};
    }

    if (this.activeTimeFrame == timeFrame) {
      return {color: 'rgba(240,125,226,0.9)'};
    }

    return {color: 'rgba(230,125,226,0.7)'};
  }

}
