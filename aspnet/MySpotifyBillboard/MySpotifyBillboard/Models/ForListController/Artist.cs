using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySpotifyBillboard.Models.ForListController
{
    public class Artist
    {

        public int ArtistId { get; set; }

        [Required]
        public string SpotifyArtistId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OpenInSpotify { get; set; }

        public ICollection<TrackArtist> TrackArtists { get; set; }

    }
}
