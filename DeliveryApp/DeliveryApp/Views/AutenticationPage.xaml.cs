
using DeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutenticationPage : ContentPage
    {
        public AutenticationPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            string uname = Preferences.Get("Username", string.Empty);
            if (String.IsNullOrEmpty(uname))
            {
                Shell.Current.GoToAsync($"//{nameof(AutenticationPage)}");
            }
            else
            {

                Shell.Current.GoToAsync($"//{nameof(MainPage)}");

            }
        }
    }
}