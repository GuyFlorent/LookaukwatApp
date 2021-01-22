using LookaukwatApp.Helpers;
using LookaukwatApp.Models;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Appartment;
using LookaukwatApp.ViewModels.House;
using LookaukwatApp.ViewModels.Job;
using LookaukwatApp.ViewModels.Mode;
using LookaukwatApp.ViewModels.Multimedia;
using LookaukwatApp.ViewModels.Vehicule;
using LookaukwatApp.Views.AppartmentView;
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
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace LookaukwatApp.ViewModels.Search
{
    [QueryProperty(nameof(SortBy), nameof(SortBy))]
    [QueryProperty(nameof(IsSort), nameof(IsSort))]
    [QueryProperty(nameof(JsonSearchModel), nameof(JsonSearchModel))]
    [QueryProperty(nameof(NumberOfresult), nameof(NumberOfresult))]
    public class ResultSearchViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        private int numberOfProduct { get; set; }
        private ProductForMobileViewModel _selectedItem;
        public Command LoadItemsCommand { get; }
        public Command FilterCommand { get; }
        public Command BackCommand { get; }
        public Command SortPageCommand { get; }
       
        public Command<ProductForMobileViewModel> ItemTapped { get; }

        private string jsonSearchModel;

        public string JsonSearchModel
        {
            //get => jsonSearchModel;
            //set => SetProperty(ref jsonSearchModel, Uri.UnescapeDataString(value));
            get
            {
                return jsonSearchModel;
            }
            set
            {
                SetProperty(ref jsonSearchModel, value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Settings.JsonSearchSave = value;
                    int pageIndex = 0;
                    var item = ShowResult(pageIndex, SortBy);
                }
                
               
            }
        }

        private const int PageSize = 30;

        private string numberOfresult;
        public string NumberOfresult
        {
            get { return numberOfresult; }
            set 
            {
                SetProperty(ref numberOfresult, value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Settings.NumberOfSearchReasult = value;
                }
            }
        }

        private string sortBy = "MostRecent";

        bool isSort = false;
        public bool IsSort
        {
            get { return isSort; }
            set
            {

                SetProperty(ref isSort, value);
                if (value == true)
                {
                    Settings.SortIResultSearch = SortBy;
                    if (string.IsNullOrWhiteSpace(JsonSearchModel))
                    {
                        JsonSearchModel = Settings.JsonSearchSave;
                    }
                    else
                    {
                        int pageIndex = 0;
                        var items = ShowResult(pageIndex, SortBy);
                    }
                    
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

        InfiniteScrollCollection<ProductForMobileViewModel> items;
        public InfiniteScrollCollection<ProductForMobileViewModel> Items { get => items; set => SetProperty(ref items, value); }
       // public InfiniteScrollCollection<ProductForMobileViewModel> Items { get; }

        public ResultSearchViewModel()
        {
            //numberOfProduct = _apiServices.Get_AllNumber_ProductsAsync().Result;
            FilterCommand = new Command(OnFilter);
            BackCommand = new Command(OnBack);
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
                    SortBy = Settings.SortIResultSearch;
                    var items = new List<ProductForMobileViewModel>();
                    if (!string.IsNullOrWhiteSpace(JsonSearchModel))
                    {
                         items = await ShowResult(page, SortBy);
                    }
                    else
                    {
                        var json = Settings.JsonSearchSave;
                        items = await ShowResultInfiniteDScroll(json,page, SortBy);
                    }
                    
                    // var items = await _apiServices.GetProductsAsync(page, PageSize);
                    //numberOfProduct = await _apiServices.Get_AllNumber_ProductsAsync();
                    IsBusy = false;
                  
                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    if (string.IsNullOrWhiteSpace(NumberOfresult))
                    {
                        NumberOfresult = Settings.NumberOfSearchReasult;
                    }

                    return Items.Count < Convert.ToInt32(NumberOfresult);
                }
            };

            Settings.SortIResultSearch = "";
        }

        private async void OnSortPage()
        {
            SortBy = Settings.SortIResultSearch;
            await PopupNavigation.Instance.PushAsync(new SortItemsPage(SortBy, "searchPage"));
            // await Shell.Current.GoToAsync(nameof(SortItemsPage));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsRefressing = true;

            try
            {
                Items.Clear();

               
                SortBy = Settings.SortIResultSearch;
                if (string.IsNullOrWhiteSpace(JsonSearchModel))
                {
                    JsonSearchModel = Settings.JsonSearchSave;
                }
                else
                {
                    int pageIndex = 0;
                    var items = ShowResult(pageIndex, SortBy);
                }
              

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
           // await Shell.Current.GoToAsync($"{nameof(SearchPage)}?{nameof(SearchViewModel.JsonOldSearch)}={JsonSearchModel}");
            await Shell.Current.GoToAsync("..");
        }

        private async void OnBack()
        {
            
            await Shell.Current.GoToAsync("///MainPage");
        }

        async void OnItemSelected(ProductForMobileViewModel item)
        {

            if (item == null)
                return;
            Settings.JsonSearchSave = JsonSearchModel;
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


        private async Task<List<ProductForMobileViewModel>> ShowResult(int pageIndex, string sortBy)
        {
            IsRunning = true;
            string data = HttpUtility.UrlDecode(JsonSearchModel);
            SearchModel UserSearchCondition = JsonConvert.DeserializeObject<SearchModel>(data);
            List<ProductForMobileViewModel> list = new List<ProductForMobileViewModel>();
            switch (UserSearchCondition.SearchOrAskJob)
            {
                case "J'offre":

                    switch (UserSearchCondition.Category)
                    {

                        case "Emploi":

                            var resultJob = await _apiServices.GetResultOfferSeachJobAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultJob);
                            list = resultJob;
                            break;
                        case "Immobilier":

                            var resultImo = await _apiServices.GetResultOfferSeachApartAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultImo);
                            list = resultImo;
                            break;
                        case "Multimedia":

                            var resultMulti = await _apiServices.GetResultOfferSeachMultiAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultMulti);
                            list = resultMulti;
                            break;
                        case "Maison":

                            var resultHouse = await _apiServices.GetResultOfferSeachHouseAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultHouse);
                            list = resultHouse;
                            break;
                        case "Mode":
                            
                            var resultMode = await _apiServices.GetResultOfferSeachModeAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultMode);
                            list = resultMode;
                            break;
                        case "Vehicule":

                            var resultVehicule = await _apiServices.GetResultOfferSearchVehiculeAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultVehicule);
                            list = resultVehicule;
                            break;

                        default:
                            var resultOffer = await _apiServices.GetResultAskAndOfferSearchAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultOffer);
                            list = resultOffer;
                            break;
                    }

                    break;

                case "Je recherche":

                    var resultAsk = await _apiServices.GetResultAskAndOfferSearchAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                    Items.AddRange(resultAsk);
                    list = resultAsk;
                    break;
            }
            IsRunning = false;
            return list;
        }


        private async Task<List<ProductForMobileViewModel>> ShowResultInfiniteDScroll(string json,int pageIndex, string sortBy)
        {
            IsRunning = true;
            string data = HttpUtility.UrlEncode(JsonSearchModel);
            SearchModel UserSearchCondition = JsonConvert.DeserializeObject<SearchModel>(data);
            List<ProductForMobileViewModel> list = new List<ProductForMobileViewModel>();
            switch (UserSearchCondition.SearchOrAskJob)
            {
                case "J'offre":

                    switch (UserSearchCondition.Category)
                    {

                        case "Emploi":

                            var resultJob = await _apiServices.GetResultOfferSeachJobAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultJob);
                            list = resultJob;
                            break;
                        case "Immobilier":

                            var resultImo = await _apiServices.GetResultOfferSeachApartAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultImo);
                            list = resultImo;
                            break;
                        case "Multimedia":

                            var resultMulti = await _apiServices.GetResultOfferSeachMultiAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultMulti);
                            list = resultMulti;
                            break;
                        case "Maison":

                            var resultHouse = await _apiServices.GetResultOfferSeachHouseAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultHouse);
                            list = resultHouse;
                            break;
                        case "Mode":

                            var resultMode = await _apiServices.GetResultOfferSeachModeAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultMode);
                            list = resultMode;
                            break;
                        case "Vehicule":

                            var resultVehicule = await _apiServices.GetResultOfferSearchVehiculeAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultVehicule);
                            list = resultVehicule;
                            break;

                        default:
                            var resultOffer = await _apiServices.GetResultAskAndOfferSearchAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                            Items.AddRange(resultOffer);
                            list = resultOffer;
                            break;
                    }

                    break;

                case "Je recherche":

                    var resultAsk = await _apiServices.GetResultAskAndOfferSearchAsync(UserSearchCondition, pageIndex: pageIndex, pageSize: PageSize, sortBy);
                    Items.AddRange(resultAsk);
                    list = resultAsk;
                    break;
            }
            IsRunning = false;
            return list;
        }
    }
}
