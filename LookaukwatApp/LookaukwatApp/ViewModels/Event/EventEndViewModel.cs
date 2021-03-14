using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Image;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.ImageView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Event
{
    [QueryProperty(nameof(Price), nameof(Price))]
    [QueryProperty(nameof(Type), nameof(Type))]
    [QueryProperty(nameof(Sport_Game), nameof(Sport_Game))]
    [QueryProperty(nameof(ArtisteName), nameof(ArtisteName))]
    [QueryProperty(nameof(SearchOrAskJob), nameof(SearchOrAskJob))]
    [QueryProperty(nameof(Rubrique), nameof(Rubrique))]
    public class EventEndViewModel : BaseViewModel
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
        private string rubrique;
        public string Rubrique
        {
            get => rubrique;
            set => SetProperty(ref rubrique, value);
        }

        private string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, Uri.UnescapeDataString(value));
        }
        private string artisteName;
        public string ArtisteName
        {
            get => artisteName;
            set => SetProperty(ref artisteName, value);
        }

        private string sport_Game;
        public string Sport_Game
        {
            get => sport_Game;
            set => SetProperty(ref sport_Game, value);
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        bool isLookaukwat = false;
        public bool IsLookaukwat
        {
            get { return isLookaukwat; }
            set { SetProperty(ref isLookaukwat, value); }
        }

        bool isProvider = false;
        public bool IsProvider
        {
            get { return isProvider; }
            set
            {
                SetProperty(ref isProvider, value);
                if (value)
                {
                    GetProviderList();
                }
            }
        }
        private string provider;
        public string Provider
        {
            get => provider;
            set => SetProperty(ref provider, Uri.UnescapeDataString(value));
        }

        private int stock = 1;
        public int Stock
        {
            get { return stock; }
            set { SetProperty(ref stock, value); }
        }
        private bool ValidateLoging()
        {
            return !String.IsNullOrWhiteSpace(TitleApart)
                && !String.IsNullOrWhiteSpace(Description)
                && !String.IsNullOrWhiteSpace(Street);
        }

        public EventEndViewModel()
        {
            PostEventCommad = new Command(OnPostApart, ValidateLoging);
            TitlePage = "Titre,description, ville, quartier...";
            TownList = StaticListViewModel.GetTownCameroonList;
            this.PropertyChanged +=
               (_, __) => PostEventCommad.ChangeCanExecute();
            CheckLookaukwat();
        }

        ObservableCollection<string> providers = new ObservableCollection<string>();
        public ObservableCollection<string> Providers { get => providers; set => SetProperty(ref providers, value); }

        private IDictionary<string, string> listProviders = new Dictionary<string, string>();
        public IDictionary<string, string> ListProviders { get => listProviders; set => SetProperty(ref listProviders, value); }

        public Command PostEventCommad { get; }

        private void CheckLookaukwat()
        {
            if (Settings.Username == "contact@lookaukwat.com")
                IsLookaukwat = true;
        }

        private async void GetProviderList()
        {
            Providers.Clear();
            ListProviders.Clear();
            ListProviders = await _apiServices.GetListProvidersAsync();

            foreach (var key in ListProviders.Keys)
            {
                Providers.Add(key);
            }
        }
        async void OnPostApart()
        {
            IsRunning = true;
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");
                IsRunning = false;
                return;
            }
            //var coor = await _apiServices.GetCoodinateAsync(Town, Street);
            
            int price = Convert.ToInt32(Price);

            var accessToken = Settings.AccessToken;
            try
            {
                string Provider_Id = null;
                if (!string.IsNullOrWhiteSpace(Provider))
                {
                    Provider_Id = ListProviders[Provider];
                }
                var ProductId = await _apiServices.EventPostAsync(accessToken, TitleApart, Description, Town, Street, price, SearchOrAskJob, Rubrique, Type, ArtisteName, Sport_Game, Provider_Id, Stock);
                if (ProductId != 0)
                {
                    IsRunning = false;
                    Id = ProductId;
                    await Shell.Current.GoToAsync($"{nameof(UploadImagePage)}?{nameof(UploadImageViewModel.ItemId)}={ProductId}");

                }
            }
            catch (Exception e) { }

        }
    }
}
