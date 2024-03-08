using CommunityToolkit.Maui.Views;
using iMate.Views;

namespace iMate.Pages;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private void NavigateSettings(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }

    private void UsernameTapped(object sender, EventArgs e)
    {

    }

    private void PersonalInformationTapped(object sender, EventArgs e) 
    { 
        Navigation.PushAsync(new PersonalInfoPage());
    }

    private void NotificationsTapped(object sender, EventArgs e) { }

    private async void FeedbackTapped(object sender, EventArgs e)
    {
        string feedback = await DisplayPromptAsync("Feedback", "Enter any feedback you may have:");
    }

}