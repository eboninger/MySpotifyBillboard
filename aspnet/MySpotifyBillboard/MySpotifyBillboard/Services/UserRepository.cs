using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySpotifyBillboard.DbContext;
using MySpotifyBillboard.Helpers;
using MySpotifyBillboard.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MySpotifyBillboard.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly BillboardDbContext _billboardDbContext;

        public UserRepository(BillboardDbContext billboardDbContext)
        {
            _billboardDbContext = billboardDbContext;
        }

        public User UserExists(string spotifyId)
        {
            return _billboardDbContext.Users.FirstOrDefault(u => u.SpotifyId == spotifyId);
        }

        public async Task<User> AddNewUser(SpotifyConnectionDataDto spotifyConnectionData)
        {
            var jsonResponse = await GetUserInfo(spotifyConnectionData);

            if (jsonResponse == null)
            {
                return null;
            }

            var newUser = new User
            {
                AccessToken = spotifyConnectionData.access_token,
                DisplayName = (string)jsonResponse["display_name"],
                Email = (string)jsonResponse["email"],
                ExpirationTime = DateTime.Now.Add(TimeSpan.FromSeconds(spotifyConnectionData.expires_in)),
                RefreshToken = spotifyConnectionData.refresh_token,
                Scope = spotifyConnectionData.scope,
                TokenType = spotifyConnectionData.token_type,
                SpotifyId = (string)jsonResponse["id"]
            };

            await _billboardDbContext.AddAsync(newUser);
            await _billboardDbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<JObject> GetUserInfo(SpotifyConnectionDataDto spotifyConnectionData)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_ADDRESS_API);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(spotifyConnectionData.token_type, spotifyConnectionData.access_token);

                HttpResponseMessage response = await client.GetAsync("v1/me");

                if (response.IsSuccessStatusCode)
                {
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
                }
            }

            return null;
        }

        public async Task<User> UpdateUserAfterRefresh(User user, string accessToken, DateTime expirationTime, string scope, string tokenType)
        {
            user.AccessToken = accessToken;
            user.Scope = scope;
            user.ExpirationTime = expirationTime;
            user.TokenType = tokenType;

            await _billboardDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Refresh(User user)
        {
            using (var client = new HttpClient())
            {
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", user.RefreshToken),
                    new KeyValuePair<string, string>("client_id", Constants.CLIENT_ID),
                    new KeyValuePair<string, string>("client_secret", Constants.CLIENT_SECRET)
                };

                using (var content = new FormUrlEncodedContent(postData))
                {
                    client.BaseAddress = new Uri(Constants.BASE_ADDRESS_ACCOUNTS);

                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    HttpResponseMessage response = await client.PostAsync("api/token", content);

                    var responseString = await response.Content.ReadAsStringAsync();
                    var spotifyRefreshData =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<SpotifyRefreshDto>(responseString);

                    // if the user has revoked their access token, delete them from the database
                    if (spotifyRefreshData.access_token == null)
                    {
                        DeleteUser(user);
                        return null;
                    }

                    return await UpdateUserAfterRefresh(user, spotifyRefreshData.access_token,
                        DateTime.Now.Add(TimeSpan.FromSeconds(spotifyRefreshData.expires_in)),
                        spotifyRefreshData.scope,
                        spotifyRefreshData.token_type);
                }
            }
        }

        public void DeleteUser(User user)
        {
            _billboardDbContext.Users.Remove(user);
            _billboardDbContext.SaveChanges();
        }

        public User UpdateUserWithNewScope(User user, SpotifyConnectionDataDto spotifyConnectionData, string scope)
        {
            user.AccessToken = spotifyConnectionData.access_token;
            user.RefreshToken = spotifyConnectionData.refresh_token;
            user.ExpirationTime = DateTime.Now.Add(TimeSpan.FromSeconds(spotifyConnectionData.expires_in));
            user.Scope = scope;
            user.TokenType = spotifyConnectionData.token_type;

            _billboardDbContext.SaveChanges();
            return user;
        }

        public JObject UpdateUserCharts(User user, string topTrackData, TimeFrame timeFrame)
        {
            if (user.TopTrackLists == null)
            {
                user.TopTrackLists = new List<TopTrackList>();
            }

            var rootObject = JsonConvert.DeserializeObject<RootObject>(topTrackData);
            var currentTopTrackList =
                _billboardDbContext.TopTrackLists.FirstOrDefault(
                    ttl => (ttl.User.Id == user.Id) && (ttl.TimeFrame == timeFrame));

            if (currentTopTrackList == null)
            {
                user = NewTTL(user, rootObject, timeFrame);
            }

            return CreateTopTrackListDto(user, timeFrame);
        }





        /********************************************************************************************************/
        /******************************PRIVATE*HELPER*METHODS****************************************************/
        /********************************************************************************************************/

        private User NewTTL(User user, RootObject rootObject, TimeFrame timeFrame)
        {
            var items = rootObject.items;
            var topTrackList = new TopTrackList();
            topTrackList.Tracks = new List<Track>();

            foreach (Item item in items)
            {
                var artists = new List<Artist>();

                foreach (JsonArtist jsonArtist in item.artists)
                {
                    var artist = _billboardDbContext.Artists.FirstOrDefault(a => a.SpotifyArtistId == jsonArtist.id);

                    if (artist == null)
                    {
                        artist = new Artist
                        {
                            Name = jsonArtist.name,
                            OpenInSpotify = jsonArtist.external_urls.spotify,
                            SpotifyArtistId = jsonArtist.id
                        };

                        _billboardDbContext.Artists.Add(artist);
                    }

                    artists.Add(artist);
                }

                var track = new Track
                {
                    AlbumId = item.album.id,
                    AlbumName = item.album.name,
                    AlbumOpenInSpotify = item.album.external_urls.spotify,
                    LargeImage = item.album.images[0].url,
                    MediumImage = item.album.images[1].url,
                    Name = item.name,
                    OpenInSpotify = item.external_urls.spotify,
                    PreviousPosition = null,
                    SmallImage = item.album.images[2].url,
                    SpotifyTrackId = item.id,
                    TopTrackList = topTrackList,
                    TimeAtNumberOne = 0,
                    TimeOnChart = 1
                };

                _billboardDbContext.Tracks.Add(track);
                _billboardDbContext.SaveChanges();

                foreach (Artist artist in artists)
                {
                    if (!_billboardDbContext.TrackArtists.Any(
                        ta => ta.ArtistId == artist.ArtistId && ta.TrackId == track.TrackId))
                    {
                        var trackArtist = new TrackArtist
                        {
                            TrackId = track.TrackId,
                            ArtistId = artist.ArtistId
                        };

                        _billboardDbContext.TrackArtists.Add(trackArtist);
                        _billboardDbContext.SaveChanges();
                    }
                }



            }

            topTrackList.User = user;
            topTrackList.TimeFrame = timeFrame;
            topTrackList.LastUpdated = DateTime.Now;

//            _billboardDbContext.TopTrackLists.Add(topTrackList);
            user.TopTrackLists.Add(topTrackList);
            _billboardDbContext.SaveChanges();

            return user;
        }

        private JObject CreateTopTrackListDto(User user, TimeFrame timeFrame)
        {
            var userLists = user.TopTrackLists.ToList();
            var listId = userLists.Find(ul => ul.TimeFrame == timeFrame).TopTrackListId;
            var listToConvert = _billboardDbContext.Tracks.Where(t => t.TopTrackList.TopTrackListId == listId).ToList();

            var topTrackListDto = new TopTrackListDto
            {
                Tracks = new List<TopTrackListDtoTrack>()
            };

            foreach (Track track in listToConvert)
            {
                var trackArtists = _billboardDbContext.TrackArtists.Where(ta => ta.TrackId == track.TrackId).ToList();
                var artists = new List<TopTrackListDtoArtist>();

                foreach (TrackArtist ta in trackArtists)
                {
                   var artist = _billboardDbContext.Artists.FirstOrDefault(a => a.ArtistId == ta.ArtistId);
                    artists.Add(new TopTrackListDtoArtist
                    {
                        Id = artist.SpotifyArtistId,
                        Name = artist.Name,
                        OpenInSpotify = artist.OpenInSpotify
                    });
                }

                topTrackListDto.Tracks.Add(new TopTrackListDtoTrack
                {
                    AlbumId = track.AlbumId,
                    AlbumName = track.AlbumName,
                    AlbumOpenInSpotify = track.AlbumOpenInSpotify,
                    Artists = artists,
                    Id = track.SpotifyTrackId,
                    LargeImage = track.LargeImage,
                    MediumImage = track.MediumImage,
                    Name = track.Name,
                    OpenInSpotify = track.OpenInSpotify,
                    SmallImage = track.SmallImage
                });
            }


            return JObject.Parse(JsonConvert.SerializeObject(topTrackListDto));
        }
    }
}
