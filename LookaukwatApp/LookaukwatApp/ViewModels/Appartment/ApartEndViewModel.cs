using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Image;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.ImageView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Appartment
{
    [QueryProperty(nameof(Price), nameof(Price))]
    [QueryProperty(nameof(Type), nameof(Type))]
    [QueryProperty(nameof(RoomNumber), nameof(RoomNumber))]
    [QueryProperty(nameof(ApartSurface), nameof(ApartSurface))]
    [QueryProperty(nameof(SearchOrAskJob), nameof(SearchOrAskJob))]
    [QueryProperty(nameof(FurnitureOrNot), nameof(FurnitureOrNot))]
    public class ApartEndViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        public IList<string> TownList { get; }
        private int id;
        public int Id
        {
            get => id;
            set => id = value;
        }
        private string titleApart;
        public string TitleApart
        {
            get => titleApart;
            set => SetProperty(ref titleApart, value);
        }
        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        private string town;
        public string Town
        {
            get => town;
            set => SetProperty(ref town, value);
        }
        private string street;
        public string Street
        {
            get => street;
            set => SetProperty(ref street, value);
        }
        private string price;
        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        private string searchOrAskJob;
        public string SearchOrAskJob
        {
            get => searchOrAskJob;
            set => SetProperty(ref searchOrAskJob, value);
        }
        private string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, Uri.UnescapeDataString(value));
        }
        private string furnitureOrNot;
        public string FurnitureOrNot
        {
            get => furnitureOrNot;
            set => SetProperty(ref furnitureOrNot, Uri.UnescapeDataString(value));
        }

        private string roomNumber;
        public string RoomNumber
        {
            get => roomNumber;
            set => SetProperty(ref roomNumber, value);
        }

        private string apartSurface;
        public string ApartSurface
        {
            get => apartSurface;
            set => SetProperty(ref apartSurface, value);
        }

        private bool ValidateLoging()
        {
            return !String.IsNullOrWhiteSpace(TitleApart)
                && !String.IsNullOrWhiteSpace(Description)
                && !String.IsNullOrWhiteSpace(Street);
        }

        public ApartEndViewModel()
        {
            PostApartCommad = new Command(OnPostApart, ValidateLoging);
            TitlePage = "Titre,description, ville, quartier...";
            TownList = StaticListViewModel.GetTownCameroonList;
            this.PropertyChanged +=
               (_, __) => PostApartCommad.ChangeCanExecute();
        }

        public Command PostApartCommad { get; }


        async void OnPostApart()
        {
            IsRunning = true;

            //var coor = await _apiServices.GetCoodinateAsync(Town, Street);
            int surface = Convert.ToInt32(ApartSurface);
            int room = Convert.ToInt32(RoomNumber);
            int price = Convert.ToInt32(Price);

            var accessToken = Settings.AccessToken;
            var ProductId = await _apiServices.ApartPostAsync(accessToken, TitleApart, Description, Town, Street, price, SearchOrAskJob, room, surface, FurnitureOrNot, Type);
            if (ProductId != 0)
            {
                IsRunning = false;
                Id = ProductId;
                await Shell.Current.GoToAsync($"{nameof(UploadImagePage)}?{nameof(UploadImageViewModel.ItemId)}={ProductId}");

            }
        }
    }
}
