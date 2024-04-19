using iMate.Services;

namespace iMate
{
    public partial class App : Application
    {
        public App(IHttpService httpService)
        {
            InitializeComponent();

            if (IsUserLoggedIn())
            {
                MainPage = new AppShell(httpService);
            }
            else
            {
                MainPage = new LoginPage(httpService);
            }
        }
        
        private bool IsUserLoggedIn()
        {
            // Implement logic to check secure storage for a login token or flag
            var isLoggedIn = SecureStorage.GetAsync("isLoggedIn").Result == "true";
            return isLoggedIn;
        }
    }
}
