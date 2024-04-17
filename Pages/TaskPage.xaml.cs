using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;
using iMate.Services;
using iMate.Models;

namespace iMate.Pages;

public partial class TaskPage : ContentPage, IRecipient<SetMoodMessage>
{
	private DeckViewModel _deckViewModel;
    private int side = 0;

	public TaskPage(IHttpService httpService)
	{
        InitializeComponent();

		_deckViewModel = new DeckViewModel(httpService);
		WeakReferenceMessenger.Default.Register<SetMoodMessage>(this);

		BindingContext = _deckViewModel;
	}
	
	public void Receive(SetMoodMessage message)
	{
		_deckViewModel.GetCards(message.Value);
	}

	private void FlipCard(object sender, EventArgs e)
	{
        StackLayout sl = (StackLayout)sender;
        Border Front = (Border)sl.Children[0];
        Border Back = (Border)sl.Children[1];
        
        Console.WriteLine(CardsCarousel.Position);

        sl.RotationY = 0;
        Animation animation = new Animation();
        animation.Add(0, 0.5, new Animation(v => sl.RotateYTo(v, 180), 0, 360));
        animation.Commit(this, "ChildAnimation", 5, 100, finished: delegate
        {
            if (side == 1)
            {
                side = 0;
                Front.IsVisible = true;
                Back.IsVisible = false;
            }
            else
            {
                side = 1;
                Front.IsVisible = false;
                Back.IsVisible = true;
            }
            animation.Dispose();
        });
    }
	

	private void CompleteCard(object? sender, EventArgs e)
	{
		Card currentCard = (Card)CardsCarousel.CurrentItem;
		//await DisplayAlert("Position", cardIdx.ToString(), "Close this shit");
		_deckViewModel.RemoveCard(currentCard.Id); 
	}
}