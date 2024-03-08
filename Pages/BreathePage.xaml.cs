using System.Diagnostics;

namespace iMate.Pages;

public partial class BreathePage : ContentPage
{
    private int BreatheTime = 1;
	public BreathePage()
	{
		InitializeComponent();
	}

    private void SetText(string text)
    {
        CircleText.Text = text; 
    }

    private int ConvertTimeToCount(int time)
    {
        int count = (time * 60) / 10;

        return count;
    }

    private async void Animate(object sender, TappedEventArgs e)
    {
        for (int i = 0; i < ConvertTimeToCount(BreatheTime); i++)
        {
            SetText("Inhale");
            await BreathingCircle.ScaleTo(2, 5000, Easing.CubicInOut);
            SetText("Exhale");
            await BreathingCircle.ScaleTo(1, 5000, Easing.CubicInOut);
        }
        SetText("Touch to Start");
    }

    private void TimePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1 && picker.ItemsSource != null && picker != null)
        {
            string? item = (string?)picker.ItemsSource[selectedIndex];
            if (item != null) {
                BreatheTime = Int32.Parse(item.Split(' ')[0]);
            }
           

            Debug.WriteLine(BreatheTime);  
        }
    }

    private void StopAnimation(object sender, EventArgs e)
    {
        BreathingCircle.CancelAnimations();
        BreathingCircle.Scale = 1;
        SetText("Touch to Start");
    }
}