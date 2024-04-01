using iMate.Models;
using System.Collections.ObjectModel;

namespace iMate.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private string _Username;

        private string _Password;

        public LoginViewModel() 
        {

            Dictionary<String, string> details = tempUser();

            _Username = details["username"];
            _Password = details["password"];

        }


        private Dictionary<string, string> tempUser()
        {

            return new Dictionary<string, string>()
            {
                ["username"] = "aturing",
                ["password"] = "password"
            };

        }

        public bool CheckUser (string username, string password)
        {
            if (_Username == username && _Password == password)
            {
                return true;
            }
            return false;
        }
    }

}
