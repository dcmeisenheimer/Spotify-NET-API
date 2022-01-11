using Microsoft.AspNetCore.Mvc;
using SpotifyPlus.Models;
using SpotifyPlus.Services;
using System.Diagnostics;

namespace SpotifyPlus.Controllers
{
    public class TopArtistSongsController : Controller
    {
        //Private fields to access token and method to get top artist
        private readonly ISpotifyAccountServices _spotifyAccountServices;
        private readonly IConfiguration _configuration;
        private readonly IArtistTopRelease _artistTopRelease;

        //constructor needed in order to use controller
        public TopArtistSongsController(
            ISpotifyAccountServices spotifyAccountServices,
            IConfiguration configuration,
            IArtistTopRelease artistTopRelease)
        {
            _spotifyAccountServices = spotifyAccountServices;
            _configuration = configuration;
            _artistTopRelease = artistTopRelease;
        }
        public async Task<IActionResult> Index()
        {
            var newReleases = await GetReleases();
            return View(newReleases);
        }

        private async Task<IEnumerable<ArtistRelease>> GetReleases()
        {
            try
            {
                var token = await _spotifyAccountServices.GetToken(
                    _configuration["Spotify:ClientID"],
                    _configuration["Spotify:ClientSecret"]);

                var newReleases = await _artistTopRelease.GetArtistReleases("7hm1BMmGSLlHTzS4hpOC9Q", "US", token);

                return newReleases;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return Enumerable.Empty<ArtistRelease>();
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
