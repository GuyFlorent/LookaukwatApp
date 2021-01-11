using LookaukwatApp.ViewModels.Mode;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.HouseView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.House
{
    public class HouseViewModel : BaseViewModel
    {
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


        public HouseViewModel()
        {
            NextMultimediaCommad = new Command(OnNextMultimedia);
            //TitlePage = "Titre,description, ville, quartier...";
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


        public Command NextMultimediaCommad { get; }


        async void OnNextMultimedia()
        {

            await Shell.Current.GoToAsync($"{nameof(HouseAddPage)}?{nameof(HouseEndViewModel.Price)}={Price}&{nameof(HouseEndViewModel.Rubrique)}={Rubrique}&{nameof(HouseEndViewModel.Type)}={Type}&{nameof(HouseEndViewModel.FabricMaterial)}={FabricMaterial}&{nameof(HouseEndViewModel.Color)}={Color}&{nameof(HouseEndViewModel.SearchOrAskJob)}={SearchOrAskJob}&{nameof(ModeEndViewModel.State)}={State}");

        }

    }
}
