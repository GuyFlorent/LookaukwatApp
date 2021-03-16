using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Appartment;
using LookaukwatApp.ViewModels.Event;
using LookaukwatApp.ViewModels.House;
using LookaukwatApp.ViewModels.Job;
using LookaukwatApp.ViewModels.Mode;
using LookaukwatApp.ViewModels.Multimedia;
using LookaukwatApp.ViewModels.OtherServices;
using LookaukwatApp.ViewModels.Vehicule;
using LookaukwatApp.Views;
using LookaukwatApp.Views.AppartmentView;
using LookaukwatApp.Views.EventView;
using LookaukwatApp.Views.HouseView;
using LookaukwatApp.Views.JobView;
using LookaukwatApp.Views.ModeView;
using LookaukwatApp.Views.MultimediaView;
using LookaukwatApp.Views.SearchView;
using LookaukwatApp.Views.SortView;
using LookaukwatApp.Views.Vehicule;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace LookaukwatApp.ViewModels.Home
{
    [QueryProperty(nameof(SortBy), nameof(SortBy))]
    [QueryProperty(nameof(IsSort), nameof(IsSort))]
    public class ItemsViewModel : BaseViewModel
    {
        private ProductForMobileViewModel _selectedItem;

        private int numberOfProduct = 0;
        public int NumberOfProduct { get => numberOfProduct; set => numberOfProduct = value; }
        ApiServices _apiServices = new ApiServices();

        public Command LoadItemsCommand { get; }
        public Command FilterCommand { get; }
        public Command SortPageCommand { get; }
        public Command NotFavoriteCommand { get; }

       
        public Command<ProductForMobileViewModel> ItemTapped { get; }


        private string sortBy = "MostRecent";
        

        bool isSort = false;
        public bool IsSort
        {
            get { return isSort; }
            set
            {
                
                SetProperty(ref isSort, value);
                if(value == true)
                {
                    Settings.SortItemPage = SortBy;
                    DownloadDataSortAsync(SortBy);
                }
               
            }
        }

        public string SortBy
        {
            get => sortBy;
            set
            {

                SetProperty(ref sortBy, value);

                
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsRefressing = true;
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");
                IsRefressing = false;
                IsRunning = false;
                return;
            }
            try
            {
                Items.Clear();
                SortBy = Settings.SortItemPage;
                var items = await _apiServices.GetProductsAsync(pageIndex: 0, pageSize: PageSize, SortBy);
                Items.AddRange(items);
                Settings.Products = JsonConvert.SerializeObject(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsRefressing = false;
                IsRunning = false;
            }
        }


        public void OnAppearing()
        {
            Items.Clear();
            List<ProductForMobileViewModel> ListFavorites = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(Settings.Products);
            Items.AddRange(ListFavorites);
        }

        public ProductForMobileViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnFilter()
        {
             await Shell.Current.GoToAsync(nameof(SearchPage));
        }

        private async void OnSortPage()
        {
            SortBy = Settings.SortItemPage;
            await PopupNavigation.Instance.PushAsync(new SortItemsPage(SortBy, "itemsPage"));
           // await Shell.Current.GoToAsync(nameof(SortItemsPage));
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
                case "Événement":
                   
                    await Shell.Current.GoToAsync($"{nameof(EventDetailPage)}?{nameof(EventDetailViewModel.ItemId)}={item.id}");

                    break;
            }

        }



        private const int PageSize = 500;

        InfiniteScrollCollection<ProductForMobileViewModel> items;
        public InfiniteScrollCollection<ProductForMobileViewModel> Items { get => items; set => SetProperty(ref items, value); }
       // public InfiniteScrollCollection<ProductForMobileViewModel> Items { get; set; }
 
        public ItemsViewModel()
        
        {
            TitlePage = "Lookaukwat";
            GetTotalNumberOfProduct();
           
            FilterCommand = new Command(OnFilter);
            SortPageCommand = new Command(OnSortPage);
            
            NotFavoriteCommand = new Command<ProductForMobileViewModel>(OnFavorite);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<ProductForMobileViewModel>(OnItemSelected);
            Items = new InfiniteScrollCollection<ProductForMobileViewModel>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Items.Count / PageSize;
                    SortBy = Settings.SortItemPage;
                    var items = await _apiServices.GetProductsAsync(page, PageSize, SortBy);
                    //numberOfProduct = await _apiServices.Get_AllNumber_ProductsAsync();
                    IsBusy = false;



                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {

                    return Items.Count < NumberOfProduct;
                }
            };


           

            Settings.SortItemPage = "";
            //Settings.Products = "";

            
            if (string.IsNullOrWhiteSpace(Settings.Products))
            {
                DownloadDataAsync(SortBy);
            }
            else
            {
                List<ProductForMobileViewModel> ListFavorites = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(Settings.Products);
                Items.AddRange(ListFavorites);
            }
            
        }

       
        private async void OnFavorite(ProductForMobileViewModel item)
        {
            bool response = CheckFavorite.IsFabvorite(item);
            if (response)
            {
                item.BlackHeart = "heart_red";
                item.RedHeart = "heart_red";
                await Shell.Current.DisplayAlert("Ajoutée aux favoris !", "Vous pouvez contacter l'annonceur à tout moment dans vos favoris pour lui montrer votre intérêt", "ok");
            }
            else
            {
                item.BlackHeart = "heart_black";
                item.RedHeart = "heart_black";
            }
        }

        private async Task DownloadDataAsync(string sortBy)
        {
            
            IsRunning = true;
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
               await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");
                IsRunning = false;
                return;
            }
           
            try
            {
                Items.Clear();
                var items = await _apiServices.GetProductsAsync(pageIndex: 0, pageSize: PageSize, sortBy);
                
            Items.AddRange(items);
                Settings.Products = JsonConvert.SerializeObject(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
               
                IsRunning = false;
            }
           
        }

        private async Task DownloadDataSortAsync(string sortBy)
        {
            IsRunning = true;
            try
            {
                Items.Clear();
                var items = await _apiServices.GetProductsAsync(pageIndex: 0, pageSize: PageSize, sortBy);

                Items.AddRange(items);
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {

                IsRunning = false;
            }

        }
      
        private async void GetTotalNumberOfProduct()
        {
            try
            {
                NumberOfProduct = await _apiServices.Get_AllNumber_ProductsAsync();
            }catch(Exception e) { }
            
        }


        public void UpdateItems()
        {
        
            //if (!string.IsNullOrWhiteSpace(Settings.Products))
            //{
            //    Items.Clear();
            //    List<ProductForMobileViewModel> ListFavorites = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(Settings.Products);
            //    Items.AddRange(ListFavorites);
            //}
        }

    }
}
