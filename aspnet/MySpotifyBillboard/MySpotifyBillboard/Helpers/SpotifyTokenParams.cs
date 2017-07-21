using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace msb.Helpers
{
    public class SpotifyTokenParams
    {
        public string Code { get; set; }
        public string RedirectUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
