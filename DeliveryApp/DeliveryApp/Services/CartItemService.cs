using DeliveryApp.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace DeliveryApp.Services
{
    public class CartItemService
    {
        public int  GetUserCartCount()
        {
            try
            {
                var cn = DependencyService.Get<ISQLite>().GetConnection();
                var count = cn.Table<CartItem>().Count();
                cn.Close();
                return count;
            }
            catch ( Exception x)
            {

                Debug.WriteLine(x.Message);
            }
            return 0;
        }

        public void RemoveItemsFromCart()
        {
            try
            {
                var cn = DependencyService.Get<ISQLite>().GetConnection();
                cn.DeleteAll<CartItem>();
                cn.Commit();
                cn.Close();
            }
            catch (Exception x)
            {

                Debug.WriteLine(x.Message);
            }
        }
    }
}
