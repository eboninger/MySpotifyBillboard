import { Artist } from './artist.model'

export class Track {
    AlbumName: string
    AlbumId: string
    AlbumOpenInSpotify: string
    Artists: Artist[]
    Id: string
    LargeImage: string
    MediumImage: string
    Name: string
    OpenInSpotify: string
    SmallImage: string

    constructor(an, ai, aois, as, i, li, mi, n, ois, si) {
        this.AlbumName = an;
        this.AlbumId = ai;
        this.AlbumOpenInSpotify = aois;
        this.Artists = as;
        this.Id = i;
        this.LargeImage = li;
        this.MediumImage = mi;
        this.Name = n;
        this.OpenInSpotify = ois;
        this.SmallImage = si;
    }
}