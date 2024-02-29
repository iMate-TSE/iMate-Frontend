using iMate.Models;

namespace iMate.Pages;

public partial class ChatPage : ContentPage
{
	private ChatViewModel _viewModel;

    public ChatPage()
	{
		InitializeComponent();

		_viewModel = new ChatViewModel();

		BindingContext = _viewModel;
	}

    private void SendChat(object sender, EventArgs e)
    {
		// Get the Current Text from the Box
		String messageText = ((Entry)sender).Text;
		
		// Create a new Message 
		// TODO: ID and Sender 
		Message message = new Message(1, messageText, "User");

		// call view model method to add message
		_viewModel.AddMessage(message);

		// Clear the text box
		((Entry)sender).Text = "";
    }
}