using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.SellViewModel
{
     public  class SellPayementConfirmationViewModel : BaseViewModel
    {
        // Card payement
        private string cardNumber;
        public string CardNumber
        {
            get => cardNumber;
            set => SetProperty(ref cardNumber, value);
        }
        private string expireYear;
        public string ExpireYear
        {
            get => expireYear;
            set => SetProperty(ref expireYear, value);
        }
        private string expireMonth;
        public string ExpireMonth
        {
            get => expireMonth;
            set => SetProperty(ref expireMonth, value);
        }
        private string cVV;
        public string CVV
        {
            get => cVV;
            set => SetProperty(ref cVV, value);
        }
        //For image payement

        private string imagePayement;
        public string ImagePayement
        {
            get => imagePayement;
            set => SetProperty(ref imagePayement, value);
        }


        // For Mobile money

        private string number;
        public string Number
        {
            get => number;
            set => SetProperty(ref number, value);
        }


        //bool for payement method
        bool isMtn = false;
        public bool IsMtn
        {
            get { return isMtn; }
            set
            {
                SetProperty(ref isMtn, value);
               
            }
        }

        bool isOrange = false;
        public bool IsOrange
        {
            get { return isOrange; }
            set
            {
                SetProperty(ref isOrange, value);
               
            }
        }
        bool isVisa = false;
        public bool IsVisa
        {
            get { return isVisa; }
            set
            {
                SetProperty(ref isVisa, value);
               
            }
        }

        public Command Buy_VisaCommad { get; }
        public Command Buy_OrangeMoneyCommad { get; }
        public Command Buy_MtnMoneyCommad { get; }
        public SellPayementConfirmationViewModel()
        {
            TitlePage = "Confirmation de paiement";
            Buy_VisaCommad = new Command(OnBuy_Visa);
            Populate();
        }
       
        private void Populate()
        {
            ItemPurchaseModelViewModel item = JsonConvert.DeserializeObject<ItemPurchaseModelViewModel>(Settings.ItemPurchase);
            switch (item.PayementMethod)
            {
                case "MTN":
                    ImagePayement = "mtn_icon";
                    IsMtn = true;
                    IsOrange = false;
                    IsVisa = false;
                    break;
                case "ORANGE":
                    ImagePayement = "orange_icon";
                    IsMtn = false;
                    IsOrange = true;
                    IsVisa = false;
                    break;
                case "VISA":
                    ImagePayement = "visa_icon";
                    IsMtn = false;
                    IsOrange = false;
                    IsVisa = true;
                    break;
            }
        }


        public async void OnBuy_Visa()
        {

        }
    }
}
