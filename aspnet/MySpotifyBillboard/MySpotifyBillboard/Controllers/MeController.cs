using System;
using Microsoft.AspNetCore.Mvc;
using MySpotifyBillboard.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;
using MySpotifyBillboard.Models.Shared;
using Newtonsoft.Json;
using Optional;
using Constants = MySpotifyBillboard.Helpers.Constants;

namespace MySpotifyBillboard.Controllers
{
    [Route("api/[controller]")]
    public class MeController : Controller
    {
        private readonly IUserRepository _userRepository;

        public MeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet("data")]
        public async Task<IActionResult> RetrieveAllData(string spotifyId)
        {
            await MakeAllRequests(spotifyId);

            return Ok();
        }


        private async Task<bool> MakeAllRequests(string spotifyId)
        {
            var user = (await _userRepository.CanContinueWithRequest(new Dictionary<string, string>
            {
                {"spotifyId", spotifyId},
            }))
            .Match(
                some: u => u,
                none: () => null
            );

            if (user == null)
            {
                return false;
            }

            var fiftyTracksShort = (await MakeTopTracksRequest(user, "short_term"))
                .Match(
                    some: fts => fts,
                    none: () => null
                );
            var fiftyTracksMedium = (await MakeTopTracksRequest(user, "medium_term"))
                .Match(
                    some: ftm => ftm,
                    none: () => null
                );
            var fiftyTracksLong = (await MakeTopTracksRequest(user, "long_term"))
                .Match(
                    some: ftl => ftl,
                    none: () => null
                );

            if (fiftyTracksLong == null || fiftyTracksMedium == null || fiftyTracksShort == null)
            {
                return false;
            }

            var combinedList = new List<Item>();
            combinedList.AddRange(fiftyTracksShort.items);
            combinedList.AddRange(fiftyTracksMedium.items);
            combinedList.AddRange(fiftyTracksLong.items);

            var totalPopularity = 0;

            foreach (Item item in combinedList)
            {
                totalPopularity += item.popularity;
            }

            var popularities = combinedList.Select(i => i.popularity).ToList();
            var highestPop = popularities.Max();
            var minPop = popularities.Min();
            var standardDev = Statistics.StandardDeviation(combinedList.Select(i => i.popularity * 1.0));

            Debug.WriteLine("AVERAGE POPULARITY: " + totalPopularity/150.0);
            Debug.WriteLine("Lowest Popularity: " + minPop);
            Debug.WriteLine("Highest Popularity: " + highestPop);
            Debug.WriteLine("Standard Deviation: " + standardDev);

            return true;
        }

        private async Task<Option<RootObjectTopTracks>> MakeTopTracksRequest(User user, string timeFrameQueryString)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_ADDRESS_API);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(user.TokenType, user.AccessToken);

                HttpResponseMessage response =
                    await client.GetAsync("v1/me/top/tracks?limit=50&time_range=" + timeFrameQueryString);
                var responseString = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    return Option.Some(JsonConvert.DeserializeObject<RootObjectTopTracks>(responseString)); ;
                }
                return Option.None<RootObjectTopTracks>();
            }
        }
    }
}
