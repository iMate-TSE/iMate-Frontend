namespace iMate.Pages;

public partial class ChatPage : ContentPage
{
	public ChatPage()
	{
		InitializeComponent();
		ShowAlert();
	}

	public async void ShowAlert()
	{
        await DisplayAlert("This is an Alert", "Big message innit", "Kill");
    }
}