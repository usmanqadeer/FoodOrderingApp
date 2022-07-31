using DeliveryApp.Helpers;
using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using Application = Xamarin.Forms.Application;
using System.Windows.Input;

namespace DeliveryApp.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private string _Username;
        public string Username
        {
            get { return _Username; }
            set { _Username = value;
                OnPropertyChanged();
            }
        }
        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                _SearchText = value;
                OnPropertyChanged();
            }
        }

        private int _UserCartItemsCount;
        public int UserCartItemsCount
        {
            get { return _UserCartItemsCount; }
            set { _UserCartItemsCount = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<ProductItem> LatestItems { get; set; }

        public Command ViewCartCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public Command OrdersHistoryCommand { get; set; }
        public Command SearchViewCommand { get; set; }
        public ICommand SearchBarCommand { get; set; }
        public Command CloseViewCommand { get; set; }

        private string searchItem;

        public string SearchedText
        {
            get { return searchItem; }
            set { searchItem = value; OnPropertyChanged("SearchProductName"); }
        }
        private bool isLoding = false;

        public bool IsLoding
        {
            get { return isLoding; }
            set { isLoding = value; OnPropertyChanged("IsLoding"); }
        }

        public ProductsViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            var uname = Preferences.Get("Username", String.Empty);
            if (string.IsNullOrEmpty(uname))
            {
                Username = "Invité";
            }
            else { Username = uname; }

            UserCartItemsCount = new CartItemService().GetUserCartCount();
            Categories = new ObservableCollection<Category>();
            LatestItems= new ObservableCollection<ProductItem>();

            GetCategoriesAsync();
            GetLatestItems();

            ViewCartCommand = new Command(async () => await ViewCartAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());
            OrdersHistoryCommand = new Command(async () => await OrdersHistoryAsync());
            SearchViewCommand = new Command(async () => await SearchViewAsync());
            SearchBarCommand = new Command(async () => await Search());
        }

        private async Task SearchViewAsync()
        {
            if (SearchText == null)
            { 
                await Application.Current.MainPage.DisplayAlert("Error", "Enter the item name for search.", "ok");
            }
            else 
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SearchResultView(SearchText)); 
            }
           
        }
        private async Task Search()
        {
            if (!string.IsNullOrEmpty(SearchedText))
            {
                var filteredProducts = LatestItems
                            .Where(x =>
                                x.Name.ToLower().Contains(SearchedText.ToLower()))
                            .ToList();

                LatestItems.Clear();

                foreach (var productItem in filteredProducts)
                    LatestItems.Add(productItem);
            }
            else
            {
                
                LatestItems.Clear();
                GetLatestItems();
            }
        }
        private async Task OrdersHistoryAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersHistoryView());
        }

        private async Task LogoutAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView());
        }

        private async Task ViewCartAsync()
        {
          await  Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private async void GetLatestItems()
        {
            IsLoding = true;
            var data = await new FoodItemService().GetFoodItemsAsync();
            LatestItems.Clear();
            foreach(var item in data)
            {
                LatestItems.Add(item);
            }
            IsLoding = false;
        }

        private async void GetCategoriesAsync()
        {
            IsLoding = true;
            var data = await new CategoryDataService().GetCategoriesAsync();
            Categories.Clear();
            foreach( var item in data)
            {
                Categories.Add(item);
            }
            IsLoding = false;
        }
    }
}
