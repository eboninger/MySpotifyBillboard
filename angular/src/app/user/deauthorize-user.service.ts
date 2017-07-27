import { Injectable } from '@angular/core'
import { Http, URLSearchParams } from '@angular/http'
import { KeyService } from './../key.service'

@Injectable()
export class DeauthorizeUserService {
    constructor(private http: Http, private keyService: KeyService) { }

    async deauthorizeUser(spotifyId: string) {
        let params = new URLSearchParams();
        params.set('spotifyId', spotifyId);
        await this.http.get(this.keyService.getSingleKey('API-URL') + 'spotify/deauthorize', { search: params })
            .subscribe();
    }
}