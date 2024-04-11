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

    private async void Update(object sender, TappedEventArgs e) 
    {
        _viewModel._soundEffects = SE.IsToggled;
        _viewModel._motivationalMessages = MM.IsToggled;
        _viewModel._practiceReminder = PR.IsToggled;
        _viewModel._reducedMotion = RM.IsToggled;
        _viewModel._smartScheduling = SS.IsToggled;
        _viewModel._reminderTime = RT.Time;
        _viewModel.UpdateSettings();
    }

    private async void Update(object sender, PropertyChangedEventArgs e)
    {
        _viewModel._soundEffects = SE.IsToggled;
        _viewModel._motivationalMessages = MM.IsToggled;
        _viewModel._practiceReminder = PR.IsToggled;
        _viewModel._reducedMotion = RM.IsToggled;
        _viewModel._smartScheduling = SS.IsToggled;
        _viewModel._reminderTime = RT.Time;
        _viewModel.UpdateSettings();
    }

}