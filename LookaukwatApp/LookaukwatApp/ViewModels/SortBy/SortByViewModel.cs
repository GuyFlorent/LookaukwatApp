using LookaukwatApp.ViewModels.Home;
using LookaukwatApp.ViewModels.Search;
using LookaukwatApp.Views;
using LookaukwatApp.Views.SearchView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.SortBy
{
   public class SortByViewModel : BaseViewModel
    {
        private string sortBy = "MostRecent";
        public string SortBy
        {
            get => sortBy;
            set => SetProperty(ref sortBy, value);
        }

        private bool isSort = false;
        public bool IsSort
        {
            get { return isSort; }
            set { isSort = value; }
        }
        public Command MostRecentCommand { get; }
        public Command MostOldCommand { get; }
        public Command LowerPriceCommand { get; }
        public Command HeigherPriceCommand { get; }

        //For searchPage

        public Command MostRecentSearchCommand { get; }
        public Command MostOldSearchCommand { get; }
        public Command LowerPriceSearchCommand { get; }
        public Command HeigherPriceSearchCommand { get; }

        //constructor
        public SortByViewModel()
        {
            MostRecentCommand = new Command(OnMostRecentPage);
            MostOldCommand = new Command(OnMostOldPage);
            LowerPriceCommand = new Command(OnLowerPricePage);
            HeigherPriceCommand = new Command(OnHeigherPricetPage);

            // for search

            MostRecentSearchCommand = new Command(OnMostRecentSearchPage);
            MostOldSearchCommand = new Command(OnMostOldSearchPage);
            LowerPriceSearchCommand = new Command(OnLowerPriceSearchPage);
            HeigherPriceSearchCommand = new Command(OnHeigherPriceSearchPage);
        }

        

        private async void OnMostRecentPage()
        {
            SortBy = "MostRecent";
            IsSort = true;
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync($"//MainPage/ItemsPage?{nameof(ItemsViewModel.SortBy)}={SortBy}&{nameof(ItemsViewModel.IsSort)}={IsSort}");
        }

        private async void OnMostOldPage()
        {
            SortBy = "MostOld";
            IsSort = true;
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync($"//MainPage/ItemsPage?{nameof(ItemsViewModel.SortBy)}={SortBy}&{nameof(ItemsViewModel.IsSort)}={IsSort}");



        }

        private async void OnLowerPricePage()
        {
            SortBy = "LowerPrice";
            IsSort = true;
            //  await DownloadDataAsync(SortBy);
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync($"//MainPage/ItemsPage?{nameof(ItemsViewModel.SortBy)}={SortBy}&{nameof(ItemsViewModel.IsSort)}={IsSort}");
            
        }

        private async void OnHeigherPricetPage()
        {
            SortBy = "HeigherPrice";
            IsSort = true;
            // await DownloadDataAsync(SortBy);
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync($"//MainPage/ItemsPage?{nameof(ItemsViewModel.SortBy)}={SortBy}&{nameof(ItemsViewModel.IsSort)}={IsSort}");

        }


        /////////For searchPage

        private async void OnMostRecentSearchPage()
        {
            SortBy = "MostRecent";
            IsSort = true;
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync($"{nameof(ResultSearchPage)}?{nameof(ResultSearchViewModel.SortBy)}={SortBy}&{nameof(ResultSearchViewModel.IsSort)}={IsSort}");
        }

        private async void OnMostOldSearchPage()
        {
            SortBy = "MostOld";
            IsSort = true;
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync($"{nameof(ResultSearchPage)}?{nameof(ResultSearchViewModel.SortBy)}={SortBy}&{nameof(ResultSearchViewModel.IsSort)}={IsSort}");



        }

        private async void OnLowerPriceSearchPage()
        {
            SortBy = "LowerPrice";
            IsSort = true;
            //  await DownloadDataAsync(SortBy);
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync($"{nameof(ResultSearchPage)}?{nameof(ResultSearchViewModel.SortBy)}={SortBy}&{nameof(ResultSearchViewModel.IsSort)}={IsSort}");

        }

        private async void OnHeigherPriceSearchPage()
        {
            SortBy = "HeigherPrice";
            IsSort = true;
            // await DownloadDataAsync(SortBy);
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync($"{nameof(ResultSearchPage)}?{nameof(ResultSearchViewModel.SortBy)}={SortBy}&{nameof(ResultSearchViewModel.IsSort)}={IsSort}");

        }
    }
}
