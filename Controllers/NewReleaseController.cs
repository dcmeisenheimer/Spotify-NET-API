using Microsoft.AspNetCore.Mvc;
using SpotifyPlus.Models;
using SpotifyPlus.Services;
using System.Diagnostics;

namespace SpotifyPlus.Controllers
{
    public class NewReleaseController : Controller
    {
        private readonly ISpotifyAccountServices _spotifyAccountServices;
        private readonly IConfiguration _configuration;
        private readonly INewRelease _newRelease;

        public NewReleaseController(
            ISpotifyAccountServices spotifyAccountServices,
            IConfiguration configuration,
            INewRelease newRelease)
        {
            _spotifyAccountServices = spotifyAccountServices;
            _configuration = configuration;
            _newRelease = newRelease;
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

                var newReleases = await _newRelease.GetNewReleases("US", 24, token);

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
