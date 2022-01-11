namespace SpotifyPlus.Services
{
    public interface ISpotifyAccountServices
    {
        Task<string> GetToken(string clientId, string clientSecret); //Access Token
    }
}
