using iMate.Services;

namespace iMate.Pages;
using SkiaSharp;
using Microcharts;

public partial class JournalPage : ContentPage
{

    private JournalViewModel _viewModel;
    
    public JournalPage(IHttpService httpService)
	{
		InitializeComponent();

        _viewModel = new JournalViewModel(httpService);

        BindingContext = _viewModel;
    }
}