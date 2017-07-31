using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpotifyBillboard.Models
{
    public class AddToPlaylistDto
    {
        public ICollection<string> uris { get; set; }
    }
}
