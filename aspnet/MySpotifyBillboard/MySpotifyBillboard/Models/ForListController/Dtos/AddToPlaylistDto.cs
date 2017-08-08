using System.Collections.Generic;

namespace MySpotifyBillboard.Models.ForListController.Dtos
{
    public class AddToPlaylistDto
    {
        public ICollection<string> uris { get; set; }
    }
}
