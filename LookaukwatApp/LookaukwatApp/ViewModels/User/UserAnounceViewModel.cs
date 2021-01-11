using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.User
{
    public class UserAnounceViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();
        public Command LoadItemsCommand { get; }
        public Command DeleteCommand { get; }
        // public Command AddItemCommand { get; }
        public Command<ProductForMobileViewModel> ItemTapped { get; }
        public ObservableCollection<ProductForMobileViewModel> Items { get; set; }
        public UserAnounceViewModel()
        {
            TitlePage = "Mes annonces";
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            //ItemTapped = new Command<ProductForMobileViewModel>(OnItemSelected);
            Items = new ObservableCollection<ProductForMobileViewModel>();
            DeleteCommand = new Command(async (e) =>
            {
                var itm = e as ProductForMobileViewModel;
                var response = await App.Current.MainPage.DisplayAlert("Notification", "Voulez vous vraiment suprimer cette annonce ?", "Oui", "Non");

                if (response)
                {
                    var AccessToken = Settings.AccessToken;

                    bool resp = await _apiServices.DeleteProduct(AccessToken, itm.id);
                    if (resp)
                        Items.Remove(itm);
                }
            });
            DownloadDataAsync();
        }

        private async Task DownloadDataAsync()
        {
            string accessToken = Settings.AccessToken;
            IsBusy = true;
            var items = await _apiServices.GetUserProductsAsync(accessToken);

            foreach (var prod in items)
            {
                Items.Add(prod);
            }
            IsBusy = false;
        }


    }
}
