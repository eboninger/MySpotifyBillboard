using System.Collections.Generic;

namespace MySpotifyBillboard.Models.ForSpotifyController
{
    public class ExternalUrls
    {
        public string spotify { get; set; }
    }

    public class Image
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Album
    {
        public string album_type { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class JsonArtist
    {
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class ExternalIds
    {
        public string isrc { get; set; }
    }


    public class LinkedFrom
    {
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Item
    {
        public Album album { get; set; }
        public List<JsonArtist> artists { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool @explicit { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public bool is_playable { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public LinkedFrom linked_from { get; set; }
    }

    public class RootObject
    {
        public List<Item> items { get; set; }
        public int total { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public string href { get; set; }
        public object next { get; set; }
    }
}
