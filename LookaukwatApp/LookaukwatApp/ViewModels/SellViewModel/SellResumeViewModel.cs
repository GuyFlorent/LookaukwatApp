using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Views.SellView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.SellViewModel
{
     public  class SellResumeViewModel : BaseViewModel
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
        private string itemPrice;
        public string ItemPrice
        {
            get => itemPrice;
            set => SetProperty(ref itemPrice, value);
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
        private string payementMethod;
        public string PayementMethod
        {
            get => payementMethod;
            set => SetProperty(ref payementMethod, value);
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string image;
        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        private string imagePayement;
        public string ImagePayement
        {
            get => imagePayement;
            set => SetProperty(ref imagePayement, value);
        }

        //check deliverd or store taken
        bool isStoreTaken = false;
        public bool IsStoreTaken
        {
            get { return isStoreTaken; }
            set
            {
                SetProperty(ref isStoreTaken, value);
               
            }
        }



        bool isHomeDeliverd = false;
        public bool IsHomeDeliverd
        {
            get { return isHomeDeliverd; }
            set
            {
                SetProperty(ref isHomeDeliverd, value);
               
            }
        }

        public Command ValidCommad { get; }
        public SellResumeViewModel()
        {
            TitlePage = "Résumé de la commande";
            ValidCommad = new Command(OnValid);
            Populate();
        }

        private void Populate()
        {
            ItemPurchaseModelViewModel item = JsonConvert.DeserializeObject<ItemPurchaseModelViewModel>(Settings.ItemPurchase);

            ItemPrice = item.Price;
            DeliveredPrice = item.DeliveredPrice;
            TotalPrice = item.TotalPrice;
            Image = item.Image;
            Title = item.Title;
            if(DeliveredPrice == " 0 ")
            {
                IsHomeDeliverd = false;
                isStoreTaken = true;
            }
            else
            {
                IsHomeDeliverd = true;
                isStoreTaken = false;
            }

            switch (item.PayementMethod)
            {
                case "MTN":
                    ImagePayement = "mtn_icon";
                    break;
                case "ORANGE":
                    ImagePayement = "orange_icon";
                    break;
                case "VISA":
                    ImagePayement = "visa_icon";
                    break;
            }

            DeliverAdressModelViewModel Json = JsonConvert.DeserializeObject<DeliverAdressModelViewModel>(Settings.AddressDelivered);
           
            FirstName = Json.FirstName;
            LastName = Json.LastName;
            Number = Json.Number;
            Telephone = Json.Telephone;
            Street = Json.Street;
            Town = Json.Town;

            if (Json.Distance < 1)
            {
                Distance = Convert.ToInt32(Json.Distance * 100).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() + " m";
            }
            else
            {
                Distance = Convert.ToInt32(Json.Distance).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() + " Km";
            }

        }

        private async void OnValid()
        {
            await Shell.Current.GoToAsync($"{nameof(SellPayementConfirmationPage)}");
        }
    }
}
