using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Views.SellView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.SellViewModel
{
    public class SellPayementMethodViewModel : BaseViewModel
    {
        private string itemPrice;
        public string ItemPrice
        {
            get => itemPrice;
            set => SetProperty(ref itemPrice,value);
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

        //Bool for payement method

        private string payementMethod;
        public string PayementMethod
        {
            get => payementMethod;
            set => SetProperty(ref payementMethod, value);
        }


        bool isMtn = true;
        public bool IsMtn
        {
            get { return isMtn; }
            set 
            { 
                SetProperty(ref isMtn, value);
                if (value)
                {
                    PayementMethod = "MTN";
                    IsOrange = false;
                    IsVisa = false;
                }
            }
        }

        bool isOrange = false;
        public bool IsOrange
        {
            get { return isOrange; }
            set
            {
                SetProperty(ref isOrange, value);
                if (value)
                {
                    PayementMethod = "ORANGE";
                    IsMtn = false;
                    IsVisa = false;
                }
            }
        }
        bool isVisa = false;
        public bool IsVisa
        {
            get { return isVisa; }
            set
            {
                SetProperty(ref isVisa, value);
                if (value)
                {
                    PayementMethod = "VISA";
                    IsOrange = false;
                    IsMtn = false;
                }
            }
        }

        public Command ResumeCommad { get; }
        public SellPayementMethodViewModel()
        {
            TitlePage = "Méthode de paiement";
            ResumeCommad = new Command(OnResume);

            Populate();
        }

        private void Populate()
        {
            ItemPurchaseModelViewModel item = JsonConvert.DeserializeObject<ItemPurchaseModelViewModel>(Settings.ItemPurchase);

            ItemPrice = item.Price;
            DeliveredPrice = item.DeliveredPrice;
            TotalPrice = item.TotalPrice;
        }

        public async void OnResume()
        {
            ItemPurchaseModelViewModel item = JsonConvert.DeserializeObject<ItemPurchaseModelViewModel>(Settings.ItemPurchase);
            item.PayementMethod = PayementMethod;
            Settings.ItemPurchase = JsonConvert.SerializeObject(item);

            await Shell.Current.GoToAsync($"{nameof(SellResumePage)}");
        }
    }
}
