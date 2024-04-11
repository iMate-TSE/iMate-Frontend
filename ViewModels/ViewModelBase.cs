using iMate.Services;


namespace iMate.ViewModels
{
    internal class ViewModelBase : ObservableObject
    {
        private readonly IHttpService _httpService;

        // Dependency Injection
        public ViewModelBase(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public IHttpService HttpService => _httpService;
    }
}
