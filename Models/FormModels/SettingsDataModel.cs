namespace iMate.Models.FormModels;

public class SettingsDataModel
{
    public string username { get; set; } = null!;
    public bool soundEffects { get; set; }
    public bool reducedMotion { get; set; }
    public bool motivation { get; set; }
    public bool practice { get; set; }
    public bool scheduling { get; set; }
    public TimeSpan reminder { get; set; }

}