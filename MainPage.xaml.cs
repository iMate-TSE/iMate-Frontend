using System.Diagnostics;

namespace iMate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void TriggerNavigate(object sender, TappedEventArgs e)
        {
            if (sender is Label label)
            {
                switch (label.Text)
                {
                    case "Breathe":
                        SemanticScreenReader.Announce("Breathe Button Clicked");
                        break;
                    case "Meditate":
                        SemanticScreenReader.Announce("Meditate Button Clicked");
                        break;
                    case "Chat":
                        SemanticScreenReader.Announce("Chat Button Clicked");
                        await Navigation.PushAsync(new ChatPage());
                        break;
                    case "Check Mood":
                        SemanticScreenReader.Announce("Check Mood button clicked");
                        break;
                }
            }
        }
    }

}
