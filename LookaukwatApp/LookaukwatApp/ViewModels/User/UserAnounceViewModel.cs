using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
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
        public Command EditCommand { get; }
        public Command PublishAnnounceCommand { get; }
        public Command<ProductForMobileViewModel> ItemTapped { get; }
        public ObservableCollection<ProductForMobileViewModel> Items { get; set; }
        
        bool isItems = false;
        public bool IsItems
        {
            get { return isItems; }
            set { SetProperty(ref isItems, value); }
        }
        
        bool isListItems = false;
        public bool IsListItems
        {
            get { return isListItems; }
            set { SetProperty(ref isListItems, value); }
        }

        public UserAnounceViewModel()
        {
            TitlePage = "Mes annonces";
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<ProductForMobileViewModel>(OnItemSelected);
            Items = new ObservableCollection<ProductForMobileViewModel>();
            PublishAnnounceCommand = new Command(OnPublish);
            DeleteCommand = new Command(async (e) =>
            {
                var itm = e as ProductForMobileViewModel;
                var response = await Shell.Current.DisplayAlert("Notification", "Voulez vous vraiment suprimer cette annonce ?", "Oui", "Non");

                if (response)
                {
                    var AccessToken = Settings.AccessToken;
                    Items.Remove(itm);
                    bool resp = await _apiServices.DeleteProduct(AccessToken, itm.id);
                    //if (resp)
                        
                }
            });

            EditCommand = new Command(async (e) =>
            {
                var item = e as ProductForMobileViewModel;

                await Shell.Current.GoToAsync($"{nameof(EditPage)}?{nameof(EditViewModel.Id)}={item.id}&{nameof(EditViewModel.Category)}={item.Category}");


            });

            DownloadDataAsync();
        }

        public async void OnPublish()
        {
            await Shell.Current.GoToAsync("//MainPage/PublishAnnouncePage");
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

        private async Task DownloadDataAsync()
        {
            string accessToken = Settings.AccessToken;
            IsBusy = true;
            var items = await _apiServices.GetUserProductsAsync(accessToken);

            if(items.Count == 0)
            {
                IsListItems = false;
                IsItems = true;
            }
            else
            {
                IsListItems = true;
                foreach (var prod in items)
                {
                    Items.Add(prod);
                }
            }
            
           
            IsBusy = false;
        }


    }
}
