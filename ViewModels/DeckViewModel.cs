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
                new Card (1, "Go for a run!", 0.1f),
                new Card (2, "Cook a meal!", 0.2f),
                new Card (3, "Meditate for 10 minutes!", 0.3f),
                new Card (4, "Do exercise for 10 minutes!", 0.4f),
                new Card (5, "Reflect on your day!", 0.5f)
            };
        }
    }
}
