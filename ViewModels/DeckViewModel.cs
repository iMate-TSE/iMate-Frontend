using iMate.Models;
using Syncfusion.Maui.Cards;
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
                new Card(1, "Go for a walk"),
                new Card(1, "Listen to rainforest sounds"),
                new Card(1, "Cook yourself some food"),
            };
        }
    }
}
