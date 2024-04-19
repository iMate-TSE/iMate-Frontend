using iMate.Services;

namespace iMate.Pages;
using SkiaSharp;
using Microcharts;

public partial class JournalPage : ContentPage
{
    
    private JournalViewModel _viewModel = null;
    
    public JournalPage(IHttpService httpService)
	{
		InitializeComponent();

        _viewModel = new JournalViewModel(httpService);

        BindingContext = _viewModel;

        foreach (var item in _viewModel.MoodEntries)
        {
            Console.WriteLine(item.Color + " " + item.Value);
        }

        chartView.Chart = _viewModel.Chart;
    }
}