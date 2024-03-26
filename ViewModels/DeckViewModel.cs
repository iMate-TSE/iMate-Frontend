using iMate.Models;
using System.Collections.ObjectModel;

namespace iMate.ViewModels
{
    public partial class DeckViewModel : ObservableObject
    {
        public ObservableCollection<Card> Cards { get; } = new ObservableCollection<Card>();

        public DeckViewModel() 
        { 
            Cards = GetCards(); 
        }

        private static ObservableCollection<Card> GetCards ()
        {
            return new ObservableCollection<Card>()
            {
                new Card(1, "Go for a walk", 3.0f),
                new Card(1, "Listen to rainforest sounds", 3.0f),
                new Card(1, "Cook yourself some food", 3.0f),
            };
        }
    }
}
