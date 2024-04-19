using iMate.Services;

namespace iMate.Views;


public partial class CustomTitleView : ContentView
{
	private HeaderViewModel _viewModel;

    protected IHttpService _httpService;

    public CustomTitleView()
    {
	    InitializeComponent();

	    _httpService = new HttpService();

	    _viewModel = new HeaderViewModel(_httpService);

	    BindingContext = _viewModel;
    }
    
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage(_httpService));
    }

}