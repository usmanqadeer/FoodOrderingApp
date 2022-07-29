using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            AnimateCarouselView();
        }
        Timer timer;
        /// <summary>
        /// Set animaion CarouselView using Timer 
        /// </summary>
        private void AnimateCarouselView()
        {
            timer = new Timer(5000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (bgVideo.CurrentState != MediaElementState.Playing)
                    bgVideo.Play();
            };

        }
    }
}