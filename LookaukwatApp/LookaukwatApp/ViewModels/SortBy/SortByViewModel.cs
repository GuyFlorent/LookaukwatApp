using LookaukwatApp.ViewModels.Home;
using LookaukwatApp.Views;
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

        public SortByViewModel()
        {
            MostRecentCommand = new Command(OnMostRecentPage);
            MostOldCommand = new Command(OnMostOldPage);
            LowerPriceCommand = new Command(OnLowerPricePage);
            HeigherPriceCommand = new Command(OnHeigherPricetPage);
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
    }
}
