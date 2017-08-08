import { Injectable } from '@angular/core'
import { URLSearchParams, RequestOptions } from '@angular/http'
import { HttpClient } from '@angular/common/http'
import { KeyService } from './../key.service'

@Injectable()
export class DeauthorizeUserService {
    constructor(private http: HttpClient, private keyService: KeyService) { }

    async deauthorizeUser(spotifyId: string) {
        await this.http.delete(this.keyService.getSingleKey('API-URL') + 'list/deauthorize/' + spotifyId)
            .subscribe();
    }
}