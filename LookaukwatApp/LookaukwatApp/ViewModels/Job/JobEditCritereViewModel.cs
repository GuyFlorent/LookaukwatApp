﻿using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.OtherServices;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.JobView;
using LookaukwatApp.Views.MessageView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Job
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class JobEditCritereViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        private string itemId;
        private int price;
        private string contratType;
        private string activitySector;
        private string searchOrAsk;

        public IList<string> ContractList { get; }
        public IList<string> ActivitysectorList { get; }
        public IList<string> SearchOrSaskList { get; }

        public Command EditCommand { get; set; }
       
       
        public int Id { get; set; }

        public string TypeContract
        {
            get => contratType;
            set => SetProperty(ref contratType, value);
        }
        public string ActivitySector
        {
            get => activitySector;
            set => SetProperty(ref activitySector, value);
        }
        public string SearchOrAskJob
        {
            get => searchOrAsk;
            set => SetProperty(ref searchOrAsk, value);
        }
      
        public int Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
      
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public JobEditCritereViewModel()
        {
            TitlePage = "Modifier les critères";

            ContractList = StaticListViewModel.GetWorkContratTypeList;
            ActivitysectorList = StaticListViewModel.GetActivitySectortList;
            SearchOrSaskList = StaticListViewModel.OfferOSearchList;
            EditCommand = new Command(OnEdit, Validate);
            this.PropertyChanged +=
               (_, __) => EditCommand.ChangeCanExecute();

        }

        private bool Validate()
        {
            return !String.IsNullOrWhiteSpace(SearchOrAskJob);
                
        }
        public async void OnEdit()
        {
            var accessToken = Settings.AccessToken;
            await _apiServices.EditJobCritereAsync(ItemId, Price, SearchOrAskJob, TypeContract, ActivitySector, accessToken);
            await Shell.Current.DisplayAlert("Information", "Modifier avec succès", "Ok");
            await Shell.Current.GoToAsync("..");

        }


        public async void LoadItemId(string itemId)
        {
            IsRunning = true;
            try
            {
                var id = Convert.ToInt32(itemId);
                var item = await _apiServices.GetUniqueJobCritereAsync(id);
                Id = item.id;
                ActivitySector = item.ActivitySector;
                TypeContract = item.TypeContract;
                SearchOrAskJob = item.SearchOrAskJob;
                Price = item.Price;
                IsRunning = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

        }
    }
}