
namespace iMate.ViewModels
{
    partial class SettingsViewModel : ObservableObject
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

        public SettingsViewModel() 
        {

            Dictionary<String, string> Settings = fetchSettings();

            _soundEffects = bool.Parse(Settings["sound"]);
            _reducedMotion = bool.Parse(Settings["motion"]);
            _motivationalMessages = bool.Parse(Settings["motivation"]);
            _practiceReminder = bool.Parse(Settings["reminder"]);
            _smartScheduling = bool.Parse(Settings["scheduling"]);
            _reminderTime = TimeSpan.Parse(Settings["time"]);

        }

        private Dictionary<string, string> fetchSettings() 
        {

            return new Dictionary<string, string>()
            {
                ["sound"] = "True",
                ["motion"] = "False",
                ["motivation"] = "False",
                ["reminder"] = "True",
                ["scheduling"] = "True",
                ["time"] = "16:25:00"
            };

        }

    }

}
