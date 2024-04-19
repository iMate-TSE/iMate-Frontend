using iMate.Services;

namespace iMate.Pages;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _viewModel;

    private IHttpService _httpService;
	public LoginPage(IHttpService httpService)
	{
		InitializeComponent();

        _httpService = httpService;

        _viewModel = new LoginViewModel(_httpService);

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
            if (await _viewModel.CheckUser())
            {
                await SecureStorage.Default.SetAsync("isLoggedIn", "true");
                Application.Current.MainPage = new AppShell(_httpService);
                //await Navigation.PopAsync();  
            }

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
                if (await _viewModel.SignUp())
                {
                    await SecureStorage.Default.SetAsync("isLoggedIn", "true");
                    Application.Current.MainPage = new AppShell(_httpService);
                    //await Navigation.PushAsync(new MainPage());
                }
        }
    }

        private async void Show_Password(object sender, EventArgs e)
        {
            if (Password.IsPassword)
            {
                Show.Source = "open_eye.png";
            }
            else
            {
                Show.Source = "closed_eye.png";
                
            }
            Password.IsPassword = !Password.IsPassword;
        }
    
}