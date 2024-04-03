namespace iMate.ViewModels;

public partial class HeaderViewModel : ObservableObject
{
    [ObservableProperty]
    private string _streak;

    [ObservableProperty]
    private string _points;
    
    public HeaderViewModel()
    {
        Dictionary<String, string> headerData = FetchHeaderData();

        _streak = $"🔥 {headerData["steak"]}";
        _points = $"👑 {headerData["points"]}";
    }

    private Dictionary<string, string> FetchHeaderData()
    {
        return new Dictionary<string, string>()
        {
            ["steak"] = "124",
            ["points"] = "1000",
        };
    }

}