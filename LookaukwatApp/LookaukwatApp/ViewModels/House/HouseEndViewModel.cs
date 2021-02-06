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

namespace LookaukwatApp.ViewModels.House
{
    [QueryProperty(nameof(Price), nameof(Price))]
    [QueryProperty(nameof(Type), nameof(Type))]
    [QueryProperty(nameof(Rubrique), nameof(Rubrique))]
    [QueryProperty(nameof(SearchOrAskJob), nameof(SearchOrAskJob))]
    [QueryProperty(nameof(FabricMaterial), nameof(FabricMaterial))]
    [QueryProperty(nameof(State), nameof(State))]
    [QueryProperty(nameof(Color), nameof(Color))]
    public class HouseEndViewModel : BaseViewModel
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
        private string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, Uri.UnescapeDataString(value));
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


        private string fabricMaterial;
        public string FabricMaterial
        {
            get => fabricMaterial;
            set => SetProperty(ref fabricMaterial, Uri.UnescapeDataString(value));
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

        public HouseEndViewModel()
        {
            PostHouseCommad = new Command(OnPostHouse, ValidateLoging);
            TitlePage = "Titre,description, ville, quartier...";
            TownList = StaticListViewModel.GetTownCameroonList;
            this.PropertyChanged +=
               (_, __) => PostHouseCommad.ChangeCanExecute();
        }

        public Command PostHouseCommad { get; }


        async void OnPostHouse()
        {
            IsRunning = true;
            int price = Convert.ToInt32(Price);

            var accessToken = Settings.AccessToken;
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsRunning = true;
                return;
            }
            try
            {
                var ProductId = await _apiServices.HousePostAsync(accessToken, Title, Description, Town, Street, price, SearchOrAskJob, Rubrique, Color, Type, State, FabricMaterial);

                if (ProductId != 0)
                {
                    IsRunning = false;
                    Id = ProductId;
                    await Shell.Current.GoToAsync($"{nameof(UploadImagePage)}?{nameof(UploadImageViewModel.ItemId)}={ProductId}");

                }
            }catch(Exception e) { }

           
        }
    }
}
