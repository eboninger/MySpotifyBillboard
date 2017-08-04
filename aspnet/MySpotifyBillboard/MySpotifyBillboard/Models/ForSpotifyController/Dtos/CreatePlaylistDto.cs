namespace MySpotifyBillboard.Models.ForSpotifyController.Dtos
{
    public class CreatePlaylistDto
    {
        public string name { get; set; }
        public bool @public { get; set; } = false;
        public bool collaborative { get; set; } = false;
        public string description { get; set; }
    }
}
