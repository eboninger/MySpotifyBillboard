import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  isSignedIn: boolean

  constructor(private activatedRoute: ActivatedRoute, private router: Router) {
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
    if (this.activatedRoute.snapshot.queryParams["spotifyId"] == null) {
      this.isSignedIn = false;
    } else {
      this.isSignedIn = true;
    }
  }

}
