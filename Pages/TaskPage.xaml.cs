namespace iMate.Pages;

public partial class TaskPage : ContentPage
{
	private DeckViewModel _deckViewModel;

	public TaskPage()
	{
		InitializeComponent();

		_deckViewModel = new DeckViewModel();

		BindingContext = _deckViewModel;
	}

	private void flipCard()
	{

	}

}