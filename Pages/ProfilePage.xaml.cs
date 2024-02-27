namespace iMate.Pages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}


    // TEMPORARY WAY TO NAVIGATE TO SETTINGS!!!!!
    private void NavigateSettings(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }

}