namespace iMate.Models.ApiModels;

public class MoodEntry
{
    public int Id { get; set; }
    
    public int userID { get; set; }
    
    public DateTime date { get; set; }

    public int? moodID { get; set; }
}