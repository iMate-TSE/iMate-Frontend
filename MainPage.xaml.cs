namespace iMate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        // Route to the correct location based on what is clicked
        private async void TriggerNavigate(object sender, TappedEventArgs e)
        {
            if (sender is Label label)
            {
                // checking to make sure a label is clicked and navigate based on the text
                switch (label.Text)
                {
                    case "Breathe":
                        SemanticScreenReader.Announce("Breathe Button Clicked");
                        await Navigation.PushAsync(new BreathePage());
                        break;
                    case "Meditate":
                        SemanticScreenReader.Announce("Meditate Button Clicked");
                        await Navigation.PushAsync(new MeditatePage());
                        break;
                    case "Chat":
                        SemanticScreenReader.Announce("Chat Button Clicked");
                        await Navigation.PushAsync(new ChatPage());
                        break;
                    case "Check Mood":
                        SemanticScreenReader.Announce("Check Mood button clicked");
                        await Navigation.PushAsync(new SliderPage());
                        break;
                }
            }
        }
    }

}
