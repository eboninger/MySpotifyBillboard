using System.Collections.Generic;
using MySpotifyBillboard.Models.Shared;

namespace MySpotifyBillboard.Models.ForListController
{
    public class Followers
    {
        public object href { get; set; }
        public int total { get; set; }
    }

    public class Tracks
    {
        public string href { get; set; }
        public List<object> items { get; set; }
        public int limit { get; set; }
        public object next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }

    public class PlaylistRootObject
    {
        public bool collaborative { get; set; }
        public object description { get; set; }
        public ExternalUrls external_urls { get; set; }
        public Followers followers { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<object> images { get; set; }
        public string name { get; set; }
        public object owner { get; set; }
        public bool @public { get; set; }
        public string snapshot_id { get; set; }
        public Tracks tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}
