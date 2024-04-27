using iMate.Services;

namespace iMate.Pages;

public partial class PersonalInfoPage : ContentPage
{
    private PersonalInfoViewModel _viewModel;
    public PersonalInfoPage(IHttpService httpService)
	{
		InitializeComponent();

        _viewModel = new PersonalInfoViewModel(httpService);

		BindingContext = _viewModel;

	}
	private void ChangePictureTapped(object sender, EventArgs e)
	{
		Navigation.PushAsync(new ProfilePhotoSelector());
	}
}