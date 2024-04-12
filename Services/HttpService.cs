using iMate.Models;
using System.Net.Http.Json;

namespace iMate.Services
{
    // Singleton
    public interface IHttpService
    {
        Task<User> FetchProfile(string token);
    }

    class HttpService : IHttpService
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://10.0.2.2:5137/api/v1/")
        };

        public HttpService()
        {
        }

        public async Task<User> FetchProfile(string token)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"Profile/?token={token}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadFromJsonAsync<User>());
                
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
