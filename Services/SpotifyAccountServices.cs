using SpotifyPlus.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SpotifyPlus.Services
{
    public class SpotifyAccountServices : ISpotifyAccountServices
    {
        private readonly HttpClient _httpClient;

        public SpotifyAccountServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetToken(string clientId, string clientSecret)
        {
            //Request Body Parameter of token
            var request = new HttpRequestMessage(HttpMethod.Post, "token"); //The request passes the HTTP Verb post message and token

            //Provide authorization in the header
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"))); //Encode the client secret in base64

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"}
            });

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var authResult = await JsonSerializer.DeserializeAsync<AuthResult>(responseStream);

            return authResult.access_token;
        }
    }
}
