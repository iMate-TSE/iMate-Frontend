using iMate.Models;
using iMate.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;

namespace iMate.ViewModels
{
    public partial class DeckViewModel : ViewModelBase
    {
        [ObservableProperty]
        public ObservableCollection<Card> _cards;
        
        public event PropertyChangedEventHandler CardsCountChanged;
        
        private bool _hasCards;

        public bool HasCards
        {
            get
            {
                return Cards.Count > 0;
            }
            set
            {
                if (_hasCards != value)
                {
                    _hasCards = value;
                    OnPropertyChanged(nameof(HasCards));
                }
            }
        }

        [ObservableProperty]
        private bool _isErrorVisible = true;
        

        private void OnPropertyChanged(string name)
        {
            CardsCountChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        

        public DeckViewModel(IHttpService httpService) : base(httpService)
        {
            Cards = new ObservableCollection<Card>();
            
            // if (mood exists) {
            GetCards();
            // }
        }

        public async void RemoveCard(int id)
        {
            if (Cards.Count > 1)
            {
                foreach (Card card in Cards)
                {
                    
                    if (card.Id == id && card.Id != 9999)
                    {
                        Cards.Remove(card);
                        break;
                    }
                }
            }

            if (Cards.Count == 1)
            {
                HasCards = false;
                IsErrorVisible = true;
            }
        }

        private async void GetCards()
        {
            Console.WriteLine("Running command");
            ObservableCollection<Card> cardList = new ObservableCollection<Card>();

            string mood = "Sad"; // this needs to eventually come from the form

            async Task GetCard()
            {
                foreach (var card in await HttpService.GetCards(mood))
                {
                    Cards.Add(new Card(card.cardID, card.Content));
                    IsErrorVisible = false;
                }
                

            }
            await GetCard(); 
            
            // There is a bug with Carousel View, so this extra card is here to avoid to running
            // into issues when removing it.
            Cards.Add(new Card(9999,"That's all your available tasks! \n 😊"));
        }

    }
}
