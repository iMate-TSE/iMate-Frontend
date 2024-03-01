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

    private void UsernameTapped(object sender, EventArgs e)
    {

    }

    private void PersonalInformationTapped(object sender, EventArgs e) 
    { 
        Navigation.PushAsync(new PersonalInfoPage());
    }

    private void NotificationsTapped(object sender, EventArgs e) { }

    private void FeedbackTapped(object sender, EventArgs e) { }

}