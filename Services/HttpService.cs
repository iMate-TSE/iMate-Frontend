using iMate.Models;
using System.Net.Http.Json;

namespace iMate.Services
{
    // Singleton
    public interface IHttpService
    {
        Task<List<Card>> GetCards();
    }

    class HttpService : IHttpService
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://10.0.2.2:5137/api/")
        };

        public HttpService() { }

        public async Task<List<Card>> GetCards()
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync("Card");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadFromJsonAsync<List<Card>>());

                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                else
                {
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
