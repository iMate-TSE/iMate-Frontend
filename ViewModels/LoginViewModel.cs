using iMate.Services;
using System.Windows.Input;

namespace iMate.ViewModels
{
    partial class LoginViewModel : ViewModelBase
    {
        public string _Username { get; set; }

        public string _Password { get; set; }


        public LoginViewModel(IHttpService httpService) : base(httpService)
        {
            _Username = "";
            _Password = "";
        }

        

        public async Task<bool> CheckUser ()
        {
            string token = "404";

            token = await HttpService.Login(_Username, _Password);

            if (token == "404" )
            {
                return false;
            }

            await SecureStorage.Default.SetAsync("auth_token", token);
            return true;
        }

        public async Task<bool> SignUp()
        {
            HttpService.SignUpUser(_Username, _Password);

            string token = "";
            token = await HttpService.Login(_Username, _Password);
            if (token == "404")
            {
                return false;
            }

            await SecureStorage.Default.SetAsync("auth_token", token);
            HttpService.CreateDefaultSettings(_Username);
            return true;
        }

    }

}
