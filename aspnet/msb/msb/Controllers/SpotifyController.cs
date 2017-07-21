using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using msb.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using msb.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using Microsoft.AspNetCore.Mvc.Internal;

namespace msb.Controllers
{
    [Route("api/[controller]")]
    public class SpotifyController : Controller
    {
        [HttpGet("token")]
        public async Task<IActionResult> SpotifyToken(SpotifyTokenParams spotifyTokenParams)
        {
            SpotifyConnectionData _spotifyConnectionData;

            using (var client = new HttpClient())
            {
                var postData = initializeAccessForm(spotifyTokenParams);

                using (var content = new FormUrlEncodedContent(postData))
                {
                    client.BaseAddress = new Uri("https://accounts.spotify.com/");

                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    HttpResponseMessage response = await client.PostAsync("api/token", content);
                    

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        _spotifyConnectionData =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<SpotifyConnectionData>(responseString);
                        Debug.WriteLine("access_token: " + _spotifyConnectionData.access_token);
                        Debug.WriteLine("token_type: " + _spotifyConnectionData.token_type);
                        Debug.WriteLine("scope: " + _spotifyConnectionData.scope);
                        Debug.WriteLine("expires_in: " + _spotifyConnectionData.expires_in);
                        Debug.WriteLine("refresh_token: " + _spotifyConnectionData.refresh_token);
                        return Ok();
                    }
                    return NotFound();
                }
            }

        }

        private static List<KeyValuePair<string, string>> initializeAccessForm(SpotifyTokenParams spotifyTokenParams)
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
    }
}
