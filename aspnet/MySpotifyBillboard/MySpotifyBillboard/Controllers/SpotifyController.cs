using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using msb.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySpotifyBillboard.DbContext;
using MySpotifyBillboard.Models;
using Newtonsoft.Json.Linq;

namespace MySpotifyBillboard.Controllers
{
    [Route("api/[controller]")]
    public class SpotifyController : Controller
    {
        private BillboardDbContext _billboardDbContext;
        const string BASE_ADDRESS_ACCOUNTS = "https://accounts.spotify.com/";
        private const string BASE_ADDRESS_API = "https://api.spotify.com/";


        public SpotifyController(BillboardDbContext billboardDbContext)
        {
            _billboardDbContext = billboardDbContext;
        }

        [HttpGet("token")]
        public async Task<IActionResult> SpotifyToken(SpotifyTokenParams spotifyTokenParams)
        {


            using (var client = new HttpClient())
            {
                var postData = InitializeAccessForm(spotifyTokenParams);

                using (var content = new FormUrlEncodedContent(postData))
                {
                    client.BaseAddress = new Uri(BASE_ADDRESS_ACCOUNTS);

                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    HttpResponseMessage response = await client.PostAsync("api/token", content);


                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var spotifyConnectionData =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<SpotifyConnectionDataDto>(responseString);

                        if (spotifyConnectionData == null)
                        {
                            throw new Exception("Bad initial request for access token");
                        }

                        var jsonResponse = await getUserInfo(spotifyConnectionData);

                        var existingUser = UserExists((string) jsonResponse["id"]);
                        if (existingUser == null)
                        {
                            var newUser = await AddNewUser(spotifyConnectionData);

                            if (newUser != null)
                            {
                                return Ok(Json(newUser));
                            }
                        }

                        return Ok(Json(existingUser));
                    }
                    return BadRequest();
                }
            }
        }

        private User UserExists(string spotifyId)
        {
            return _billboardDbContext.Users.FirstOrDefault(u => u.SpotifyId == spotifyId);
        }

        private async Task<User> AddNewUser(SpotifyConnectionDataDto spotifyConnectionData)
        {
            var jsonResponse = await getUserInfo(spotifyConnectionData);

            if (jsonResponse == null)
            {
                return null;
            }

            var newUser = new User
            {
                AccessToken = spotifyConnectionData.access_token,
                DisplayName = (string) jsonResponse["display_name"],
                Email = (string) jsonResponse["email"],
                ExpirationTime = DateTime.Now.Add(TimeSpan.FromSeconds(spotifyConnectionData.expires_in)),
                RefreshToken = spotifyConnectionData.refresh_token,
                Scope = spotifyConnectionData.scope,
                TokenType = spotifyConnectionData.token_type,
                SpotifyId = (string) jsonResponse["id"]
            };

            await _billboardDbContext.AddAsync(newUser);
            await _billboardDbContext.SaveChangesAsync();

            return newUser;


        }

        private async Task<JObject> getUserInfo(SpotifyConnectionDataDto spotifyConnectionData)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADDRESS_API);
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

        private static List<KeyValuePair<string, string>> InitializeAccessForm(SpotifyTokenParams spotifyTokenParams)
        {
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", spotifyTokenParams.Code),
                new KeyValuePair<string, string>("redirect_uri", spotifyTokenParams.RedirectUri),
                new KeyValuePair<string, string>("client_id", spotifyTokenParams.ClientId),
                new KeyValuePair<string, string>("client_secret", spotifyTokenParams.ClientSecret)
            };
            return postData;
        }


        [HttpGet("recently_played")]
        public async Task<IActionResult> RecentlyPlayed([FromQuery] string spotifyId)
        {
            Debug.WriteLine("SPOTIFY_ID: " + spotifyId);
             
            var user = UserExists(spotifyId);

            if (user == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADDRESS_API);
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

        [HttpGet("user")]
        public async Task<IActionResult> UserInfo(string spotifyId, string clientId, string clientSecret)
        {
            if (spotifyId == null || clientId == null || clientSecret == null)
            {
                return BadRequest();
            }

            var user = UserExists(spotifyId);

            if (user == null)
            {
                return NotFound();
            }

            // if the access token has expired
            if (DateTime.Now > user.ExpirationTime)
            {
                return Ok(Json(await Refresh(user, clientId, clientSecret)));
            }

            return Ok(Json(user));
        }

        private async Task<User> Refresh(User user, string clientId, string clientSecret)
        {
            using (var client = new HttpClient())
            {
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", user.RefreshToken),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret)
                };

                using (var content = new FormUrlEncodedContent(postData))
                {
                    client.BaseAddress = new Uri(BASE_ADDRESS_ACCOUNTS);

                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    HttpResponseMessage response = await client.PostAsync("api/token", content);

                    var responseString = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("RESPONSE: " + responseString);
                    var spotifyRefreshData =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<SpotifyRefreshDto>(responseString);

                    if (spotifyRefreshData == null)
                    {
                        throw new Exception("Bad request for refresh token");
                    }

                    user.AccessToken = spotifyRefreshData.access_token;
                    user.Scope = spotifyRefreshData.scope;
                    user.ExpirationTime = DateTime.Now.Add(TimeSpan.FromSeconds(spotifyRefreshData.expires_in));
                    user.TokenType = spotifyRefreshData.token_type;

                    await _billboardDbContext.SaveChangesAsync();
                    return user;
                }
            }
        }
    }
}
