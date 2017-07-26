using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySpotifyBillboard.Models
{
    public class Artist
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ArtistId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OpenInSpotify { get; set; }
    }
}
