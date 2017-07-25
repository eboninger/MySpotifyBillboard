export class User {
    Id: number;
    AccessToken: string;
    TokenType: string;
    DisplayName: string;
    Scope: string;
    ExpirationTime: Date;
    RefreshToken: string;
    Email: string;
    SpotifyId: string;

    constructor (at, dn, e, et, id, rt, s, si, tt) {
        this.Id = id;
        this.AccessToken = at;
        this.TokenType = tt;
        this.DisplayName = dn;
        this.Scope = s;
        this.ExpirationTime = et;
        this.RefreshToken = rt;
        this.Email = e;
        this.SpotifyId = si;
    }

}