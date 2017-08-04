namespace MySpotifyBillboard.Models.ForSpotifyController.Dtos
{
    public class SpotifyRefreshDto
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
    }
}
