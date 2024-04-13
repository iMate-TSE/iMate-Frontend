
using System.Text.Json;
using iMate.Services;
using iMate.Models.FormModels;
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
            { "sound", "true" },
            { "motion", "true" },
            { "motivation", "false" },
            { "reminder", "true" },
            { "scheduling", "false" },
            { "time", "12:15:00" },
        };

        private string _userName;

        

        public SettingsViewModel(IHttpService httpService) : base(httpService)
        {

            GetSettings();
            
            LogOutCommand = new Command(LogOut);
            UpdateSettingsCommand = new Command(UpdateSettings);
            
            
            
        }

        public async void GetSettings()
        {
            
            await GetUsername();
            
            var settings = await HttpService.GetSettings(_userName);
            

            if (settings != null)
            {
                _settings = settings;
                SoundEffects = bool.Parse(_settings["sound"]);
                ReducedMotion = bool.Parse(_settings["motion"]);
                MotivationalMessages = bool.Parse(_settings["motivation"]);
                PracticeReminder = bool.Parse(_settings["reminder"]);
                SmartScheduling = bool.Parse(_settings["scheduling"]);
                ReminderTime = TimeSpan.Parse(_settings["time"]);
            }
        }

        public async Task GetUsername()
        {
            string? token = await SecureStorage.Default.GetAsync("auth_token");
            if (token != null)
            {
                _userName = await HttpService.GetUsername(token);
            }
            else
            {
                _userName = "user";
            }
        }

        public async void UpdateSettings()
        {
            var settingsData = new SettingsDataModel
            {
                username  = _userName,
                soundEffects= this.SoundEffects,
                reducedMotion= this.ReducedMotion,
                motivation= this.MotivationalMessages,
                practice = this.PracticeReminder,
                scheduling = this.SmartScheduling,
                reminder = this.ReminderTime
            };
            string content = JsonSerializer.Serialize(settingsData);
            
            Console.WriteLine("================"+content);
            HttpService.UpdateSettings(content);
        }

        public async void LogOut()
        {
            string token = await SecureStorage.Default.GetAsync("auth_token");
            HttpService.LogOut(token);
            SecureStorage.Default.Remove("auth_token");
        }

        public ICommand LogOutCommand { get; }
        public ICommand UpdateSettingsCommand { get; }

    }

}
