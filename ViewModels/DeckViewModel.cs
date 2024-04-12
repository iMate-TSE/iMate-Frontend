using iMate.Models;
using iMate.Services;
using System.Collections.ObjectModel;

namespace iMate.ViewModels
{
    public partial class DeckViewModel : ViewModelBase
    {
        List<Card> cards = new List<Card>();

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

        private ObservableCollection<Card> GetCards()
        {
            Console.WriteLine("Running command");
            ObservableCollection<Card> cardList = new ObservableCollection<Card>();

            string mood = "Happy"; // this needs to eventually come from the form

            async void GetCard()
            {
                List<Card> cards = await HttpService.GetCards(mood);
                foreach (var card in cards)
                {
                    cards.Add(card);
                }

            }
            GetCard();

            return cardList;
        }

    }
}
