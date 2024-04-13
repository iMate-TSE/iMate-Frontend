using iMate.Services;
using iMate.Models;

namespace iMate.ViewModels
{
    partial class PersonalInfoViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _fullname;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private int _age;

        [ObservableProperty]
        private string _gender;
        
        

        public PersonalInfoViewModel(IHttpService httpService) : base(httpService)
        {
            Dictionary<String, string> ProfileData = fetchProfileData();

            _fullname = ProfileData["fullname"];
            _username = ProfileData["username"];
            _age = Int16.Parse(ProfileData["age"]);
            _gender = ProfileData["gender"];
        }

        private Dictionary<string, string> fetchProfileData()
        {
            User? profile = null;
            
            async void getProfileAsync()
            {
                string token = await SecureStorage.Default.GetAsync("auth_token");
                User? Profile = await HttpService.FetchProfile(token);
                profile = Profile;
            }

            if (profile != null)
            {
                return new Dictionary<string, string>()
                {
                    ["fullname"] = profile.userName,
                    ["username"] = profile.userName,
                    ["age"] = profile.age.ToString(),
                    ["gender"] = profile.gender
                };
            }

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
