namespace iMate.Models.ApiModels;

public class DatabaseCard
{
    public int cardID { get; set; } = 0;

    public int cardCreditsValue { get; set; }

    public int targetMoodId { get; set; }

    public PadRanges targetMood { get; set; } = null!;

    public int moodID { get; set; }

    public PadRanges mood { get; set; } = null!;

    // Fact or Task or Other resource
    public string? Content { get; set; }

    public static int id = 0;
}