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

namespace LookaukwatApp.ViewModels.Job
{
    public class JobViewModel : BaseViewModel
    {
       
        ApiServices _apiServices = new ApiServices();
        public IList<string> ContractList { get; }
        public IList<string> ActivitysectorList { get; }
        public IList<string> SearchOrSaskList { get; }
        public IList<string> TownList { get; }

        private int id;
        public int Id
        {
            get => id;
            set => id = value;
        }
        private string titleJob;
        public string TitleJob
        {
            get => titleJob;
            set => SetProperty(ref titleJob, value);
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
        private int price;
        public int Price
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
        private string typeContract;
        public string TypeContract
        {
            get => typeContract;
            set => SetProperty(ref typeContract, value);
        }
        private string activitySector;
        public string ActivitySector
        {
            get => activitySector;
            set 
            { 
                SetProperty(ref activitySector, value);
                if (value != "Autre")
                {
                    SearchrText = "Je recherche un emploi de " + value;
                    OfferText = "J'offre un emploi de " + value;
                }
            }
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
            return !String.IsNullOrWhiteSpace(TitleJob)
                && !String.IsNullOrWhiteSpace(Description)
                && !String.IsNullOrWhiteSpace(Street)
                && !String.IsNullOrWhiteSpace(ActivitySector)
                && !String.IsNullOrWhiteSpace(TypeContract);
        }


        private bool isOffer = true;
        public bool IsOffer
        {
            get { return isOffer; }
            set
            {
                SetProperty(ref isOffer, value);

                if (value == true)
                {
                    IsSearch = false;
                    SearchOrAskJob = "J'offre";
                }
            }
        }
        private bool isSearch = false;

        public bool IsSearch
        {
            get { return isSearch; }
            set
            {
                SetProperty(ref isSearch, value);

                if (value == true)
                {
                    IsOffer = false;
                    SearchOrAskJob = "Je recherche";

                }
            }
        }

        private string offerText = "J'offre du travail / prestation de service";
        public string OfferText
        {
            get => offerText;
            set => SetProperty(ref offerText, value);
        }

        private string searchText = "Je recherche du travail / prestation de service";
        public string SearchrText
        {
            get => searchText;
            set => SetProperty(ref searchText, value);
        }

        public JobViewModel()
        {
            PostJobCommad = new Command(OnPostJob, ValidateLoging);
            TitlePage = "Titre, salaire, description...";
            ContractList = StaticListViewModel.GetWorkContratTypeList;
            ActivitysectorList = StaticListViewModel.GetActivitySectortList;
            TownList = StaticListViewModel.GetTownCameroonList;
            SearchOrSaskList = StaticListViewModel.OfferOSearchList;
            this.PropertyChanged +=
               (_, __) => PostJobCommad.ChangeCanExecute();

            CheckLookaukwat();
        }

        ObservableCollection<string> providers = new ObservableCollection<string>();
        public ObservableCollection<string> Providers { get => providers; set => SetProperty(ref providers, value); }

        private IDictionary<string, string> listProviders = new Dictionary<string, string>();
        public IDictionary<string, string> ListProviders { get => listProviders; set => SetProperty(ref listProviders, value); }

        public Command PostJobCommad { get; }

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
        async void OnPostJob()
        {
            //var coor = await _apiServices.GetCoodinateAsync(Town, Street);
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
                string Provider_Id = null;
                if (!string.IsNullOrWhiteSpace(Provider))
                {
                    Provider_Id = ListProviders[Provider];
                }
                var accessToken = Settings.AccessToken;
                var ProductId = await _apiServices.JobPostAsync(accessToken, TitleJob, Description, Town, Street, Price, SearchOrAskJob, TypeContract, ActivitySector, Provider_Id, Stock);
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
