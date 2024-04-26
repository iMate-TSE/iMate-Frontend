using iMate.Services;

namespace iMate.ViewModels;

public partial class HeaderViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _streak;

    [ObservableProperty]
    private string _points;
    
    [ObservableProperty]
    private string _profilePhoto;
    
    public HeaderViewModel(IHttpService httpService) : base(httpService)
    {
        FetchHeaderData();
    }

    private async void FetchHeaderData()
    {
        string token = await SecureStorage.Default.GetAsync("auth_token");
        int points = await HttpService.FetchPoints(token);
        int streak = await HttpService.FetchStreak(token);
        string pfp = await HttpService.FetchProfilePhoto(token);
        Console.WriteLine(pfp);
        
        Points = $"👑 {points}" ?? $"👑 0";
        Streak = $"🔥 {streak}" ?? $"🔥 0";
        ProfilePhoto = pfp;
    }

}