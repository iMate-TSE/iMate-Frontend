namespace iMate;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void Help_Clicked(object sender, EventArgs e)
    {
		await DisplayAlert("Help", "Help is on the way, dear!!! (not)", "k");
    }

}