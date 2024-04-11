using iMate.Models;
using iMate.Services;
using System.Collections.ObjectModel;

namespace iMate.ViewModels
{
    public partial class DeckViewModel : ViewModelBase
    {
        List<Card> cards = new List<Card>

        public ObservableCollection<Card> Cards { get; } = new ObservableCollection<Card>();

        [ObservableProperty] 
        private bool _hasCards;

        public DeckViewModel(IHttpService httpService) : base(httpService) 
        { 
            Cards = GetCards();
            HasCards = !(Cards.Count > 0);
        }

        public void RemoveCard(int index)
        {
            if (index < 0) return;
            Cards.RemoveAt(index);
            HasCards = !(Cards.Count > 0);
        }

        private static ObservableCollection<Card> GetCards ()
        {
            return new ObservableCollection<Card>()
            {
                new Card(1, "Go for a walk"),
                new Card(1, "Listen to rainforest sounds"),
                new Card(1, "Cook yourself some food"),

                GetCard();
            };
        }

        public async void GetCard()
        {
            Console.WriteLine("Running command");

            string mood = "Happy"; // this needs to eventually come from the form

            cards = await HttpService.GetCards(mood);
        }


    }
}
