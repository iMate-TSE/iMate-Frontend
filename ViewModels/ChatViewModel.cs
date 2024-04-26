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

        public int msgIndex = -1;
        public List<Message> backlog = new List<Message>()
        {
            new Message(2, "On a scale of 1 to 10, how would you rate your overall sense of enjoyment or pleasure right now?", "Bot"),
            new Message(3, "How energized or awake do you feel at the moment?", "Bot"),
            new Message(4, "How in control do you feel of your current situation, or do you feel like things are beyond your control?", "Bot")
        };

        public ChatViewModel() 
        {
            //ConnectToServer();
            Messages = GetMessages();
            
        }

        private static ObservableCollection<Message> GetMessages()
        {
            // this eventually needs to hit the backend and do ai things
            return new ObservableCollection<Message>()
            {
                new Message(1, "Hello I am your mood assessor how are you doing today?", "Bot"),
            };
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
            

            List<string> values = new List<string>() {"1","2","3","4", "5", "6", "7", "8" , "9", "10", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };
            if (!values.Any(item => message.Content.Contains(item)) && msgIndex > -1)
            {
                Messages.Add(new Message(23, "That's great, but can you give an answer as a number from 1-10", "Bot"));
                Thread.Sleep(1000);
                Messages.Add(backlog[msgIndex]);
            }
            else
            {
                if (backlog.Count > (msgIndex + 1))
                {
                    Messages.Add(backlog[msgIndex + 1]);
                    msgIndex++;
                }
            }

        }
    }
}
