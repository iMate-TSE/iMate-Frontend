using iMate.Services;

namespace iMate
{
    public partial class AppShell : Shell
    {
        private IHttpService _httpService;
        public AppShell()
        {
            InitializeComponent();

            Navigation.PushAsync(new LoginPage(_httpService));

        }
    }
}
