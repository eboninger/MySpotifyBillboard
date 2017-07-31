using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySpotifyBillboard.Models
{
    public class Track
    {
        [Required]
        public string AlbumName { get; set; }

        [Required]
        public string AlbumId { get; set; }

        [Required]
        public string AlbumOpenInSpotify { get; set; }

        public ICollection<TrackArtist> TrackArtists { get; set; }

        [Required]
        public int TrackId { get; set; }

        [Required]
        public string LargeImage { get; set; }

        public DateTime LastUpdated { get; set; }

        [Required]
        public string MediumImage { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OpenInSpotify { get; set; }

        [Required]
        public int Position { get; set; }

        public int PreviousPosition { get; set; }

        [Required]
        public string SmallImage { get; set; }

        [Required]
        public string SpotifyTrackId { get; set; }

        [Required]
        public string SpotifyURI { get; set; }

        public int TimeAtNumberOne { get; set; }

        [Required]
        public int TimeOnChart { get; set; }

        public TopTrackList TopTrackList { get; set; }
        
    }
}
