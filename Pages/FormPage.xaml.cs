using CommunityToolkit.Maui.Views;
using iMate.Views;

namespace iMate.Pages;

public partial class FormPage : ContentPage
{
	private FormViewModel _viewModel;

	public FormPage()
	{
		InitializeComponent();

        _viewModel = new FormViewModel();
        BindingContext = _viewModel;
    }

}