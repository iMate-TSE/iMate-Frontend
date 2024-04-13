using iMate.Models;
using iMate.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace iMate.ViewModels
{
    public partial class DeckViewModel : ViewModelBase
    {
        List<Card> cards = new List<Card>();

        public ICommand GetCardsCommand { get; }

        public ObservableCollection<Card> Cards { get; set; } = new ObservableCollection<Card>();

        [ObservableProperty] 
        private bool _hasCards;

        public DeckViewModel(IHttpService httpService) : base(httpService)
        {
            GetCardsCommand = new Command(GetCards);
            HasCards = !(Cards.Count > 0);
        }

        public void RemoveCard(int index)
        {
            if (index < 0) return;
            Cards.RemoveAt(index);
            HasCards = !(Cards.Count > 0);
        }

        private void GetCards()
        {
            Console.WriteLine("Running command");
            ObservableCollection<Card> cardList = new ObservableCollection<Card>();

            string mood = "Sad"; // this needs to eventually come from the form

            async void GetCard()
            {
                foreach (var card in await HttpService.GetCards(mood))
                {
                    Console.WriteLine("=====================");
                    Console.WriteLine(card.Content);
                    cardList.Add(new Card(card.Id, card.Content));
                }

            }
            GetCard();
            Cards = cardList;
        }

    }
}
