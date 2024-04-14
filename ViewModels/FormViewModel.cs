using iMate.Models;
using System.Collections.ObjectModel;


namespace iMate.ViewModels
{

    partial class FormViewModel : ObservableObject
    {
        bool _isSelected;

        public bool _Selected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }

}
