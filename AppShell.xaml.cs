using iMate.Services;

namespace iMate
{
    public partial class AppShell : Shell
    {
        public AppShell(IHttpService httpService)
        {
            InitializeComponent();

            Navigation.PushAsync(new LoginPage(httpService));

        }
    }
}
