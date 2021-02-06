using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.Vehicule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Vehicule
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class VehiculeEditCritereViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        private string itemId;
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

        ObservableCollection<string> brands = new ObservableCollection<string>();
        public ObservableCollection<string> Brands { get => brands; set => SetProperty(ref brands, value); }

        ObservableCollection<string> models = new ObservableCollection<string>();
        public ObservableCollection<string> Models { get => models; set => SetProperty(ref models, value); }

        private void UpdateTypelist(string rubrique)
        {

            Brands.Clear();
            Models.Clear();
            switch (rubrique)
            {
                case "Voitures":

                    foreach (var brand in BrandVehiculeAutoList)
                    {
                        Brands.Add(brand);
                    }

                    break;
                case "Location voitures":
                    foreach (var brand in BrandVehiculeAutoList)
                    {
                        Brands.Add(brand);
                    }

                    break;
                case "Motos":
                    foreach (var brand in BrandVehiculeMotoList)
                    {
                        Brands.Add(brand);
                    }
                    break;
                case "Equipement Auto":
                    foreach (var brand in BrandVehiculeAutoList)
                    {
                        Brands.Add(brand);
                    }


                    break;
                case "Equipement Moto":
                    foreach (var brand in BrandVehiculeMotoList)
                    {
                        Brands.Add(brand);
                    }
                    break;

            }
        }

        private void UpdateModellist(string brand, string rubrique)
        {
            if (rubrique == "Voitures" || rubrique == "Location voitures")
            {
                Models.Clear();
                switch (brand)
                {
                    case "Toyota":
                        foreach (var model in ModelVehiculeAutoToyotaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Mercedes":
                        foreach (var model in ModelVehiculeAutoMercedesList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Hyundrai":
                        foreach (var model in ModelVehiculeAutoHyundaiList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Mazda":
                        foreach (var model in ModelVehiculeAutoMazdaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Kia":
                        foreach (var model in ModelVehiculeAutoKiaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    default:
                        break;
                }
            }
        }



        public IList<string> RubriqueVehiculeList { get; }
        public IList<string> BrandVehiculeAutoList { get; }
        public IList<string> BrandVehiculeMotoList { get; }
        public IList<string> ModelVehiculeAutoToyotaList { get; }
        public IList<string> ModelVehiculeAutoHyundaiList { get; }
        public IList<string> ModelVehiculeAutoMercedesList { get; }
        public IList<string> ModelVehiculeAutoMazdaList { get; }
        public IList<string> ModelVehiculeAutoKiaList { get; }
        public IList<string> TypeVehiculeAutoList { get; }
        public IList<string> PetrolVehiculeList { get; }
        public IList<string> NumberOfDoorVehiculeList { get; }
        public IList<string> ColorVehiculeList { get; }
        public IList<string> GearBoxVehiculeList { get; }
        public IList<string> StateVehiculeList { get; }
        public IList<string> SearchOrSaskList { get; }


        private int price;
        public int Price
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

        private string rubrique;
        public string Rubrique
        {
            get
            {
                return rubrique;
            }
            set
            {
                SetProperty(ref rubrique, value);
                UpdateTypelist(value);
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
                SetProperty(ref brand, value);
                UpdateModellist(value, Rubrique);
            }
        }

        private string model;
        public string Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }

        private string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        private string petrol;
        public string Petrol
        {
            get => petrol;
            set => SetProperty(ref petrol, value);
        }
        private string firstYear;
        public string FirstYear
        {
            get => firstYear;
            set => SetProperty(ref firstYear, value);
        }
        private string year;
        public string Year
        {
            get => year;
            set => SetProperty(ref year, value);
        }

        private string mileage;
        public string Mileage
        {
            get => mileage;
            set => SetProperty(ref mileage, value);
        }

        private string numberOfDoor;
        public string NumberOfDoor
        {
            get => numberOfDoor;
            set => SetProperty(ref numberOfDoor, value);
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
            set => SetProperty(ref state, value);
        }

        private string color;
        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }


        public VehiculeEditCritereViewModel()
        {
            EditCommand = new Command(OnEdit);
            TitlePage = "Modifier mon annonce";
            RubriqueVehiculeList = StaticListVehiculeViewModel.GetRubriqueVehiculeList;
            BrandVehiculeAutoList = StaticListVehiculeViewModel.GetBrandVehiculeAutoList;
            BrandVehiculeMotoList = StaticListVehiculeViewModel.GetBrandVehiculeMotoList;
            ModelVehiculeAutoToyotaList = StaticListVehiculeViewModel.GetModelVehiculeAutoToyotaList;
            ModelVehiculeAutoHyundaiList = StaticListVehiculeViewModel.GetModelVehiculeAutoHyundaiList;
            ModelVehiculeAutoMercedesList = StaticListVehiculeViewModel.GetModelVehiculeAutoMercedesList;
            ModelVehiculeAutoMazdaList = StaticListVehiculeViewModel.GetModelVehiculeAutoMazdaList;
            ModelVehiculeAutoKiaList = StaticListVehiculeViewModel.GetModelVehiculeAutoKiaList;
            TypeVehiculeAutoList = StaticListVehiculeViewModel.GetTypeVehiculeAutoList;
            PetrolVehiculeList = StaticListVehiculeViewModel.GetPetrolVehiculeList;
            NumberOfDoorVehiculeList = StaticListVehiculeViewModel.GetNumberOfDoorVehiculeList;
            ColorVehiculeList = StaticListVehiculeViewModel.GetColorVehiculeList;
            GearBoxVehiculeList = StaticListVehiculeViewModel.GetGearBoxVehiculeList;
            StateVehiculeList = StaticListVehiculeViewModel.GetStateVehiculeList;
            SearchOrSaskList = StaticListViewModel.OfferOSearchList;
        }

        public Command EditCommand { get; set; }

        public async void OnEdit()
        {
            IsBusy = true;
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsBusy = false;

                return;
            }

            try
            {
                var accessToken = Settings.AccessToken;
                await _apiServices.EditVehiculeCritereAsync(ItemId, Price, SearchOrAskJob, Rubrique, Brand, Type, Color, Petrol, Year, Mileage, NumberOfDoor, GearBox, Model, State, FirstYear, accessToken);
                IsBusy = false;
                await Shell.Current.DisplayAlert("Information", "Modifiée avec succès", "Ok");
                await Shell.Current.GoToAsync("..");
            }catch(Exception e) { Console.WriteLine(e.Message); }

            
        }
        public async void LoadItemId(string itemId)
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
                var id = Convert.ToInt32(itemId);
                var item = await _apiServices.GetUniqueVehiculeCritereAsync(id);
                
                SearchOrAskJob = item.SearchOrAsk;
                Price = item.Price;
                //for House model
                Rubrique = item.RubriqueVehicule;
                Brand = item.BrandVehicule;
                Type = item.TypeVehicule;
                Color = item.ColorVehicule;
                Petrol = item.PetrolVehicule;
                Year = item.YearVehicule;
                Mileage = item.MileageVehicule;
                NumberOfDoor = item.NumberOfDoorVehicule;
                GearBox = item.GearBoxVehicule;
                Model = item.ModelVehicule;
                State = item.StateVehicule;
                FirstYear = item.FirstYearVehicule;

               
                IsRunning = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

        }
    }
}
