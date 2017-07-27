using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MySpotifyBillboard.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public string DisplayName { get; set; }
        
        [Required]
        public string Scope { get; set; }

        [Required]
        public DateTime ExpirationTime { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string SpotifyId { get; set; }

//        public ICollection<TopTrackList> TopTrackLists { get; set; }
    }
}
