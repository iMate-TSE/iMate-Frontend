namespace iMate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF1cXmhKYVF3WmFZfVpgdV9DY1ZVQ2Y/P1ZhSXxXdkZiXX5ddXdRQWleWEY=");
            MainPage = new AppShell();
        }
    }
}
