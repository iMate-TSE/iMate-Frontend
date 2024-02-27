using Microsoft.Maui.ApplicationModel.Communication;
using System.Runtime.Intrinsics.X86;

namespace iMate;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
    }
    private async void TriggerNavigate(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    }