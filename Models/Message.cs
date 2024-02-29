namespace iMate.Models
{
    public class Message
    {
        public Message() { }
        public Message(int id, string message, string sender) : this()
        {
            Id = id;
            Content = message;
            Sender = sender;
            DateTimeSent = DateTime.Now.ToString("HH:mm");
        }
        public int Id { get; set; }

        public string Content { get; set; }

        public String DateTimeSent { get; set; }

        public string Sender { get; set; }
    }
}
