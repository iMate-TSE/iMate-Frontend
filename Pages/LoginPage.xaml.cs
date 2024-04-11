using iMate.Services;

namespace iMate.Pages;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _viewModel;
	public LoginPage(IHttpService httpService)
	{
		InitializeComponent();

        _viewModel = new LoginViewModel(httpService);

        BindingContext = _viewModel;

    }

    private async void Help_Clicked(object sender, EventArgs e)
    {
		await DisplayAlert("Help", "Trying to sign up?\n Enter a username and password into the boxes then hit sign up to create an account!", "Okay");
    }

    private async void Login(object sender, EventArgs e)
    {

        string username = UserName.Text;
        username = username.Replace(" ", "");
        string password = Password.Text;
        password = password.Replace(" ", "");

        if (username == null || password == null)
        {
            IncorrectWarning.IsVisible = false;
            NullWarning.IsVisible = true;
        }
        else if (username == "" || password == "")
        {
            IncorrectWarning.IsVisible = false;
            NullWarning.IsVisible = true;
        }
        else
        {
            _viewModel._Username = username;
            _viewModel._Password = password;
            if (await _viewModel.CheckUser()) { await Navigation.PushAsync(new MainPage()); }

            NullWarning.IsVisible = false;
            IncorrectWarning.IsVisible = true;
        }

    }

        private async void Sign_Up(object sender, EventArgs e)
        {
            string username = UserName.Text;
            username = username.Replace(" ", "");
            string password = Password.Text;
            password = password.Replace(" ", "");

            if (username == null || password == null)
            {
                IncorrectWarning.IsVisible = false;
                NullWarning.IsVisible = true;
            }
            else if (username == "" || password == "")
            {
                IncorrectWarning.IsVisible = false;
                NullWarning.IsVisible = true;
            }
            else
            {
                _viewModel._Username = username;
                _viewModel._Password = password;
                if (await _viewModel.SignUp()) { await Navigation.PushAsync(new MainPage()); }
        }
    }
    
}