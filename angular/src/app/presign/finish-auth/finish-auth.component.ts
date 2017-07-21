import { Component, OnInit } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { KeyService } from '../../key.service'

@Component({
  selector: 'app-finish-auth',
  templateUrl: './finish-auth.component.html',
  styleUrls: ['./finish-auth.component.css']
})
export class FinishAuthComponent implements OnInit {
  recentlyPlayed = {};

  constructor(private http: Http, private activatedRoute: ActivatedRoute,
               private keyService: KeyService) { }

  ngOnInit() {
    let code = this.activatedRoute.snapshot.queryParams['code'];
    console.log(code);
    let params: URLSearchParams = new URLSearchParams();
    params.set('redirecturi', this.keyService.getSingleKey("RedirectURI"));
    params.set('clientid', this.keyService.getSingleKey("ClientID"));
    params.set('clientsecret', this.keyService.getSingleKey("ClientSecret")); 
    params.set('code', code);

    var response = this.http.get('http://localhost:52722/api/spotify/token', {
      search: params }).subscribe();

    this.http.get('http://localhost:52722/api/spotify/recently_played').subscribe(data => this.recentlyPlayed = data);
  }

}
