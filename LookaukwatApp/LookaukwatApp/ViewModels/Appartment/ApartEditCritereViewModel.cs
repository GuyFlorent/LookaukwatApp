using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.StaticList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Appartment
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ApartEditCritereViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        public IList<string> TypeList { get; }
        public IList<string> FurnitureOrNotList { get; }
        public IList<string> SearchOrSaskList { get; }

        private string itemId;
        private int apartSurface;
        private string type;
        private int roomNumber;
        private string furnitureOrNot;
        private int price;
        private string searchOrAsk;

        public string SearchOrAsk
        {
            get => searchOrAsk;
            set => SetProperty(ref searchOrAsk, value);
        }

        public int Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public int ApartSurface
        {
            get => apartSurface;
            set => SetProperty(ref apartSurface, value);
        }
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
        public int RoomNumber
        {
            get => roomNumber;
            set => SetProperty(ref roomNumber, value);
        }
        public string FurnitureOrNot
        {
            get => furnitureOrNot;
            set => SetProperty(ref furnitureOrNot, value);
        }

        public Command EditCommand { get; set; }
        public ApartEditCritereViewModel()
        {
            TitlePage = "Modifier les critères";
            TypeList = StaticListViewModel.GetListApartType;
            FurnitureOrNotList = StaticListViewModel.GetListFurnitureOrNot;
            SearchOrSaskList = StaticListViewModel.OfferOSearchList;
            EditCommand = new Command(OnEdit);
            this.PropertyChanged +=
               (_, __) => EditCommand.ChangeCanExecute();
        }


        public async void OnEdit()
        {
            IsBusy = true;
            var accessToken = Settings.AccessToken;
            await _apiServices.EditApartCritereAsync(ItemId, Price, SearchOrAsk, Type, RoomNumber,FurnitureOrNot,ApartSurface, accessToken);
            IsBusy = false;
            await Shell.Current.DisplayAlert("Information", "Modifiée avec succès", "Ok");
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadItemId(string itemId)
        {
            IsRunning = true;
            try
            {
                var id = Convert.ToInt32(itemId);
                var item = await _apiServices.GetUniqueApartCritereAsync(id);
                SearchOrAsk = item.SearchOrAsk;
                Price = item.Price;
                //for Apart model
                FurnitureOrNot = item.FurnitureOrNot;
                ApartSurface = item.ApartSurface;
                Type = item.Type;
                RoomNumber = item.RoomNumber;

                IsRunning = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

        }
    }
}
