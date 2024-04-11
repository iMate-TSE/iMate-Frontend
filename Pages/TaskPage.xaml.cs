using System.Diagnostics;

namespace iMate.Pages;

public partial class TaskPage : ContentPage
{
	private DeckViewModel _deckViewModel;
    private int side = 0;

    public TaskPage()
	{
        InitializeComponent();

        _deckViewModel = new DeckViewModel();

		BindingContext = _deckViewModel;
	}

	private void FlipCard(object sender, EventArgs e)
	{
        StackLayout sl = (StackLayout)sender;
        Border Front = (Border)sl.Children[0];
        Border Back = (Border)sl.Children[1];

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

	private void Button_OnClicked(object? sender, EventArgs e)
	{
		int cardIdx = CardsCarousel.Position;
		//await DisplayAlert("Position", cardIdx.ToString(), "Close this shit");
		_deckViewModel.RemoveCard(cardIdx); 
	}
}