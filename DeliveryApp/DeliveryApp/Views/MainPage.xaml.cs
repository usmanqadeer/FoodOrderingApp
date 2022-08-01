using DeliveryApp.Models;
using DeliveryApp.ViewModels;
using System;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeliveryApp.Views
{
    public partial class MainPage : ContentPage 
    {
        
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new ProductsViewModel(Navigation);
        }
        /// <summary>
        /// St time for shocase products animain it repete aftr 5(s)
        /// </summary>
        private Timer timer;
        private int TotalBanners;
        protected override void OnAppearing()
        {
            timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += Timer_Elapsed;
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            timer?.Dispose();
            base.OnDisappearing();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                if (cvBanners.Position > 2)
                {
                    cvBanners.Position = 0;
                    return;
                }

                cvBanners.Position += 1;
            });
        }
       /// <summary>
       /// This event handle the category animain bacgron color and update products by category id.
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private  void SelectType(object sender, EventArgs e)
        {
            var view = sender as View;
            var parent = view.Parent as StackLayout;

            foreach (var child in parent.Children)
            {
                VisualStateManager.GoToState(child, "Normal");
                ChangeTextColor(child, "#707070");
            }

            VisualStateManager.GoToState(view, "Selected");
            ChangeTextColor(view, "#FFFFFF");

            var category = (sender as View).BindingContext as Category;
            App.Current.MainPage.Navigation.PushModalAsync(new CategoryView(category));
        }
        /// <summary>
        /// This function change the color of text lable category name duering the frame bacgroun color changing.
        /// </summary>
        /// <param name="child"></param>
        /// <param name="hexColor"></param>
        private void ChangeTextColor(View child, string hexColor)
        {
            var txtCtrl = child.FindByName<Label>("PropertyTypeName");

            if (txtCtrl != null)
                txtCtrl.TextColor = Color.FromHex(hexColor);
        }
        /// <summary>
        /// this gusture used to pass vale product  datail page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(AutenticationPage)}");
            Preferences.Remove("Username");
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var product = (sender as View).BindingContext as ProductItem;
            await this.Navigation.PushModalAsync(new ProductDetailsView(product));
        }
    }

}


