namespace iMate.Views;
public partial class CustomTitleView : ContentView
{
	public CustomTitleView()
	{
		InitializeComponent();
	}
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }

}