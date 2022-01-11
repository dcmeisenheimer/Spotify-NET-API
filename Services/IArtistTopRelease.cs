using SpotifyPlus.Models;

namespace SpotifyPlus.Services
{
    public interface IArtistTopRelease
    {
        Task<IEnumerable<ArtistRelease>> GetArtistReleases(string id, string market, string accessToken);
    }
}
