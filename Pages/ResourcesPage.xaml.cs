namespace iMate.Pages;

public partial class ResourcesPage : ContentPage
{
	public ResourcesPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (sender is Label label)
        {
            try
            {
                Uri uri = new Uri("https://" + label.Text);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}