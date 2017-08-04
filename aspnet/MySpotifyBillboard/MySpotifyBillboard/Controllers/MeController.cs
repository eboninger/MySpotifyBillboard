using Microsoft.AspNetCore.Mvc;
using msb.Helpers;
using MySpotifyBillboard.Helpers;
using MySpotifyBillboard.Models.ForSpotifyController;
using MySpotifyBillboard.Models.ForSpotifyController.Dtos;
using MySpotifyBillboard.Models.Shared;
using MySpotifyBillboard.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
            var user = await _userRepository.CanContinueWithRequest(new Dictionary<string, string>
            {
                {"spotifyId", spotifyId},
            });

            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
