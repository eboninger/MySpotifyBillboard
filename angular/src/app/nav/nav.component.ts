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

  async ngOnInit() {

  }

  spotifyId() {
    let spotifyId = this.cookieService.get("spotifyId");
    if (spotifyId == null || spotifyId == "") {
      
    } else {
      return spotifyId;
    }
  }

  cookied() {
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

}
