namespace iMate.Services
{
    // Singleton
    public interface IHttpService
    {

    }

    class HttpService : IHttpService
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://10.0.2.2:5137/api/")
        };

        public HttpService() { }
    }
    
}
