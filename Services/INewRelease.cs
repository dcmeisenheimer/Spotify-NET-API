using SpotifyPlus.Models;

namespace SpotifyPlus.Services
{
    public interface INewRelease
    {
        Task<IEnumerable<ArtistRelease>> GetNewReleases(string countryCode, int limit, string accessToken);
    }
}
