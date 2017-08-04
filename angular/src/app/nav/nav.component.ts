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

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
               private cookieService: CookieService) {
    router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.updateLinks();
      }
    })
   }

  ngOnInit() {
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
    let activeTimeFrame = this.activatedRoute.snapshot.queryParams["timeFrame"];

    if ((activeTimeFrame == null || activeTimeFrame == "") && (timeFrame == "account")) {
      return {color: 'rgba(240,125,226,0.9)'};
    }

    if (activeTimeFrame == null || activeTimeFrame == "") {
      return {color: 'rgba(230,125,226,0.7)'};
    }

    if (activeTimeFrame == timeFrame) {
      return {color: 'rgba(240,125,226,0.9)'};
    }

    return {color: 'rgba(230,125,226,0.7)'};
  }

}
