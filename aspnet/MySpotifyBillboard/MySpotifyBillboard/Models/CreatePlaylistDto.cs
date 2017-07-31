using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpotifyBillboard.Models
{
    public class CreatePlaylistDto
    {
        public string name { get; set; }
        public bool @public { get; set; } = false;
        public bool collaborative { get; set; } = false;
        public string description { get; set; }
    }
}
