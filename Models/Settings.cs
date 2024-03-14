namespace iMate.Models


{
    public class Settings
    {
        public Settings( bool sound, bool motion, bool motivation, bool reminder, bool sheduling, string time) 
        {

            SoundEffects = sound;
            ReducedMotion = motion;
            MotivationalMessage = motivation;
            PracticeReminder = reminder;
            SmartScheduling = sheduling;
            ReminderTime = time;

        }
        public bool SoundEffects { get; set; }
        public bool ReducedMotion {  get; set; }
        public bool MotivationalMessage { get; set; }
        public bool PracticeReminder { get; set; }
        public bool SmartScheduling { get; set; }
        public string ReminderTime { get; set; }

    }

}
