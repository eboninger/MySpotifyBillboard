﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MySpotifyBillboard.Models
{
    public class TopTrackList
    {
        public int TopTrackListId { get; set; }

        public ICollection<Track> Tracks { get; set; }

        public User User { get; set; }

        public TimeFrame TimeFrame { get; set; }

    }
}
