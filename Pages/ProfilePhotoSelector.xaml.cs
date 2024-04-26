namespace iMate.Pages;

public partial class ProfilePhotoSelector : ContentPage
{
    public ProfilePhotoSelector()
    {
        InitializeComponent();
    }

    private async void SelectPhoto(object? sender, EventArgs e)
    {
        ImageButton picture = (ImageButton)sender;

        switch (picture.Source.ToString())
        {
            case "File: cat.png":
                CatBorder.Stroke = Color.FromArgb("#F46197");
                BirdBorder.Stroke = Color.FromArgb("#EAE5E5");
                DogBorder.Stroke = Color.FromArgb("#EAE5E5");
                FishBorder.Stroke = Color.FromArgb("#EAE5E5");
                await SecureStorage.Default.SetAsync("photo", "cat.png");
                break;
            case "File: bird.png":
                BirdBorder.Stroke= Color.FromArgb("#F46197");
                CatBorder.Stroke = Color.FromArgb("#EAE5E5");
                DogBorder.Stroke = Color.FromArgb("#EAE5E5");
                FishBorder.Stroke = Color.FromArgb("#EAE5E5");
                await SecureStorage.Default.SetAsync("photo", "bird.png");
                break;
            case "File: dog.png":
                DogBorder.Stroke = Color.FromArgb("#F46197");
                CatBorder.Stroke = Color.FromArgb("#EAE5E5");
                BirdBorder.Stroke = Color.FromArgb("#EAE5E5");
                FishBorder.Stroke = Color.FromArgb("#EAE5E5");
                await SecureStorage.Default.SetAsync("photo", "dog.png");
                break;
            case "File: fish.png":
                FishBorder.Stroke = Color.FromArgb("#F46197");
                DogBorder.Stroke = Color.FromArgb("#EAE5E5");
                CatBorder.Stroke = Color.FromArgb("#EAE5E5");
                BirdBorder.Stroke = Color.FromArgb("#EAE5E5");
                await SecureStorage.Default.SetAsync("photo", "fish.png");
                break;
                
        }

    }
}