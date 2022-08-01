
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : Shell
    {
        public HomePage()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(MainPage),typeof(MainPage));
        }

        private async void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(AutenticationPage)}");
            Preferences.Remove("Username");
        }
    }
}