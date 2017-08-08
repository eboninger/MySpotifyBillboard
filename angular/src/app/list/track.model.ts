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
    PreviousPosition: number
    SmallImage: string
    TimeOnChart: number
    TimeAtNumberOne: number

    constructor(an, ai, aois, as, i, li, mi, n, ois, pp, si, toc, tano) {
        this.AlbumName = an;
        this.AlbumId = ai;
        this.AlbumOpenInSpotify = aois;
        this.Artists = as;
        this.Id = i;
        this.LargeImage = li;
        this.MediumImage = mi;
        this.Name = n;
        this.OpenInSpotify = ois;
        this.PreviousPosition = pp;
        this.SmallImage = si;
        this.TimeOnChart = toc;
        this.TimeAtNumberOne = tano;
    }
}