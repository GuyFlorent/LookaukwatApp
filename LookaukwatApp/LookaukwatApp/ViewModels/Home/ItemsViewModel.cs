using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Appartment;
using LookaukwatApp.ViewModels.House;
using LookaukwatApp.ViewModels.Job;
using LookaukwatApp.ViewModels.Mode;
using LookaukwatApp.ViewModels.Multimedia;
using LookaukwatApp.ViewModels.Vehicule;
using LookaukwatApp.Views;
using LookaukwatApp.Views.AppartmentView;
using LookaukwatApp.Views.HouseView;
using LookaukwatApp.Views.JobView;
using LookaukwatApp.Views.ModeView;
using LookaukwatApp.Views.MultimediaView;
using LookaukwatApp.Views.SearchView;
using LookaukwatApp.Views.SortView;
using LookaukwatApp.Views.Vehicule;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace LookaukwatApp.ViewModels.Home
{
    [QueryProperty(nameof(SortBy), nameof(SortBy))]
    [QueryProperty(nameof(IsSort), nameof(IsSort))]
    public class ItemsViewModel : BaseViewModel
    {
        private ProductForMobileViewModel _selectedItem;

        private int numberOfProduct { get; set; }
        ApiServices _apiServices = new ApiServices();

        public Command LoadItemsCommand { get; }
        public Command FilterCommand { get; }
        public Command SortPageCommand { get; }
       
        
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
                    DownloadDataAsync(SortBy);
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

            try
            {
                Items.Clear();
                SortBy = Settings.SortItemPage;
                var items = await _apiServices.GetProductsAsync(pageIndex: 0, pageSize: PageSize, SortBy);
                Items.AddRange(items);

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

      
        //public void OnAppearing()
        //{
        //    IsImageView = true;
        //    SelectedItem = null;
        //}

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
            }

        }



        private const int PageSize = 30;

        InfiniteScrollCollection<ProductForMobileViewModel> items;
        public InfiniteScrollCollection<ProductForMobileViewModel> Items { get => items; set => SetProperty(ref items, value); }
       // public InfiniteScrollCollection<ProductForMobileViewModel> Items { get; set; }
 
        public ItemsViewModel()
        {
            TitlePage = "Lookaukwat";
            GetTotalNumberOfProduct();
           
            FilterCommand = new Command(OnFilter);
            SortPageCommand = new Command(OnSortPage);
           
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

                    return Items.Count < numberOfProduct;
                }
            };
            Settings.SortItemPage = "";
            DownloadDataAsync(SortBy);
        }

        //public async void OnApearing( string sortBy, bool isSort)
        //{
        //    if (IsSort)
        //    {
               
        //        await DownloadDataAsync(SortBy);
        //    }
        //    IsSort = false;
        //}

        private async Task DownloadDataAsync(string sortBy)
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
            numberOfProduct = await _apiServices.Get_AllNumber_ProductsAsync();
        }

    }
}
