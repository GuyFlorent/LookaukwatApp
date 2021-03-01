using LookaukwatApp.Models;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.User
{
    [QueryProperty(nameof(CommandId), nameof(CommandId))]
    class UserBillViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        ObservableCollection<PurchaseForBill> purchases = new ObservableCollection<PurchaseForBill>();
        public ObservableCollection<PurchaseForBill> Purchases { get => purchases; set => SetProperty(ref purchases, value); }

        bool isDelivered = false;
        public bool IsHomeDelivered
        {
            get { return isDelivered; }
            set { SetProperty(ref isDelivered, value); }
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

        private string qrCode;
        public string QrCode
        {
            get => qrCode;
            set => SetProperty(ref qrCode, value);
        }

        private string getCommandId = "-1";
        public string GetCommandId
        {
            get => getCommandId;
            set => SetProperty(ref getCommandId, value);
        }

        private string commandId;
        public string CommandId
        {
            get
            {
                return commandId;
            }
            set
            {
                commandId = value;
                if (value != null)
                {
                    GetCommandId = value;
                    LoadItemId(value);
                }
            }
        }

       
        public UserBillViewModel()
        {
            TitlePage = "Facture numéro "+CommandId ;

        }

        private async void LoadItemId(string value)
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


                int IdCommand = Convert.ToInt32(value);
                CommandModel command = await _apiServices.GetCommandBillAsync(IdCommand);
                TitlePage = "Facture numéro " + value;
                // for user address
                FirstName = command.DeliveredAdress.FirstName;
                LastName = command.DeliveredAdress.LastName;
                Town = command.DeliveredAdress.Town;
                Street = command.DeliveredAdress.Street;
                Number = command.DeliveredAdress.Number;
                Telephone = command.DeliveredAdress.Telephone;

                // for purchase
                foreach (var purchase in command.Purchases)
                {
                    PurchaseForBill purchaseForBill = new PurchaseForBill();
                    purchaseForBill.Image = purchase.product.Images.Select(s => s.ImageMobile).FirstOrDefault();
                    purchaseForBill.Price = purchase.product.Price.ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() + " FCFA";
                    purchaseForBill.Quantity = purchase.Quantities;
                    purchaseForBill.Price_Per_Quantity = (purchase.product.Price * purchase.Quantities).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() + " FCFA"; ;
                    Purchases.Add(purchaseForBill);
                }
                //totalprica
                TotalPrice = command.TotalPrice.ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() + " FCFA"; ;
                // for check if isdeliverd
                IsHomeDelivered = command.IsHomeDelivered;
                DeliveredPrice = command.DeliveredPrice.ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() + " FCFA"; ;
                //For QrCode
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                IsRunning = false;
                IsBusy = true;
            }
        }

    }
}
