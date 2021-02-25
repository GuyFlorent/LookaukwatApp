﻿using LookaukwatApp.Helpers;
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
            CheckLookaukwat();
        }

        ObservableCollection<string> providers = new ObservableCollection<string>();
        public ObservableCollection<string> Providers { get => providers; set => SetProperty(ref providers, value); }

        private IDictionary<string, string> listProviders = new Dictionary<string, string>();
        public IDictionary<string, string> ListProviders { get => listProviders; set => SetProperty(ref listProviders, value); }

        public Command PostMultimediaCommad { get; }

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
                string Provider_Id = null;
                if (!string.IsNullOrWhiteSpace(Provider))
                {
                    Provider_Id = ListProviders[Provider];
                }
                var accessToken = Settings.AccessToken;
                var ProductId = await _apiServices.MultimediaPostAsync(accessToken, Title, Description, Town, Street, price, SearchOrAskJob, Rubrique, Brand, Model, Capacity, Provider_Id, Stock);
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
