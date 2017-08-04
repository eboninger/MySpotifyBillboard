using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MySpotifyBillboard.Models.ForSpotifyController;

namespace MySpotifyBillboard.Models.ForSpotifyController
{
    public class Track
    {
        public Track() { }


        public Track(string albumName, string albumId, string albumOIS, string largeImage, DateTime lastUpdated,
            string mediumImage, string name, string openInSpotify, int position, int previousPosition, string smallImage,
            string spotifyTrackId, string spotifyUri, int timeAtNumberOne, int timeOnChart, TopTrackList topTrackList)
        {
            AlbumName = albumName;
            AlbumId = albumId;
            AlbumOpenInSpotify = albumOIS;
            LargeImage = largeImage;
            LastUpdated = lastUpdated;
            MediumImage = mediumImage;
            Name = name;
            OpenInSpotify = openInSpotify;
            Position = position;
            PreviousPosition = previousPosition;
            SmallImage = smallImage;
            SpotifyTrackId = spotifyTrackId;
            SpotifyURI = spotifyUri;
            TimeAtNumberOne = timeAtNumberOne;
            TimeOnChart = timeOnChart;
            TopTrackList = topTrackList;

        }

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
