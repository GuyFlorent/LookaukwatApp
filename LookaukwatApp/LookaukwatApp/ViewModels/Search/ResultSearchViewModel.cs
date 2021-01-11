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
using LookaukwatApp.Views.Vehicule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace LookaukwatApp.ViewModels.Search
{
    [QueryProperty(nameof(JsonSearchModel), nameof(JsonSearchModel))]
    public class ResultSearchViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        private int numberOfProduct { get; set; }
        private ProductForMobileViewModel _selectedItem;
        public Command LoadItemsCommand { get; }
        public Command FilterCommand { get; }
        public Command<ProductForMobileViewModel> ItemTapped { get; }

        private string jsonSearchModel;

        public string JsonSearchModel
        {
            get => jsonSearchModel;
            set => SetProperty(ref jsonSearchModel, Uri.UnescapeDataString(value));
        }

        private const int PageSize = 10;

        public InfiniteScrollCollection<ProductForMobileViewModel> Items { get; }

        public ResultSearchViewModel()
        {
            //numberOfProduct = _apiServices.Get_AllNumber_ProductsAsync().Result;
            FilterCommand = new Command(OnFilter);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<ProductForMobileViewModel>(OnItemSelected);

            Items = new InfiniteScrollCollection<ProductForMobileViewModel>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Items.Count / PageSize;

                    var items = await _apiServices.GetProductsAsync(page, PageSize);
                    //numberOfProduct = await _apiServices.Get_AllNumber_ProductsAsync();
                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {

                    return Items.Count < 500;
                }
            };

            ShowResult();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsRefressing = true;

            try
            {
                Items.Clear();

                //var items = await _apiServices.GetProductsAsync(pageIndex: 0, pageSize: PageSize);
                //Items.AddRange(items);

                ShowResult();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsRefressing = false;
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
            await Shell.Current.GoToAsync($"{nameof(SearchPage)}?{nameof(SearchViewModel.Categori)}={JsonSearchModel}");
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


        private async void ShowResult()
        {
            SearchModel UserSearchCondition = JsonConvert.DeserializeObject<SearchModel>(JsonSearchModel);

            switch (UserSearchCondition.SearchOrAskJob)
            {
                case "J'offre":

                    switch (UserSearchCondition.Category)
                    {

                        case "Emploi":

                            List<ProductForMobileViewModel> resultJob = await _apiServices.GetResultOfferSeachJobAsync(UserSearchCondition, pageIndex: 0, pageSize: PageSize);
                            Items.AddRange(resultJob);

                            break;
                        case "Immobilier":

                            List<ProductForMobileViewModel> resultImo = await _apiServices.GetResultOfferSeachApartAsync(UserSearchCondition, pageIndex: 0, pageSize: PageSize);
                            Items.AddRange(resultImo);
                            break;
                        case "Multimédia":

                            List<ProductForMobileViewModel> resultMulti = await _apiServices.GetResultOfferSeachMultiAsync(UserSearchCondition, pageIndex: 0, pageSize: PageSize);
                            Items.AddRange(resultMulti);
                            break;
                        case "Maison":

                            List<ProductForMobileViewModel> resultHouse = await _apiServices.GetResultOfferSeachHouseAsync(UserSearchCondition, pageIndex: 0, pageSize: PageSize);
                            Items.AddRange(resultHouse);
                            break;
                        case "Mode":

                            List<ProductForMobileViewModel> resultMode = await _apiServices.GetResultOfferSeachModeAsync(UserSearchCondition, pageIndex: 0, pageSize: PageSize);
                            Items.AddRange(resultMode);
                            break;
                        case "Véhicule":

                            List<ProductForMobileViewModel> resultVehicule = await _apiServices.GetResultOfferSearchVehiculeAsync(UserSearchCondition, pageIndex: 0, pageSize: PageSize);
                            Items.AddRange(resultVehicule);
                            break;

                        default:
                            List<ProductForMobileViewModel> resultOffer = await _apiServices.GetResultAskAndOfferSeachAsync(UserSearchCondition, pageIndex: 0, pageSize: PageSize);
                            Items.AddRange(resultOffer);
                            break;
                    }

                    break;

                case "Je recherche":

                    List<ProductForMobileViewModel> resultAsk = await _apiServices.GetResultAskAndOfferSearchAsync(UserSearchCondition, pageIndex: 0, pageSize: PageSize);
                    Items.AddRange(resultAsk);
                    break;
            }
        }
    }
}
