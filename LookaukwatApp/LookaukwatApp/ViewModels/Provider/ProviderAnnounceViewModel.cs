using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using LookaukwatApp.ViewModels.Appartment;
using LookaukwatApp.ViewModels.Edit;
using LookaukwatApp.ViewModels.House;
using LookaukwatApp.ViewModels.Job;
using LookaukwatApp.ViewModels.Mode;
using LookaukwatApp.ViewModels.Multimedia;
using LookaukwatApp.ViewModels.Vehicule;
using LookaukwatApp.Views.AppartmentView;
using LookaukwatApp.Views.EditView;
using LookaukwatApp.Views.HouseView;
using LookaukwatApp.Views.JobView;
using LookaukwatApp.Views.ModeView;
using LookaukwatApp.Views.MultimediaView;
using LookaukwatApp.Views.Vehicule;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Provider
{
    public class ProviderAnnounceViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();
        public ObservableCollection<ProductForMobileViewModel> Items { get; set; }
        public Command<ProductForMobileViewModel> ItemTapped { get; }
        bool isEmpty = false;
        public bool IsEmpty
        {
            get { return isEmpty; }
            set { SetProperty(ref isEmpty, value); }
        }

        bool isNotEmty = false;
        public bool IsNotEmty
        {
            get { return isNotEmty; }
            set { SetProperty(ref isNotEmty, value); }
        }

        public ProviderAnnounceViewModel()
        {
            Items = new ObservableCollection<ProductForMobileViewModel>();
            ItemTapped = new Command<ProductForMobileViewModel>(OnItemSelected);
            DownloadDataAsync();
        }


        private async Task DownloadDataAsync()
        {
            string accessToken = Settings.AccessToken;
            IsBusy = true;

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsBusy = false;
                return;
            }

            try
            {
                var items = await _apiServices.GetProviderProductsAsync(accessToken);

                if (items.Count == 0)
                {
                    IsEmpty = true;
                }
                else
                {
                    IsNotEmty = true;
                    foreach (var prod in items)
                    {
                        Items.Add(prod);
                    }
                }


                IsBusy = false;
            }
            catch (Exception e) 
            { 
                Console.WriteLine(e.Message); }

        }

        async void OnItemSelected(ProductForMobileViewModel item)
        {

            if (item == null)
                return;

            switch (item.Category)
            {
                case "Emploi":

                    // This will push the ItemDetailPage onto the navigation stack
                    await Shell.Current.GoToAsync($"{nameof(JobDetailPage)}?{nameof(JobDetailsViewModel.ItemId)}={item.id}");

                    break;

                case "Immobilier":

                    await Shell.Current.GoToAsync($"{nameof(ApartDetailPage)}?{nameof(ApartDetailViewModel.ItemId)}={item.id}");
                    break;

                case "Mode":

                    await Shell.Current.GoToAsync($"{nameof(ModeDetailPage)}?{nameof(ModeDetailViewModel.ItemId)}={item.id}");

                    break;

                case "Multimedia":
                    await Shell.Current.GoToAsync($"{nameof(MultimediaDetailPage)}?{nameof(MultimediaDetailViewModel.ItemId)}={item.id}");
                    break;

                case "Vehicule":
                    await Shell.Current.GoToAsync($"{nameof(VehiculeDetailPage)}?{nameof(VehiculeDetailViewModel.ItemId)}={item.id}");
                    break;

                case "Maison":
                    await Shell.Current.GoToAsync($"{nameof(HouseDetailPage)}?{nameof(HouseDetailViewModel.ItemId)}={item.id}");
                    break;
            }

        }
    }
}
