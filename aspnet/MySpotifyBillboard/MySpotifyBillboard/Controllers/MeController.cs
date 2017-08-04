using Microsoft.AspNetCore.Mvc;
using MySpotifyBillboard.Services;
using System.Collections.Generic;
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

            return true;
        }
    }
}
