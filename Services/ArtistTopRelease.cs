using SpotifyPlus.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SpotifyPlus.Services
{
    public class ArtistTopRelease : IArtistTopRelease
    {
        private readonly HttpClient _httpClient;

        public ArtistTopRelease(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ArtistRelease>> GetArtistReleases(string id, string market, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"artists/{id}/top-tracks?market={market}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetTopArtistTracks>(responseStream);

            return responseObject?.tracks?.Select(i => new ArtistRelease
            {
                Name = i.name,
                Date = i.album.release_date,
                ImageUrl = i.album.images.FirstOrDefault().url,
                Link = i.external_urls.spotify,
                Artists = string.Join(",", i.album.artists.Select(i => i.name))
            }); ;
        }
    }
}
