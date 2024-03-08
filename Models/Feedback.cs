namespace iMate.Models
{
    internal class Feedback
    {
        public Feedback() { }

        public Feedback(int id, string content)
        { 
            Id = id;
            Content = content;
        }
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
