using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavorisPage : ContentPage
    {
        public FavorisPage()
        {
            InitializeComponent();
          
        }


        protected  override void OnAppearing()
        {
            base.OnAppearing();

            RefreshList();

        }
            private void RefreshList()
        {
            var Liste = Settings.JsonFavoriteList;

            if (!string.IsNullOrWhiteSpace(Liste))
            {
                List<ProductForMobileViewModel> ListFavorites = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(Liste);
                if (ListFavorites.Count > 0)
                {
                    Stack_HasResult.IsVisible = true;
                    Stack_Has_NoResult.IsVisible = false;
                    mylist.ItemsSource = ListFavorites;
                    
                    
                }
                else
                {
                    Stack_Has_NoResult.IsVisible = true;
                    Stack_HasResult.IsVisible = false;
                    mylist.ItemsSource = "";
                }
            }
        }
    }
}