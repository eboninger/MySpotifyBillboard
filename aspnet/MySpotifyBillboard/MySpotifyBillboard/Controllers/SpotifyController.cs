using Microsoft.AspNetCore.Mvc;
using msb.Helpers;
using MySpotifyBillboard.DbContext;
using MySpotifyBillboard.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using MySpotifyBillboard.Helpers;
using MySpotifyBillboard.Services;

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
                        var existingUser = _userRepository.UserExists((string) jsonResponse["id"]);
                        if (existingUser == null)
                        {
                            var newUser = await _userRepository.AddNewUser(spotifyConnectionData);

                            if (newUser != null)
                            {
                                return Ok(Json(newUser));
                            }
                        }
                        // if the user scope has changed
                        else if (existingUser.Scope != spotifyTokenParams.Scope)
                        {
                            existingUser = _userRepository.UpdateUserWithNewScope(existingUser, spotifyConnectionData, spotifyTokenParams.Scope);
                        }

                        return Ok(Json(existingUser));
                    }
                    return BadRequest();
                }
            }
        }

        [HttpGet("recently_played")]
        public async Task<IActionResult> RecentlyPlayed([FromQuery] string spotifyId)
        {
            var user = _userRepository.UserExists(spotifyId);

            if (user == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_ADDRESS_API);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(user.TokenType, user.AccessToken);

                HttpResponseMessage response = await client.GetAsync("v1/me/player/recently-played?limit=50");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return Ok(Json(responseString));
                }
                return NotFound();
            }

        }

        [HttpGet("top_all_time_tracks")]
        public async Task<IActionResult> TopAllTimeTracks([FromQuery] string spotifyId)
        {
            var user = _userRepository.UserExists(spotifyId);

            if (user == null)
            {
                return NotFound();
            }

            if (ExpiredAccessToken(user.ExpirationTime))
            {
                user = await _userRepository.Refresh(user);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_ADDRESS_API);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(user.TokenType, user.AccessToken);

                HttpResponseMessage response = await client.GetAsync("v1/me/top/tracks?limit=50&time_range=long_term");
                var responseString = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    return Ok(Json(responseString));
                }
                return NotFound();
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> UserInfo(string spotifyId)
        {
            if (spotifyId == null)
            {
                return BadRequest();
            }

            var user = _userRepository.UserExists(spotifyId);

            if (user == null)
            {
                return NotFound();
            }

            // if the access token has expired
            return Ok(ExpiredAccessToken(user.ExpirationTime) ? Json(await _userRepository.Refresh(user)) : Json(user));
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
    }
}
