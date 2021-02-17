using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Views.SellView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.SellViewModel
{
     public  class SellDeliverTypeViewModel : BaseViewModel
    {

        // For Deliverd
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

        // For item
        private int itemPrice;
        public string ItemPrice
        {
            get => itemPrice.ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim();
            set => SetProperty(ref itemPrice, Convert.ToInt32(value));
        }

        private string deliveredPrice;
        public string DeliveredPrice
        {
            get => deliveredPrice;
            set => SetProperty(ref deliveredPrice, value);
        }

        private string totalPrice;
        public string TotalPrice
        {
            get => totalPrice;
            set => SetProperty(ref totalPrice, value);
        }

        private string distance;
        public string Distance
        {
            get => distance;
            set => SetProperty(ref distance, value);
        }

        //check deliverd or store taken
        bool isStoreTaken = true;
        public bool IsStoreTaken
        {
            get { return isStoreTaken; }
            set 
            { 
                SetProperty(ref isStoreTaken, value);
                if (value)
                {
                    PopulateStoreTake();
                   
                }
            }
        }

      

        bool isHomeDeliverd = false;
        public bool IsHomeDeliverd
        {
            get { return isHomeDeliverd; }
            set 
            { 
                SetProperty(ref isHomeDeliverd, value);
                if (value)
                {
                    PopulateHomeDeliverd();

                }
            }
        }

        
            public Command EditDeliverAdressCommand { get; }
            public Command PayementCommad { get; }
        public SellDeliverTypeViewModel()
        {
            TitlePage = "Choisir le type de livraison";
            EditDeliverAdressCommand = new Command(OnEditDelivereAdress);
            PayementCommad = new Command(OnPayement);
            Populate_Address();
        }

        public async void OnPayement()
        {

            ItemPurchaseModelViewModel item = JsonConvert.DeserializeObject<ItemPurchaseModelViewModel>(Settings.ItemPurchase);
            item.TotalPrice = TotalPrice;
            item.DeliveredPrice = deliveredPrice;
            Settings.ItemPurchase = JsonConvert.SerializeObject(item);

            await Shell.Current.GoToAsync($"{nameof(SellPayementMethodPage)}");

        }
        public async void OnEditDelivereAdress()
        {
            await Shell.Current.GoToAsync($"{nameof(SellDeliveredAdressPage)}?{nameof(SellDeliverAddressViewModel.JsonAddress)}={Settings.AddressDelivered}");
      
        }

        private void Populate_Address()
        {
            
            DeliverAdressModelViewModel Json = JsonConvert.DeserializeObject<DeliverAdressModelViewModel>(Settings.AddressDelivered);
            FirstName = Json.FirstName;
            LastName = Json.LastName;
            Number = Json.Number;
            Telephone = Json.Telephone;
            Street = Json.Street;
            Town = Json.Town;
            
            

            ItemPurchaseModelViewModel item = JsonConvert.DeserializeObject<ItemPurchaseModelViewModel>(Settings.ItemPurchase);

            ItemPrice = item.Price;


            //getting distance between item and receiver
            
            if (!double.TryParse(item.Lat.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double latItem))
                return;

            if (!double.TryParse(item.Lon.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double lonItem))
                return;
            //for client
            if (!double.TryParse(Json.Lat.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double latAdress))
                return;

            if (!double.TryParse(Json.Lon.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double lonAdress))
                return;

            Location ItemLocation = new Location(latItem, lonItem);
            Location ClientLocation = new Location(latAdress, lonAdress);
            Json.Distance = Location.CalculateDistance(ItemLocation, ClientLocation, DistanceUnits.Kilometers);

            // serialized in json
            string JsonAddress = JsonConvert.SerializeObject(Json);

            Settings.AddressDelivered = JsonAddress;

            if (Json.Distance < 1)
            {
                Distance = Convert.ToInt32(Json.Distance * 100).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() + " m";
                DeliveredPrice = Convert.ToInt32(Json.Distance * 100 * 0.2).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim();

            }
            else
            {
                Distance = Convert.ToInt32(Json.Distance).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() + " Km";
                DeliveredPrice = Convert.ToInt32(Json.Distance * 200).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim();

            }
        }

        private void PopulateStoreTake()
        {

            DeliveredPrice = " 0 ";
            IsHomeDeliverd = false;
           
            TotalPrice  = (itemPrice).ToString();
        }

        private void PopulateHomeDeliverd()
        {
            int Delivered;
            DeliverAdressModelViewModel Json = JsonConvert.DeserializeObject<DeliverAdressModelViewModel>(Settings.AddressDelivered);
            if (Json.Distance < 1)
            {
                DeliveredPrice = Convert.ToInt32(Json.Distance * 100 * 0.2).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim();

                Delivered = Convert.ToInt32(Json.Distance * 100 * 0.2);

            }
            else
            {
                DeliveredPrice = Convert.ToInt32(Json.Distance * 200).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim();

                Delivered = Convert.ToInt32(Json.Distance * 200);
            }

            IsStoreTaken = false;
            
            TotalPrice = (itemPrice + Delivered).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim();
        }
    }
}
