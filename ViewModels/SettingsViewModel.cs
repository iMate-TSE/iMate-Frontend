
using iMate.Services;
using System.Windows.Input;

namespace iMate.ViewModels
{
    partial class SettingsViewModel : ViewModelBase
    {

        [ObservableProperty]
        public bool _soundEffects;

        [ObservableProperty]
        public bool _reducedMotion;

        [ObservableProperty]
        public bool _motivationalMessages;

        [ObservableProperty]
        public bool _practiceReminder;

        [ObservableProperty]
        public bool _smartScheduling;

        [ObservableProperty]
        public TimeSpan _reminderTime;

        private Dictionary<string, string> _settings = new Dictionary<string, string>();

        private string userName;

        

        public SettingsViewModel(IHttpService httpService) : base(httpService)
        {
            GetUsername();
            GetSettings();
            LogOutCommand = new Command(LogOut);
            UpdateSettingsCommand = new Command(UpdateSettings);
            _soundEffects = bool.Parse(_settings["sound"]);
            _reducedMotion = bool.Parse(_settings["motion"]);
            _motivationalMessages = bool.Parse(_settings["motivation"]);
            _practiceReminder = bool.Parse(_settings["reminder"]);
            _smartScheduling = bool.Parse(_settings["scheduling"]);
            _reminderTime = TimeSpan.Parse(_settings["time"]);

        }

        public async void GetSettings()
        {
            _settings = await HttpService.GetSettings(userName);
        }

        public async void GetUsername()
        {
            string token = await SecureStorage.Default.GetAsync("auth_token");
            userName = await HttpService.GetUsername(token);
        }

        public async void UpdateSettings() 
        {
            HttpService.UpdateSettings(userName, _soundEffects, _reducedMotion, _motivationalMessages, _practiceReminder, _smartScheduling, _reminderTime.ToString());
        }

        public async void LogOut()
        {
            HttpService.LogOut(userName);
            SecureStorage.Default.Remove("auth_token");
        }

        public ICommand LogOutCommand { get; }
        public ICommand UpdateSettingsCommand { get; }
    }

}
