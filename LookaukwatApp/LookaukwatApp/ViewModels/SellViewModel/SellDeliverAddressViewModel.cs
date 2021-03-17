using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.SellView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.SellViewModel
{
    [QueryProperty(nameof(JsonAddress), nameof(JsonAddress))]
    public class SellDeliverAddressViewModel : BaseViewModel
    {
        private string jsonAddress;
        public string JsonAddress
        {
            get => jsonAddress;
            set 
            { 
                SetProperty(ref jsonAddress, value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Populate_Address(value);
                }
            }
        }

       

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        private string street;
        public string Street
        {
            get => street;
            set => SetProperty(ref street, value);
        }

        private string town;
        public string Town
        {
            get => town;
            set => SetProperty(ref town, value);
        }

        private string number;
        public string Number
        {
            get => number;
            set => SetProperty(ref number, value);
        }

        private string telephone;
        public string Telephone
        {
            get => telephone;
            set => SetProperty(ref telephone, value);
        }
        public Command SaveDeliverAdressCommad { get; }
        public IList<string> TownList { get; }
        public SellDeliverAddressViewModel()
        {
            TitlePage = "Adresse de livraison/Facturation";
            TownList = StaticListViewModel.GetTownCameroonList;
            SaveDeliverAdressCommad = new Command(OnSaveDeliverAdress, Validate);
            this.PropertyChanged +=
               (_, __) => SaveDeliverAdressCommad.ChangeCanExecute();
        }

        private bool Validate()
        {
            return !String.IsNullOrWhiteSpace(FirstName)
                && !String.IsNullOrWhiteSpace(LastName)
                && !String.IsNullOrWhiteSpace(Street)
                && !String.IsNullOrWhiteSpace(Town)
                && !String.IsNullOrWhiteSpace(Number);
        }


        public async void OnSaveDeliverAdress()
        {
            //get lat and lon
            var address = $"{Street + " " + Town + " " + "Cameroun"}";
            var locations = await Geocoding.GetLocationsAsync(address);
            var location = locations?.FirstOrDefault();
            DeliverAdressModelViewModel adress = new DeliverAdressModelViewModel() { };
            if (location != null)
            {
                adress.Lat = location.Latitude.ToString();
                adress.Lon = location.Longitude.ToString();
            }
            adress.FirstName = FirstName;
            adress.LastName = LastName;
            adress.Number = Number;
            adress.Telephone = Telephone;
            adress.Town = Town;
            adress.Street = Street;

           
            // serialized in json
            string JsonAddress = JsonConvert.SerializeObject(adress);

            Settings.AddressDelivered = JsonAddress;

            await Shell.Current.GoToAsync($"{nameof(SellDeliveredTypePage)}");
        }


        private void Populate_Address(string jsonAddress)
        {
            TitlePage = "Modifier adresse livraison/Facturation";
            DeliverAdressModelViewModel Json = JsonConvert.DeserializeObject<DeliverAdressModelViewModel>(jsonAddress);
            FirstName = Json.FirstName;
            LastName = Json.LastName;
            Number = Json.Number;
            Telephone = Json.Telephone;
            Street = Json.Street;
            Town = Json.Town;
        }
    }
}
