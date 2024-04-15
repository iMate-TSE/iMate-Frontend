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

	private void FromPositiveNextQuestion(object? sender, EventArgs e)
	{
		double value = PositiveSlider.Value;
		if (value <=5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
	}
	
	private void FromActiveNextQuestion(object? sender, EventArgs e)
	{
		double value = Activeslider.Value;
		if (value <= 5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
		_viewModel.fetchQuestions();
	}

	private void FromPassivityNextQuestion(object? sender, EventArgs e)
	{
		double value = PassivitySlider.Value;
		if (value <= 5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
		_viewModel.fetchQuestions();
	}
	
	private void FromNegativeNextQuestion(object? sender, EventArgs e)
	{
		double value = NegativeSlider.Value;
		if (value <= 5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
	}

	private void FromIntensityNextQuestion(object? sender, EventArgs e)
	{
		double value = IntenseSlider.Value;
		if (value <= 5) _viewModel.Decide(0);
		else { _viewModel.Decide(1);}
		_viewModel.fetchQuestions();
	}
}