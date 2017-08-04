using System.Collections.Generic;

namespace MySpotifyBillboard.Models.ForSpotifyController.Dtos
{
    public class AddToPlaylistDto
    {
        public ICollection<string> uris { get; set; }
    }
}
