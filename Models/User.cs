namespace iMate.Models;

public class User
{
    public int userID { get; set; }
    
    public string? userName { get; set; }

    public string? avatarPath { get; set; }

    public string? encryptedPassword { get; set; }

    public int? age { get; set; }

    public string? gender { get; set; }

    public int credits { get; set; }

    public int streak { get; set; }
    
    public int? settingsID { get; set; }

    public Settings Settings { get; set; } = null!;
}