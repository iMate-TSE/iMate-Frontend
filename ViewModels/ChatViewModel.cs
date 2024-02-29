using iMate.Models;
using System.Collections.ObjectModel;

namespace iMate.ViewModels
{
    /// <summary>
    /// A view model connects a page or view to the model. It is like the intermediate step. 
    /// This needs to talk to the backend.
    /// 
    /// Using an Observable means the UI will auto update when there is a change (i.e a new message)
    /// </summary>
    public partial class ChatViewModel : ObservableObject
    {
        public ObservableCollection<Message> Messages { get;  } = new ObservableCollection<Message>();

        public ChatViewModel() 
        {
            Messages = GetMessages();
        }

        private static ObservableCollection<Message> GetMessages()
        {
            // this eventually needs to hit the backend and do ai things
            return new ObservableCollection<Message>()
            {
                new Message(1, "Hello Bot how are you doing today?", "Richard"),
                new Message(2, "Hey Richard! I'm doing well, thank you. How about you?", "Bot"),
                new Message(3, "I'm doing great, just catching up on some work. How can I assist you today?", "Richard"),
                new Message(4, "I need help with setting up a new feature in our application. Are you available to guide me through it?", "Richard"),
                new Message(5, "Of course! I'm here to help. Let's get started. What feature are you looking to set up?", "Bot"),
                new Message(6, "I want to implement a chat feature where users can communicate in real-time. Can you help me with that?", "Richard")
            };
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
        }
    }
}
