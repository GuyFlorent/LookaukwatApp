using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Mode;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.HouseView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.House
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class HouseEditCritereViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        private string itemId;

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

        ObservableCollection<string> types = new ObservableCollection<string>();
        public ObservableCollection<string> Types { get => types; set => SetProperty(ref types, value); }

        ObservableCollection<string> fabricMaterials = new ObservableCollection<string>();
        public ObservableCollection<string> FabricMaterials { get => fabricMaterials; set => SetProperty(ref fabricMaterials, value); }

        private void UpdateTypelist(string rubrique)
        {
            Types.Clear();
            FabricMaterials.Clear();

            switch (rubrique)
            {
                case "Ameublement":
                    foreach (var list in TypeAmebleumentHouseList)
                    {
                        Types.Add(list);
                    }
                    foreach (var fabric in FabricMaterialAmmeblementDecorationList)
                    {
                        FabricMaterials.Add(fabric);
                    }

                    break;
                case "Electroménager":

                    break;
                case "Cuisine":
                    foreach (var list in TypeOutilTableHouseList)
                    {
                        Types.Add(list);
                    }

                    foreach (var fabric in FabricMaterialOutilTableList)
                    {
                        FabricMaterials.Add(fabric);
                    }
                    break;
                case "Décoration":
                    foreach (var list in TypeDecorationHouseList)
                    {
                        Types.Add(list);
                    }

                    foreach (var fabric in FabricMaterialAmmeblementDecorationList)
                    {
                        FabricMaterials.Add(fabric);
                    }
                    break;
                case "Linge de maison":
                    foreach (var list in TypeLingeHouseList)
                    {
                        Types.Add(list);
                    }

                    foreach (var fabric in FabricMaterialLingeList)
                    {
                        FabricMaterials.Add(fabric);
                    }
                    break;
                case "Bricolage":

                    break;
                case "Jardinage":

                    break;
            }
        }



        public IList<string> RubriqueList { get; }
        public IList<string> TypeAmebleumentHouseList { get; }
        public IList<string> TypeOutilTableHouseList { get; }
        public IList<string> TypeDecorationHouseList { get; }
        public IList<string> TypeLingeHouseList { get; }
        public IList<string> FabricMaterialAmmeblementDecorationList { get; }
        public IList<string> FabricMaterialOutilTableList { get; }
        public IList<string> FabricMaterialLingeList { get; }
        public IList<string> ColorList { get; }
        public IList<string> StateList { get; }
        public IList<string> SearchOrSaskList { get; }


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
        private string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
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
                SetProperty(ref rubrique, value);
                UpdateTypelist(value);
            }

        }

        private string fabricMaterial;
        public string FabricMaterial
        {
            get => fabricMaterial;
            set => SetProperty(ref fabricMaterial, value);
        }


        private string state;
        public string State
        {
            get => state;
            set => SetProperty(ref state, value);
        }

        private string color;
        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }

        public Command EditCommand { get; set; }

        public HouseEditCritereViewModel()
        {
            TitlePage = "Modifier mon annonce";
            EditCommand = new Command(OnEdit);
            RubriqueList = StaticListHouseViewModel.GetRubriqueHouseListt;
            TypeAmebleumentHouseList = StaticListHouseViewModel.GetTypeAmebleumentHouseList;
            TypeOutilTableHouseList = StaticListHouseViewModel.GetTypeOutilTableHouseList;
            TypeDecorationHouseList = StaticListHouseViewModel.GetTypeDecorationHouseList;
            TypeLingeHouseList = StaticListHouseViewModel.GetTypeLingeHouseList;
            FabricMaterialAmmeblementDecorationList = StaticListHouseViewModel.GetFabricMaterialAmmeblementDecorationList;
            FabricMaterialOutilTableList = StaticListHouseViewModel.GetFabricMaterialOutilTableList;
            FabricMaterialLingeList = StaticListHouseViewModel.GetFabricMaterialLingeList;
            ColorList = StaticListModeViewModel.GetColorModeList;
            StateList = StaticListHouseViewModel.GetStateHouseList;
            SearchOrSaskList = StaticListViewModel.OfferOSearchList;
        }


        public async void OnEdit()
        {
            IsBusy = true;
            var accessToken = Settings.AccessToken;
            await _apiServices.EditHouseCritereAsync(ItemId, Price, SearchOrAskJob, Rubrique, FabricMaterial,Type,Color,State, accessToken);
            IsBusy = false;
            await Shell.Current.DisplayAlert("Information", "Modifiée avec succès", "Ok");
            await Shell.Current.GoToAsync("..");

        }


        public async void LoadItemId(string itemId)
        {
            IsRunning = true;
            try
            {
                var id = Convert.ToInt32(itemId);
                var item = await _apiServices.GetUniqueHouseCritereAsync(id);
              
                SearchOrAskJob = item.SearchOrAsk;
                Price = item.Price;
                
                //for House model
                Rubrique = item.RubriqueHouse;
                FabricMaterial = item.FabricMaterialeHouse;
                Type = item.TypeHouse;
                Color = item.ColorHouse;
                State = item.StateHouse;

                IsRunning = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

        }
    }
}
