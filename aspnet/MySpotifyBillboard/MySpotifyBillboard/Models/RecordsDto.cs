using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpotifyBillboard.Models
{
    public class RecordsDto
    {
        public ICollection<RecordsDtoTrack> LongestNumberOne { get; set; }
        public ICollection<RecordsDtoTrack> LongestNumberOneCons { get; set; }
        public ICollection<RecordsDtoTrack> LongestTimeInChart { get; set; }
        public ICollection<RecordsDtoTrack> LongestTimeInChartCons { get; set; }
    }

    public class RecordsDtoTrack
    {
        public RecordsDtoTrack(string albumName, string albumId, string albumOIS, ICollection<RecordsDtoArtist> artists,
                                string id, string largeImage, string mediumImage, string name, string OIS, int previousPosition,
                                string smallImage, int timeAtNumberOne, int timeOnChart)
        {
            AlbumName = albumName;
            AlbumId = albumId;
            AlbumOpenInSpotify = albumOIS;
            Artists = artists;
            Id = id;
            LargeImage = largeImage;
            MediumImage = mediumImage;
            Name = name;
            OpenInSpotify = OIS;
            PreviousPosition = previousPosition;
            SmallImage = smallImage;
            TimeAtNumberOne = timeAtNumberOne;
            TimeOnChart = timeOnChart;
        }

        public string AlbumName { get; set; }
        public string AlbumId { get; set; }
        public string AlbumOpenInSpotify { get; set; }
        public ICollection<RecordsDtoArtist> Artists { get; set; }
        public string Id { get; set; }
        public string LargeImage { get; set; }
        public string MediumImage { get; set; }
        public string Name { get; set; }
        public string OpenInSpotify { get; set; }
        public int PreviousPosition { get; set; }
        public string SmallImage { get; set; }
        public int TimeAtNumberOne { get; set; }
        public int TimeOnChart { get; set; }
    }

    public class RecordsDtoArtist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OpenInSpotify { get; set; }
    }
}
