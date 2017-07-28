using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpotifyBillboard.Models
{
    public class TopTrackListDto
    {
        public ICollection<TopTrackListDtoTrack> Tracks { get; set; }
    }

    public class TopTrackListDtoTrack
    {
        public string AlbumName { get; set; }
        public string AlbumId { get; set; }
        public string AlbumOpenInSpotify { get; set; }
        public ICollection<TopTrackListDtoArtist> Artists { get; set; }
        public string Id { get; set; }
        public string LargeImage { get; set; }
        public string MediumImage { get; set; }
        public string Name { get; set; }
        public string OpenInSpotify { get; set; }
        public string SmallImage { get; set; }
    }

    public class TopTrackListDtoArtist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OpenInSpotify { get; set; }
    }
}
