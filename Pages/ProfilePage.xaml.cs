using CommunityToolkit.Maui.Views;
using iMate.Services;
using iMate.Views;

namespace iMate.Pages;

public partial class ProfilePage : ContentPage
{
    private PersonalInfoViewModel _viewmodel;
    private IHttpService _httpService;
    
    public ProfilePage(IHttpService httpService)
    {
        InitializeComponent();

        _httpService = httpService;

        _viewmodel = new PersonalInfoViewModel(_httpService);

        BindingContext = _viewmodel;
    }

    private void NavigateSettings(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage(_httpService));
    }

    private void UsernameTapped(object sender, EventArgs e)
    {

    }

    private void PersonalInformationTapped(object sender, EventArgs e) 
    { 
        Navigation.PushAsync(new PersonalInfoPage(_httpService));
    }

    private void NotificationsTapped(object sender, EventArgs e) { }

    private async void FeedbackTapped(object sender, EventArgs e)
    {
        string feedback = await DisplayPromptAsync("Feedback", "Enter any feedback you may have:");
    }

}