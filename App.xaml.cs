using iMate.Services;

namespace iMate
{
    public partial class App : Application
    {
        public App(IHttpService httpService)
        {
            InitializeComponent();
            MainPage = new AppShell(httpService);
        }
    }
}
