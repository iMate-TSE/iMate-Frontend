namespace iMate.Pages
{
    public partial class SliderPage : ContentPage
    {
        public SliderPage()
        {
            InitializeComponent();
            BindingContext = new SliderPageViewModel(); // connect the mvvm to sliders page
        }
        
    }
}
    
