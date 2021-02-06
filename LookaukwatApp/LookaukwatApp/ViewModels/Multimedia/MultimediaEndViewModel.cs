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

namespace LookaukwatApp.ViewModels.Multimedia
{
    [QueryProperty(nameof(Price), nameof(Price))]
    [QueryProperty(nameof(Brand), nameof(Brand))]
    [QueryProperty(nameof(SearchOrAskJob), nameof(SearchOrAskJob))]
    [QueryProperty(nameof(Rubrique), nameof(Rubrique))]
    [QueryProperty(nameof(Model), nameof(Model))]
    [QueryProperty(nameof(Capacity), nameof(Capacity))]

    public class MultimediaEndViewModel : BaseViewModel
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

        private string brand;
        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                SetProperty(ref brand, Uri.UnescapeDataString(value));

            }
        }


        private string model;
        public string Model
        {
            get => model;
            set => SetProperty(ref model, Uri.UnescapeDataString(value));
        }

        private string capacity;
        public string Capacity
        {
            get => capacity;
            set => SetProperty(ref capacity, Uri.UnescapeDataString(value));
        }

        private bool ValidateLoging()
        {
            return !String.IsNullOrWhiteSpace(Title)
                && !String.IsNullOrWhiteSpace(Description)
                && !String.IsNullOrWhiteSpace(Street);
        }

        public MultimediaEndViewModel()
        {
            PostMultimediaCommad = new Command(OnPostMultimedia, ValidateLoging);
            TitlePage = "Titre,description, ville, quartier...";
            TownList = StaticListViewModel.GetTownCameroonList;
            this.PropertyChanged +=
               (_, __) => PostMultimediaCommad.ChangeCanExecute();
        }

        public Command PostMultimediaCommad { get; }


        async void OnPostMultimedia()
        {
            IsRunning = true;
            int price = Convert.ToInt32(Price);

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsRunning = false;
                return;
            }
            try
            {
                var accessToken = Settings.AccessToken;
                var ProductId = await _apiServices.MultimediaPostAsync(accessToken, Title, Description, Town, Street, price, SearchOrAskJob, Rubrique, Brand, Model, Capacity);
                IsRunning = false;
                if (ProductId != 0)
                {
                    Id = ProductId;
                    await Shell.Current.GoToAsync($"{nameof(UploadImagePage)}?{nameof(UploadImageViewModel.ItemId)}={ProductId}");

                }
            }catch(Exception e) { Console.WriteLine(e.Message); }
            
        }
    }
}
