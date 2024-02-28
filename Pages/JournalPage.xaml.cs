namespace iMate.Pages;

public partial class JournalPage : ContentPage
{
	public JournalPage()
	{
		InitializeComponent();
	}

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }
}