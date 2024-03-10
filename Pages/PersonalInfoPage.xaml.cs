namespace iMate.Pages;

public partial class PersonalInfoPage : ContentPage
{
    private PersonalInfoViewModel _viewModel;
    public PersonalInfoPage()
	{
		InitializeComponent();

        _viewModel = new PersonalInfoViewModel();

		BindingContext = _viewModel;

	}
	private void ChangePictureTapped(object sender, EventArgs e)
	{

	}
}