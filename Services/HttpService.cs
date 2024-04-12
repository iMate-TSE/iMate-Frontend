using iMate.Models;
using System.Net.Http.Json;

namespace iMate.Services
{
    // Singleton
    public interface IHttpService
    {

        Task<List<Card>> GetCards(string mood);
        Task<User> FetchProfile(string token);
    }


    class HttpService : IHttpService
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://10.0.2.2:5137/api/v1/")
        };


        public HttpService() { }
        
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
                

        }
        
        public async Task<List<Card>> GetCards(string mood)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync($"getCards?mood={mood}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadFromJsonAsync<List<Card>>());
                
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                else
                    return new List<Card>();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Card>();
            }
        }
    }
}
