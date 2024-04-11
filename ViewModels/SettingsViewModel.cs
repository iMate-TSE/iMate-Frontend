
using iMate.Services;
using System.Windows.Input;

namespace iMate.ViewModels
{
    partial class SettingsViewModel : ViewModelBase
    {

        [ObservableProperty]
        private bool _soundEffects;

        [ObservableProperty]
        private bool _reducedMotion;

        [ObservableProperty]
        private bool _motivationalMessages;

        [ObservableProperty]
        private bool _practiceReminder;

        [ObservableProperty]
        private bool _smartScheduling;

        [ObservableProperty]
        private TimeSpan _reminderTime;

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
            string? token = await SecureStorage.Default.GetAsync("auth_token");
            if (token != null)
            {
                userName = await HttpService.GetUsername(token);
            }
        }

        public async void UpdateSettings() 
        {
            HttpService.UpdateSettings(userName, SoundEffects, ReducedMotion, MotivationalMessages, PracticeReminder, SmartScheduling, ReminderTime.ToString());
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
