using System.Text.Json;
using System.Windows.Input;
using iMate.Services;
using iMate.Models;
using iMate.Models.FormModels;

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

        [ObservableProperty] private string _profilePhoto;

        public ICommand updateProfileCommand { get; set; }

        public PersonalInfoViewModel(IHttpService httpService) : base(httpService)
        {
            fetchProfileData();
            updateProfileCommand = new Command(UpdateProfile);
        }

        private void fetchProfileData()
        {
            async void getProfileAsync()
            {
                string token = await SecureStorage.Default.GetAsync("auth_token");
                User Profile = await HttpService.FetchProfile(token);
                if (Profile != null)
                {
                    Age = Profile.age ?? 0;
                    Username = Profile.userName;
                    Gender = Profile.gender;
                    Fullname = Profile.fullName;
                    ProfilePhoto = Profile.avatarPath ?? "fish.png";
                }
            }


            getProfileAsync();


            Fullname = "No User";
            Username = "No User";
            Age = 0;
            Gender = "N/A";
            ProfilePhoto = "fish.png";
        }

        public async void UpdateProfile()
        {
            string currentToken = await SecureStorage.Default.GetAsync("auth_token");
            string currentPhoto = await SecureStorage.Default.GetAsync("photo") ?? "fish.png";
            
            var profileData = new ProfileDataModel
            {
                fullname = Fullname,
                token = currentToken,
                username = Username,
                age = Age,
                gender = Gender,
                avatarPath = currentPhoto
            };

            string content = JsonSerializer.Serialize(profileData);

            HttpService.UpdateProfile(content);
        }
    }
}
