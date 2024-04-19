using iMate.Services;

namespace iMate.ViewModels;

public partial class HeaderViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _streak;

    [ObservableProperty]
    private string _points;
    
    public HeaderViewModel(IHttpService httpService) : base(httpService)
    {
        FetchHeaderData();
    }

    private async void FetchHeaderData()
    {
        string token = await SecureStorage.Default.GetAsync("auth_token");
        int points = await HttpService.FetchPoints(token);
        Points = $"👑 {points}" ?? $"👑 0";
        Streak = $"🔥 0";
    }

}