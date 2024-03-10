using iMate.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;


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
            
        }

    }
}
