using iMate.Services;

namespace iMate.Views;
public partial class CustomTitleView : ContentView
{
	private HeaderViewModel _viewModel;
	public CustomTitleView()
	{
		InitializeComponent();

		_viewModel = new HeaderViewModel();

		BindingContext = _viewModel;
	}

	protected IHttpService _httpservice;
	public CustomTitleView(IHttpService httpService)
	{
		InitializeComponent();

		_httpservice = httpService;

		_viewModel = new HeaderViewModel();

		BindingContext = _viewModel;
	}
	
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage(_httpservice));
    }

}