using CommunityToolkit.Maui.Views;
using iMate.Services;
using iMate.Views;

namespace iMate.Pages;

public partial class FormPage : ContentPage
{
	private FormViewModel _viewModel;

	private IHttpService _httpService;
	public FormPage(IHttpService httpService)
	{
		InitializeComponent();

		_httpService = httpService;

        _viewModel = new FormViewModel(_httpService);
        BindingContext = _viewModel;
    }

}