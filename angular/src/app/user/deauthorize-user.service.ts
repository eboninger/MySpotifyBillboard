import { Injectable } from '@angular/core'
import { Http, URLSearchParams, RequestOptions } from '@angular/http'
import { KeyService } from './../key.service'

@Injectable()
export class DeauthorizeUserService {
    constructor(private http: Http, private keyService: KeyService) { }

    async deauthorizeUser(spotifyId: string) {
        let params = new URLSearchParams();
        params.set('spotifyId', spotifyId);
        let options = new RequestOptions();
        // options.withCredentials = true;
        options.params = params;
        await this.http.delete(this.keyService.getSingleKey('API-URL') + 'list/deauthorize', options)
            .subscribe();
    }
}