
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

        private Dictionary<string, string> _settings = new Dictionary<string, string>()
        {
            { "sound", "false" },
            { "motion", "false" },
            { "motivation", "false" },
            { "reminder", "false" },
            { "scheduling", "false" },
            { "time", "00:00:00" },
        };

        private string userName = "user";

        

        public SettingsViewModel(IHttpService httpService) : base(httpService)
        {
            //GetUsername();
            //GetSettings();
            LogOutCommand = new Command(LogOut);
            //UpdateSettingsCommand = new Command(UpdateSettings);
            _soundEffects = bool.Parse(_settings["sound"]);
            _reducedMotion = bool.Parse(_settings["motion"]);
            _motivationalMessages = bool.Parse(_settings["motivation"]);
            _practiceReminder = bool.Parse(_settings["reminder"]);
            _smartScheduling = bool.Parse(_settings["scheduling"]);
            _reminderTime = TimeSpan.Parse(_settings["time"]);

        }

        /*public async void GetSettings()
        {
            
            var settings = await HttpService.GetSettings(userName);

            if (settings != null)
            {
                _settings = settings;
            }
        }*/

        /*public async void GetUsername()
        {
            string? token = await SecureStorage.Default.GetAsync("auth_token");
            if (token != null)
            {
                userName = await HttpService.GetUsername(token);
            }
            else
            {
                userName = "user";
            }
        }*/

        /*public async void UpdateSettings() 
        {
            HttpService.UpdateSettings(userName, SoundEffects, ReducedMotion, MotivationalMessages, PracticeReminder, SmartScheduling, ReminderTime.ToString());
        }*/

        public async void LogOut()
        {
            string token = await SecureStorage.Default.GetAsync("auth_token");
            //HttpService.LogOut(token);
            SecureStorage.Default.Remove("auth_token");
        }

        public ICommand LogOutCommand { get; }
        public ICommand UpdateSettingsCommand { get; }
    }

}
