namespace iMate.ViewModels
{
    partial class PersonalInfoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _fullname;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private int _age;

        [ObservableProperty]
        private string _gender;

        public PersonalInfoViewModel()
        {
            Dictionary<String, string> ProfileData = fetchProfileData();

            _fullname = ProfileData["fullname"];
            _username = ProfileData["username"];
            _age = Int16.Parse(ProfileData["age"]);
            _gender = ProfileData["gender"];
        }

        private Dictionary<string, string> fetchProfileData()
        {
            return new Dictionary<string, string>()
            {
                ["fullname"] = "Alan Turing",
                ["username"] = "aturing",
                ["age"] = "21",
                ["gender"] = "Male"
            };
        }

    }
}
