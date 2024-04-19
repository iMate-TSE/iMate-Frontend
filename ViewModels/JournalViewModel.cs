using System.Collections.ObjectModel;
using iMate.Models.ApiModels;
using iMate.Services;
using SkiaSharp;
using Microcharts;

namespace iMate.ViewModels;

partial class JournalViewModel : ViewModelBase
{
    [ObservableProperty] public ObservableCollection<ChartEntry> _moodEntries = new ObservableCollection<ChartEntry>()
    {
        new ChartEntry(0)
        {
            Label = "Error",
            ValueLabel = "Error",
            Color = SKColors.Crimson
        },
    };

    [ObservableProperty] private LineChart _chart;

    private Dictionary<int, string> moodColourMap = new Dictionary<int, string>()
    {
        
    };
    
    public JournalViewModel(IHttpService httpService) : base(httpService)
    {
        GetJournal();
        Console.WriteLine("HELL(OOOOOOOOOOOOOOOOOOO");

        foreach (var e in MoodEntries)
        {   
            Console.WriteLine("FROM CONSTR LOOOP" + e.Value);
        }
        
        Chart = new LineChart()
        {
            Entries = MoodEntries,
            LabelTextSize = 35,
            LabelOrientation = Orientation.Horizontal,
            BackgroundColor = SKColor.Parse("#EAE5E5"),
        };
    }
    
    public async void GetJournal()
    {
        async Task GetData()
        {
            string? token = await SecureStorage.Default.GetAsync("auth_token");
            if (token != null)
            {
                List<MoodEntry> thisWeek = await HttpService.GetJournalData(token);

                Console.WriteLine("=================" + thisWeek.Count);

                foreach (var card in thisWeek)
                {
                    string label = card.date.DayOfWeek switch
                    {
                        DayOfWeek.Friday => "Fri",
                        DayOfWeek.Monday => "Mon",
                        DayOfWeek.Saturday => "Sat",
                        DayOfWeek.Sunday => "Sun",
                        DayOfWeek.Thursday => "Thu",
                        DayOfWeek.Tuesday => "Tue",
                        DayOfWeek.Wednesday => "Wed"
                    };


                    string valueLabel = "a";

                    SKColor color = card.moodID switch
                    {
                        1 => SKColor.Parse("#FFFF00"),
                        2 => SKColor.Parse("#FFC107"),
                        3 => SKColor.Parse("#F08080"),
                        4 => SKColor.Parse("#90EE90"),
                        5 => SKColor.Parse("#C0392B"),
                        6 => SKColor.Parse("#9932CC"),
                        7 => SKColor.Parse("#EEDC82"),
                        8 => SKColor.Parse("#708090"),
                        9 => SKColor.Parse("#4169E1"),
                        10 => SKColor.Parse("#A9A9A9"),
                        11 => SKColor.Parse("#36454C"),
                        12 => SKColor.Parse("#D3D3D3"),
                        13 => SKColor.Parse("#2F4F4F")
                    };

                    ChartEntry entry = new ChartEntry(card.Id)
                    {
                        Label = label,
                        ValueLabel = valueLabel,
                        Color = color
                    };
                    
                    MoodEntries.Add(entry);
                }
            }
            else
            {
                MoodEntries.Add(new ChartEntry(0)
                {
                    Label = "Error",
                    ValueLabel = "Error",
                    Color = SKColors.Crimson
                });
            }
        }
        await GetData(); 
    }
}