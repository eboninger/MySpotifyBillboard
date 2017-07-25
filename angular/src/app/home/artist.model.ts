export class Artist {
    Id: string
    Name: string
    OpenInSpotify: string

    constructor(i, n, ois) {
        this.Id = i;
        this.Name = n;
        this.OpenInSpotify = ois;
    }
}