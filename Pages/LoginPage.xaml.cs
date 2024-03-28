namespace iMate.Pages;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _viewModel;
	public LoginPage()
	{
		InitializeComponent();

        _viewModel = new LoginViewModel();

        BindingContext = _viewModel;

    }

    private async void Help_Clicked(object sender, EventArgs e)
    {
		await DisplayAlert("Login", "This page will allow you to enter into the app with personalised details and data history!", "Okay");
    }

    private async void Login(object sender, EventArgs e)
    {

        string username = UserName.Text;
        username = username.Replace(" ", "");
        string password = Password.Text;
        password = password.Replace(" ", "");

        if (username == "" || password == "")
        {
            IncorrectWarning.IsVisible = false;
            NullWarning.IsVisible = true;
        }
        else if ( _viewModel.CheckUser(username, password) )
        {
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            NullWarning.IsVisible = false;
            IncorrectWarning.IsVisible= true;
        }
    }
}