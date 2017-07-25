import { Component, OnInit } from '@angular/core';
import { User } from './../user/user.model'
import { ActivatedRoute } from '@angular/router'
import { UserDataService } from './../user-data.service'
import { Http, URLSearchParams } from '@angular/http'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userData: User

  constructor(private activatedRoute: ActivatedRoute, private userDataService: UserDataService,
               private http: Http ) { }

  async ngOnInit() {
    await this.userDataService
      .getUser(this.activatedRoute.snapshot.queryParams["spotifyId"])
      .subscribe(res =>  {
        if (res == null) {
          return;
        }
        this.userData = this.userDataService.serializeUser(res.json()["value"]);
        this.getRecentlyPlayed();
       } );
  }

  async getRecentlyPlayed() {
    let params = new URLSearchParams();
    params.set('spotifyId', this.userData.SpotifyId);
    await this.http.get('http://localhost:52722/api/spotify/recently_played', { search: params })
      .subscribe(res => {
        console.log(res);
      });
  }
}
