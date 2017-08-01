using Microsoft.AspNetCore.Mvc;
using msb.Helpers;
using MySpotifyBillboard.DbContext;
using MySpotifyBillboard.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MySpotifyBillboard.Helpers;
using MySpotifyBillboard.Services;
using Newtonsoft.Json;

namespace MySpotifyBillboard.Controllers
{
    [Route("api/[controller]")]
    public class SpotifyController : Controller
    {
        private readonly IUserRepository _userRepository;

        public SpotifyController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        // get initial token for access to the spotify API.  user must specify all token params
        [HttpGet("token")]
        public async Task<IActionResult> SpotifyToken(SpotifyTokenParams spotifyTokenParams)
        {
            using (var client = new HttpClient())
            {
                var postData = InitializeAccessForm(spotifyTokenParams);

                using (var content = new FormUrlEncodedContent(postData))
                {
                    client.BaseAddress = new Uri(Constants.BASE_ADDRESS_ACCOUNTS);

                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    HttpResponseMessage response = await client.PostAsync("api/token", content);

                    // if we received a token from the spotify accounts service, convert that into our Dto model
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var spotifyConnectionData =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<SpotifyConnectionDataDto>(responseString);

                        if (spotifyConnectionData == null)
                        {
                            throw new Exception("Bad initial request for access token");
                        }

                        var jsonResponse = await _userRepository.GetUserInfo(spotifyConnectionData);

                        // if the user is not in our database, create a new user for the app, otherwise,
                        // return the existing user
                        var existingUser = _userRepository.UserExists((string)jsonResponse["id"]);
                        if (existingUser == null)
                        {
                            var newUser = await _userRepository.AddNewUser(spotifyConnectionData);

                            if (newUser != null)
                            {
                                return Ok(Json(newUser));
                            }
                            return BadRequest();
                        }
                        // if the user scope has changed
                        if (existingUser.Scope != spotifyTokenParams.Scope)
                        {
                            existingUser = _userRepository.UpdateUserWithNewScope(existingUser, spotifyConnectionData, spotifyTokenParams.Scope);
                        }

                        return Ok(Json(existingUser));
                    }
                    return BadRequest();
                }
            }
        }


        [HttpGet("top_tracks")]
        public async Task<IActionResult> TopTracks([FromQuery] string spotifyId, string timeFrame)
        {
            var timeFrameQueryString = GetTimeFrameQueryString(timeFrame);

            // if query strings are not complete or do not conform to standards, return BadRequest
            if (string.IsNullOrEmpty(spotifyId) || string.IsNullOrEmpty(timeFrameQueryString))
            {
                return BadRequest();
            }

            var user = _userRepository.UserExists(spotifyId);

            // if user does not exist, return not found
            if (user == null)
            {
                return NotFound();
            }

            // if user's session with the spotify API has expired, refresh that user's access token
            if (ExpiredAccessToken(user.ExpirationTime))
            {
                user = await _userRepository.Refresh(user);
            }

            // make a request to the spotify API server for the top 50 tracks in the requested time frame
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_ADDRESS_API);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(user.TokenType, user.AccessToken);

                HttpResponseMessage response = await client.GetAsync("v1/me/top/tracks?limit=50&time_range=" + timeFrameQueryString);
                var responseString = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    return Ok(_userRepository.UpdateUserCharts(user, responseString, AsTimeFrame(timeFrame)));
                }
                return NotFound();
            }
        }

        [HttpGet("playlist")]
        public async Task<IActionResult> CreatePlaylistForUser(string spotifyId, string timeFrame)
        {
            if (string.IsNullOrEmpty(timeFrame))
            {
                return BadRequest();
            }

            var timeFrameObj = AsTimeFrame(timeFrame);

            var user = _userRepository.UserExists(spotifyId);

            if (user == null)
            {
                return NotFound();
            }

            if (ExpiredAccessToken(user.ExpirationTime))
            {
                user = await _userRepository.Refresh(user);
            }

            // format the name for the playist to be created (i.e. "Four Week Top Tracks (07/31/2017 16:42:40)" )
            CreatePlaylistDto requestContentDto = new CreatePlaylistDto
            {
                name = TimeFrameForPlaylistName(timeFrameObj) + " Top Tracks (" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + ")"
            };

            var requestContentString = JsonConvert.SerializeObject(requestContentDto);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_ADDRESS_API);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(user.TokenType, user.AccessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync("v1/users/" + user.SpotifyId + "/playlists", 
                    new StringContent(requestContentString, Encoding.UTF8, "application/json"));
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var rootObject = JsonConvert.DeserializeObject<PlaylistRootObject>(responseString);
                    if (await AddTracksToPlaylist(rootObject.id, user, timeFrameObj))
                    {
                        return Ok(Json(rootObject.external_urls.spotify));
                    }
                }
                return BadRequest();
            }
        }

        
//        [HttpGet("user")]
//        public async Task<IActionResult> UserInfo(string spotifyId)
//        {
//            if (string.IsNullOrEmpty(spotifyId))
//            {
//                return BadRequest();
//            }
//
//            var user = _userRepository.UserExists(spotifyId);
//
//            if (user == null)
//            {
//                return NotFound();
//            }
//
//            if (!ExpiredAccessToken(user.ExpirationTime)) return Ok(Json(user));
//
//            var updatedUser = await _userRepository.Refresh(user);
//
//            // if user has revoked access token
//            if (updatedUser == null)
//            {
//                return NotFound();
//            }
//            return Json(updatedUser);
//            // if the access token has expired
//        }

        [HttpGet("deauthorize")]
        public IActionResult Deauthorize(string spotifyId)
        {
            if (string.IsNullOrEmpty(spotifyId))
            {
                return BadRequest();
            }

            var user = _userRepository.UserExists(spotifyId);

            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(user);
            return Ok();
        }















        /********************************************************************************************************/
        /********************************PRIVATE*HELPER*METHODS**************************************************/
        /********************************************************************************************************/

        // create form for http request using the given spotify token parameters
        private static List<KeyValuePair<string, string>> InitializeAccessForm(SpotifyTokenParams spotifyTokenParams)
        {
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", spotifyTokenParams.Code),
                new KeyValuePair<string, string>("redirect_uri", spotifyTokenParams.RedirectUri),
                new KeyValuePair<string, string>("client_id", Constants.CLIENT_ID),
                new KeyValuePair<string, string>("client_secret", Constants.CLIENT_SECRET)
            };
            return postData;
        }

        private static bool ExpiredAccessToken(DateTime expirationTime)
        {
            return DateTime.Now > expirationTime;
        }

        // changes time frame from query string at home to query string needed from spotify API. returns null
        // if the query string is not valid
        private string GetTimeFrameQueryString(string timeFrame)
        {
            switch (timeFrame)
            {
                case "long":
                    return "long_term";
                case "med":
                    return "medium_term";
                case "short":
                    return "short_term";
                default:
                    return null;
            }
        }

        // take the user query string and convert to TimeFrame enum
        private TimeFrame AsTimeFrame(string timeFrame)
        {
            switch (timeFrame)
            {
                case "long":
                    return TimeFrame.Long;
                case "med":
                    return TimeFrame.Med;
                default:
                    return TimeFrame.Short;
            }
        }

        // take a time frame and convert to text for playlist name
        private string TimeFrameForPlaylistName(TimeFrame timeFrame)
        {
            switch (timeFrame)
            {
                case TimeFrame.Long:
                    return "All Time";
                case TimeFrame.Med:
                    return "Six Month";
                default:
                    return "Four Week";
            }
        }

        // given a playlist id, a user, and a time frame, add all tracks from that user's TopTrackList in the given
        // time frame to the playlist with the given id
        private async Task<bool> AddTracksToPlaylist(string playlistId, User user, TimeFrame timeFrame)
        {
            var addToPlaylistDto = _userRepository.GetUrisFromUserTopTrackList(user, timeFrame);
            var requestContentString = JsonConvert.SerializeObject(addToPlaylistDto);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_ADDRESS_API);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(user.TokenType, user.AccessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync("v1/users/" + user.SpotifyId + "/playlists/" + playlistId + "/tracks",
                    new StringContent(requestContentString, Encoding.UTF8, "application/json"));
                var responseString = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}

