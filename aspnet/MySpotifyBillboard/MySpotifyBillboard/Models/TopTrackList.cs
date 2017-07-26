using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySpotifyBillboard.Models
{
    public class TopTrackList
    {
        [Required]
        public int Id { get; set; }

        public int UserId { get; set; }

        public IEnumerable<Track> Tracks { get; set; }

    }
}
