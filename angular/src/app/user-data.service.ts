import { Injectable } from '@angular/core'
import { User } from './user/user.model'
import { Http, URLSearchParams } from '@angular/http'
import { KeyService } from './key.service'

@Injectable()
export class UserDataService {
    user: User

    constructor(private http: Http, private keyService: KeyService ) { }

    getUser(spotifyId: string) {
        return this.requestUserFromApi(spotifyId)
    }

    requestUserFromApi(spotifyId: string) {
        let responseBody = {}
        let params = new URLSearchParams();
        params.set('spotifyId', spotifyId);
        return this.http.get('http://localhost:52722/api/spotify/user', { search: params });
    }

    serializeUser(receivedData: any) {
        return new User(receivedData["accessToken"], receivedData["displayName"], receivedData["email"],
                              receivedData["expirationTime"], receivedData["id"], receivedData["refreshToken"],
                              receivedData["scope"], receivedData["spotifyId"], receivedData["tokenType"])
    }
}