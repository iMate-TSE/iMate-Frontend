using iMate.Models;
using iMate.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using iMate.Models.ApiModels;

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
            
            GetCards();
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
        
        List<string> affirmations = new List<string>
        {
            "I create a safe and secure space for myself wherever I am.",
            "I give myself permission to do what is right for me.",
            "I am confident in my ability to overcome challenges.",
            "I use my time and talents to help others in need.",
            "What I love about myself is my ability to empathize with others.",
            "I feel proud of myself when I accomplish my goals.",
            "I give myself space to grow and learn.",
            "I allow myself to be who I am without judgment.",
            "I listen to my intuition and trust my inner guide.",
            "I accept my emotions and let them serve their purpose.",
            "I give myself the care and attention that I deserve.",
            "My drive and ambition allow me to achieve my goals.",
            "I share my talents with the world by creating meaningful art.",
            "I am good at helping others to find solutions to their problems.",
            "I am always headed in the right direction.",
            "I trust that I am on the right path.",
            "I am creatively inspired by the world around me.",
            "My mind is full of brilliant ideas.",
            "I put my energy into things that matter to me.",
            "I trust myself to make the right decision.",
            "I am becoming closer to my true self every day.",
            "I am grateful to have people in my life who support and uplift me.",
            "I am learning valuable lessons from myself every day.",
            "I am at peace with who I am as a person.",
            "I make a difference in the world by simply existing in it."
        };

        private async void GetCards()
        {
            string? currentMood = await SecureStorage.Default.GetAsync("current_mood");

            if (currentMood == null) return;
            
            
            async Task GetCard()
            {
                List<DatabaseCard> allCards = await HttpService.GetCards(currentMood);
                List<DatabaseCard> randomFour = allCards.OrderBy(c => Guid.NewGuid()).Take(4).ToList();
                
                foreach (var card in randomFour)
                {
                    Cards.Add(new Card(card.cardID, card.Content));
                    IsErrorVisible = false;
                }
                
            }
            await GetCard(); 
            
            // There is a bug with Carousel View, so this extra card is here to avoid to running
            // into issues when removing it.
            Random random = new Random();
            int randomIndex = random.Next(0, affirmations.Count);
            Cards.Add(new Card(9999, affirmations[randomIndex] + "😊"));
        }

    }
}
