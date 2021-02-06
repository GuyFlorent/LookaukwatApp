using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Image;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.ImageView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Vehicule
{
    [QueryProperty(nameof(Price), nameof(Price))]
    [QueryProperty(nameof(Type), nameof(Type))]
    [QueryProperty(nameof(Rubrique), nameof(Rubrique))]
    [QueryProperty(nameof(Brand), nameof(Brand))]
    [QueryProperty(nameof(SearchOrAskJob), nameof(SearchOrAskJob))]
    [QueryProperty(nameof(Model), nameof(Model))]
    [QueryProperty(nameof(Petrol), nameof(Petrol))]
    [QueryProperty(nameof(FirstYear), nameof(FirstYear))]
    [QueryProperty(nameof(Year), nameof(Year))]
    [QueryProperty(nameof(Mileage), nameof(Mileage))]
    [QueryProperty(nameof(NumberOfDoor), nameof(NumberOfDoor))]
    [QueryProperty(nameof(GearBox), nameof(GearBox))]
    [QueryProperty(nameof(State), nameof(State))]
    [QueryProperty(nameof(Color), nameof(Color))]
    public class VehiculeEndViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        public IList<string> TownList { get; }
        private int id;
        public int Id
        {
            get => id;
            set => id = value;
        }
        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
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
            set => SetProperty(ref searchOrAskJob, Uri.UnescapeDataString(value));
        }
        private string rubrique;
        public string Rubrique
        {
            get
            {
                return rubrique;
            }
            set
            {
                SetProperty(ref rubrique, Uri.UnescapeDataString(value));

            }

        }

        private string brand;
        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                SetProperty(ref brand, Uri.UnescapeDataString(value));

            }
        }

        private string model;
        public string Model
        {
            get => model;
            set => SetProperty(ref model, Uri.UnescapeDataString(value));
        }

        private string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, Uri.UnescapeDataString(value));
        }

        private string petrol;
        public string Petrol
        {
            get => petrol;
            set => SetProperty(ref petrol, Uri.UnescapeDataString(value));
        }
        private string firstYear;
        public string FirstYear
        {
            get => firstYear;
            set => SetProperty(ref firstYear, Uri.UnescapeDataString(value));
        }
        private string year;
        public string Year
        {
            get => year;
            set => SetProperty(ref year, Uri.UnescapeDataString(value));
        }

        private string mileage;
        public string Mileage
        {
            get => mileage;
            set => SetProperty(ref mileage, Uri.UnescapeDataString(value));
        }

        private string numberOfDoor;
        public string NumberOfDoor
        {
            get => numberOfDoor;
            set => SetProperty(ref numberOfDoor, Uri.UnescapeDataString(value));
        }

        private string gearBox;
        public string GearBox
        {
            get => gearBox;
            set => SetProperty(ref gearBox, value);
        }

        private string state;
        public string State
        {
            get => state;
            set => SetProperty(ref state, Uri.UnescapeDataString(value));
        }

        private string color;
        public string Color
        {
            get => color;
            set => SetProperty(ref color, Uri.UnescapeDataString(value));
        }


        private bool ValidateLoging()
        {
            return !String.IsNullOrWhiteSpace(Title)
                && !String.IsNullOrWhiteSpace(Description)
                && !String.IsNullOrWhiteSpace(Street);
        }

        public VehiculeEndViewModel()
        {
            PostVehiculeCommad = new Command(OnPostVehicule, ValidateLoging);
            TitlePage = "Titre,description, ville, quartier...";
            TownList = StaticListViewModel.GetTownCameroonList;
            this.PropertyChanged +=
               (_, __) => PostVehiculeCommad.ChangeCanExecute();
        }

        public Command PostVehiculeCommad { get; }


        async void OnPostVehicule()
        {
            IsRunning = true;
            int price = Convert.ToInt32(Price);

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsRunning = false;

                return;
            }

            try
            {
                var accessToken = Settings.AccessToken;
                var ProductId = await _apiServices.VehiculePostAsync(accessToken, Title, Description, Town, Street, price, SearchOrAskJob, Rubrique, Brand, Color, Type, Petrol, State, FirstYear, Year, Mileage, NumberOfDoor, GearBox, Model);

                if (ProductId != 0)
                {
                    IsRunning = false;
                    Id = ProductId;
                    await Shell.Current.GoToAsync($"{nameof(UploadImagePage)}?{nameof(UploadImageViewModel.ItemId)}={ProductId}");

                }
            }catch(Exception e) { Console.WriteLine(e.Message); }

           
        }
    }
}
