using CommunityToolkit.Maui.Views;
using iMate.Services;
using iMate.Views;
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
    private async void TriggerNavigate(object sender, TappedEventArgs e)
    {
        _viewModel.LogOut();
        await Navigation.PushAsync(new LoginPage(_httpService));
    }

    private async void UpdateTap(object sender, PropertyChangedEventArgs e) 
    {
        _viewModel.SoundEffects = SE.IsToggled;
        _viewModel.MotivationalMessages = MM.IsToggled;
        _viewModel.PracticeReminder = PR.IsToggled;
        _viewModel.ReducedMotion = RM.IsToggled;
        _viewModel.SmartScheduling = SS.IsToggled;
        _viewModel.ReminderTime = RT.Time;
        _viewModel.UpdateSettings();
    }

    private async void Update(object? sender, ToggledEventArgs e)
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