namespace iMate.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
    }
    private async void TriggerNavigate(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

}