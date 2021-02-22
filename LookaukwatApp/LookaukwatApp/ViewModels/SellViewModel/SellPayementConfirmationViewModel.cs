using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.SellViewModel
{
     public  class SellPayementConfirmationViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();
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
            Buy_VisaCommad = new Command(OnBuy_Visa, Validate);
            this.PropertyChanged +=
               (_, __) => Buy_VisaCommad.ChangeCanExecute();
            Populate();
        }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(CardNumber)
                   && !string.IsNullOrWhiteSpace(ExpireYear)
                   && !string.IsNullOrWhiteSpace(ExpireMonth)
                   && !string.IsNullOrWhiteSpace(CVV)
                   ;

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


                StripeConfiguration.ApiKey = "sk_test_51ILqWeHYZYz66rr3vVNW9ZxHIh4BwxW4nyWce7L6F44Ijvl9PR9YUHiGJjS1BVvB2FPB2EymXKDb9fDyUGiPhLmm00Q38uSEmD";
                // Step 1: create card option

                CreditCardOptions stripeOption = new CreditCardOptions();
                stripeOption.Number = CardNumber;
                stripeOption.ExpYear = Convert.ToInt64(ExpireYear);
                stripeOption.ExpMonth = Convert.ToInt64(ExpireMonth);
                stripeOption.Cvc = CVV;


                // step 2: Assign card to token object
                TokenCreateOptions stripeCard = new TokenCreateOptions();
                stripeCard.Card = stripeOption;

                TokenService service = new TokenService();
                Token newToken = service.Create(stripeCard);

                // step 3: assign the token to the source
                var option = new SourceCreateOptions
                {
                    Type = SourceType.Card,
                    Currency = "xaf",
                    Token = newToken.Id
                };

                var sourceService = new SourceService();
                Source source = sourceService.Create(option);

                // step 4: create customer
                DeliverAdressModelViewModel Json = JsonConvert.DeserializeObject<DeliverAdressModelViewModel>(Settings.AddressDelivered);


                CustomerCreateOptions customer = new CustomerCreateOptions
                {
                    Name = Json.FirstName + " " + Json.LastName,
                    Email = Settings.Username,
                    Description = "Buy a thing in lookaukwat",
                    Address = new AddressOptions { City = Json.Town, Country = "Cameroon", Line1 = Json.Street, }
                };

                var customerService = new CustomerService();
                var cust = customerService.Create(customer);

                // step 5: charge option
                ItemPurchaseModelViewModel item = JsonConvert.DeserializeObject<ItemPurchaseModelViewModel>(Settings.ItemPurchase);

                var TotalPrice = Convert.ToInt64(item.TotalPrice_int);
                var chargeoption = new ChargeCreateOptions
                {
                    Amount = TotalPrice,
                    Currency = "XAF",
                    ReceiptEmail = "wangueujunior23@gmail.com",
                    Customer = cust.Id,
                    Source = source.Id
                };

                // step 6: charge the customer
                var chargeService = new ChargeService();
                Charge charge = await chargeService.CreateAsync(chargeoption);
                if (charge.Status == "succeeded")
                {
                    int result = await _apiServices.CommandPostAsync(item.Id, item.PayementMethod, item.DeliveredPrice_int, item.TotalPrice_int,
                        Json.FirstName, Json.LastName, Json.Town, Json.Street, Json.Number, Json.Telephone, Json.Distance);
                    IsBusy = false;
                    string text = "Votre numéro de commande est " + result + "." + Environment.NewLine +
                        "Votre facture et suivie de votre commande se trouve dans :" + Environment.NewLine +
                        "- Compte, " + Environment.NewLine +
                        "- Transactions " + Environment.NewLine +
                        "Lookaukwat vous remercie de votre confiance !";

                    await Shell.Current.DisplayAlert("Commande validée avec succès !", text, "OK");
                    await Shell.Current.GoToAsync("//MainPage/UserProfilePage/UserTransactionsPage");
                }
                else
                {
                    
                    await Shell.Current.DisplayAlert("Echec de paiement !", "Vérifiez votre compte", "OK");
                }
            }catch(Exception e) 
            {

                Console.WriteLine(e.Message);
            }
            finally { IsBusy = false; }
        }
    }
}
