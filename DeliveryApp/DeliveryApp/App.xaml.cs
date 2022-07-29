using DeliveryApp.Helpers;
using DeliveryApp.Models;
using DeliveryApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryApp
{
    public partial class App : Application
    {
       
        public App()
        {
            Device.SetFlags(new string[] { "MediaElement_Experimental", "Brush_Experimental" });
            InitializeComponent();

            //  MainPage = new LoginView();
            //MainPage = new NavigationPage(new SettingsPage());


           string uname = Preferences.Get("Username", string.Empty);
            if (String.IsNullOrEmpty(uname))
            {
                MainPage = new LoginView();
            }
            else 
            {

                MainPage = new NavigationPage(new ProductsView());

            }
        }

        protected override void OnStart()
        {
            CreateTable();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        public bool CreateTable()
        {
            try
            {
                var cn = DependencyService.Get<ISQLite>().GetConnection();
                cn.CreateTable<CartItem>();
                cn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
