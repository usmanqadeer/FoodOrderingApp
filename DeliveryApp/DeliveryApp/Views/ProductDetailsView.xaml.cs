﻿using DeliveryApp.Models;
using DeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsView : ContentPage
    {
        
        public ProductDetailsView(ProductItem foodItem)
        {
            InitializeComponent();
            BindingContext = new ProductDetailsViewModel(foodItem);
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {

        }

        private void BayNowButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}