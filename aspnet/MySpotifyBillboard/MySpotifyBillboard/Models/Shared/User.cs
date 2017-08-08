using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySpotifyBillboard.Models.ForListController;

namespace MySpotifyBillboard.Models.Shared
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        [Required]
        public string Scope { get; set; }

        [Required]
        public DateTime ExpirationTime { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        [Required]
        public string SpotifyId { get; set; }

        public ICollection<TopTrackList> TopTrackLists { get; set; }

        [MaxLength(60000)]
        public string ShortTrackList { get; set; }

        [MaxLength(60000)]
        public string MedTrackList { get; set; }

        [MaxLength(60000)]
        public string LongTrackList { get; set; }
    }
}
