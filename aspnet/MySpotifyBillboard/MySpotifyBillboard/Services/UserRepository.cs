using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MySpotifyBillboard.DbContext;
using MySpotifyBillboard.Helpers;
using MySpotifyBillboard.Models;
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
    }
}
