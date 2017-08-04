using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySpotifyBillboard.Models.ForSpotifyController;
using MySpotifyBillboard.Models.Shared;

namespace MySpotifyBillboard.Models.ForSpotifyController
{
    public class TopTrackList
    {
        public int TopTrackListId { get; set; }

        public ICollection<Track> Tracks { get; set; }

        public User User { get; set; }

        public TimeFrame TimeFrame { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime LastChanged { get; set; }
    }
}
