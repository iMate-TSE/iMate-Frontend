using iMate.Services;

namespace iMate.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        private readonly IHttpService _httpService;

        // Dependency Injection
        public ViewModelBase(IHttpService httpService)
        {
            _httpService = httpService;
        }

        protected IHttpService HttpService => _httpService;
    }
}
