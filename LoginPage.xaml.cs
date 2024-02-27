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

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private async void Login(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}