using iMate.Services;
using System.ComponentModel;
namespace iMate.Pages;

public partial class SettingsPage : ContentPage
{

    private SettingsViewModel _viewModel;
    private IHttpService _httpService;

	public SettingsPage(IHttpService httpService)
	{
		InitializeComponent();
        _httpService = httpService;

        _viewModel = new SettingsViewModel(_httpService);
        BindingContext = _viewModel;
    }
    private async void Logout(object sender, TappedEventArgs e)
    {
        await SecureStorage.Default.SetAsync("isLoggedIn", "false");
        _viewModel.LogOut();
        Application.Current.MainPage = new LoginPage(_httpService);
    }
    

    private async void Update(object sender, EventArgs e)
    {
        _viewModel.SoundEffects = SE.IsToggled;
        _viewModel.MotivationalMessages = MM.IsToggled;
        _viewModel.PracticeReminder = PR.IsToggled;
        _viewModel.ReducedMotion = RM.IsToggled;
        _viewModel.SmartScheduling = SS.IsToggled;
        _viewModel.ReminderTime = RT.Time;
        _viewModel.UpdateSettings();
    }
}