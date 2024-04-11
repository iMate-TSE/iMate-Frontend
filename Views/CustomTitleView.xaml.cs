using iMate.Services;
namespace iMate.Views;
public partial class CustomTitleView : ContentView
{
	private HeaderViewModel _viewModel;

    private IHttpService _httpService;

    public CustomTitleView(IHttpService httpService)
	{
		InitializeComponent();

        _httpService = httpService;

		_viewModel = new HeaderViewModel();

		BindingContext = _viewModel;
	}
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage(_httpService));
    }

}