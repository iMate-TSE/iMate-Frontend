using CommunityToolkit.Maui.Views;
using iMate.Views;

namespace iMate.Pages;

public partial class SettingsPage : ContentPage
{

    private SettingsViewModel _viewModel;

	public SettingsPage()
	{
		InitializeComponent();

        _viewModel = new SettingsViewModel();
        BindingContext = _viewModel;
    }
    private async void TriggerNavigate(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

}