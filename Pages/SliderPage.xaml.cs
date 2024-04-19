using iMate.Services;

namespace iMate.Pages;

public partial class SliderPage : ContentPage
{
	private FormViewModel _viewModel;
	
	public SliderPage(IHttpService httpService)
	{
		_viewModel = new FormViewModel(httpService);
		BindingContext = _viewModel;
		
		InitializeComponent();
	}

	private void UserNegativeClicked(object? sender, EventArgs e)
	{
		_viewModel.Decide(0);
	}

	private void UserPositiveClicked(object? sender, EventArgs e)
	{
		_viewModel.Decide(1);
	}
	
	private void UserIDKClicked(object? sender, EventArgs e)
	{
		_viewModel.IsPADForm = true;
		_viewModel.ShowPADSubmit = true;
	}
	
	private void SubmitPADForm(object? sender, EventArgs e)
	{
		int p = (int)PSlider.Value;
		int a = (int)ASlider.Value;
		int d = (int)DSlider.Value;
		_viewModel.PADRegression(p,a,d);
	}
	
	
	private void FromPositiveNextQuestion(object? sender, EventArgs e)
	{
		double value = PositiveSlider.Value;
		_viewModel.PositiveValue = value;
		if (value <=5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
	}
	
	private void FromActiveNextQuestion(object? sender, EventArgs e)
	{
		double value = Activeslider.Value;
		_viewModel.ActivityValue = value;
		if (value <= 5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
	}

	private void FromPassivityNextQuestion(object? sender, EventArgs e)
	{
		double value = PassivitySlider.Value;
		_viewModel.PassivityValue = value;
		if (value <= 5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
	}
	
	private void FromNegativeNextQuestion(object? sender, EventArgs e)
	{
		double value = NegativeSlider.Value;
		_viewModel.NegativeValue = value;
		if (value <= 5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
	}

	private void FromIntensityNextQuestion(object? sender, EventArgs e)
	{
		double value = IntenseSlider.Value;
		_viewModel.IntensityValue = value;
		if (value <= 5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
	}

	private void RadioButton_OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
	{
		RadioButton button = (RadioButton)sender;
		if (button.IsChecked)
		{
			string selectedValue = button.Content.ToString();
			int numericValue = -1;

			switch (selectedValue)
			{
				case "Always":
					numericValue = 3;
					break;
				case "Sometimes":
					numericValue = 1;
					break;
				case "Never":
					numericValue = 0;
					break;
			}

			int questionid = int.Parse(button.GroupName);
			_viewModel.UpdateResponses(questionid, numericValue);
		}
	}
}