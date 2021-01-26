using LookaukwatApp.Models;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.ModeView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Mode
{
    public class ModeViewModel : BaseViewModel
    {
        ObservableCollection<string> types = new ObservableCollection<string>();
        public ObservableCollection<string> Types { get => types; set => SetProperty(ref types, value); }

        ObservableCollection<string> brands = new ObservableCollection<string>();
        public ObservableCollection<string> Brands { get => brands; set => SetProperty(ref brands, value); }

        ObservableCollection<string> sizes = new ObservableCollection<string>();
        public ObservableCollection<string> Sizes { get => sizes; set => SetProperty(ref sizes, value); }

        private void UpdateTypelist(string rubrique)
        {
            Types.Clear();
            Brands.Clear();
            Sizes.Clear();
            switch (rubrique)
            {
                case "Vêtements":
                    foreach (var list in TypeClothesList)
                    {
                        Types.Add(list);
                    }
                    foreach (var brand in BrandClothesList)
                    {
                        Brands.Add(brand);
                    }
                    foreach (var size in SizeClothesList)
                    {
                        Sizes.Add(size);
                    }
                    break;
                case "Chaussures":
                    foreach (var list in TypeShoesList)
                    {
                        Types.Add(list);
                    }
                    foreach (var brand in BrandShoesList)
                    {
                        Brands.Add(brand);
                    }
                    foreach (var size in SizeShoesList)
                    {
                        Sizes.Add(size);
                    }
                    break;
                case "Accesoires & Bagagerie":
                    foreach (var list in TypeAccesorieLugagesList)
                    {
                        Types.Add(list);
                    }

                    foreach (var brand in BrandClothesList)
                    {
                        Brands.Add(brand);
                    }
                    break;
                case "Montres & Bijoux":
                    foreach (var list in TypeWatchJewelryList)
                    {
                        Types.Add(list);
                    }

                    foreach (var brand in BrandClothesList)
                    {
                        Brands.Add(brand);
                    }
                    break;
                case "Equipement bébé":
                    foreach (var list in TypeBabyEquipmentList)
                    {
                        Types.Add(list);
                    }

                    foreach (var brand in BrandClothesList)
                    {
                        Brands.Add(brand);
                    }
                    break;
                case "Vêtements bébé":
                    foreach (var list in TypeBabyClothesList)
                    {
                        Types.Add(list);
                    }

                    foreach (var brand in BrandClothesList)
                    {
                        Brands.Add(brand);
                    }
                    foreach (var size in SizeClothesList)
                    {
                        Sizes.Add(size);
                    }
                    break;
            }
        }



        public IList<string> TypeClothesList { get; }
        public IList<string> TypeShoesList { get; }
        public IList<string> TypeAccesorieLugagesList { get; }
        public IList<string> TypeWatchJewelryList { get; }
        public IList<string> TypeBabyEquipmentList { get; }
        public IList<string> TypeBabyClothesList { get; }
        public IList<string> RubriqueList { get; }
        public IList<string> BrandClothesList { get; }
        public IList<string> BrandShoesList { get; }
        public IList<string> UniversList { get; }
        public IList<string> SizeClothesList { get; }
        public IList<string> SizeShoesList { get; }
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
            set 
            { 
                SetProperty(ref type, value);
                if(value != "Autre")
                {
                    SearchrText = "Je recherche un(e) " + value;
                    OfferText = "Je vends un(e) " + value;
                }
                
            }
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

        private string brand;
        public string Brand
        {
            get => brand;
            set => SetProperty(ref brand, value);
        }

        private string univers;
        public string Univers
        {
            get => univers;
            set => SetProperty(ref univers, value);
        }

        private string size;
        public string Size
        {
            get => size;
            set => SetProperty(ref size, value);
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


        private bool Validate()
        {
            return !String.IsNullOrWhiteSpace(SearchOrAskJob)
                && !String.IsNullOrWhiteSpace(Rubrique);
               
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

        private string offerText = "Je vends";
        public string OfferText
        {
            get => offerText;
            set => SetProperty(ref offerText, value);
        }

        private string searchText = "Je recherche";
        public string SearchrText
        {
            get => searchText;
            set => SetProperty(ref searchText, value);
        }

        public ModeViewModel()
        {
            NextModeCommad = new Command(OnNextApart, Validate);
            this.PropertyChanged +=
              (_, __) => NextModeCommad.ChangeCanExecute();
            //TitlePage = "Titre,description, ville, quartier...";
            TypeAccesorieLugagesList = StaticListModeViewModel.GetTypeAccesorieLugagesList;
            TypeBabyClothesList = StaticListModeViewModel.GetTypeBabyClothesList;
            TypeBabyEquipmentList = StaticListModeViewModel.GetTypeBabyEquipmentList;
            TypeClothesList = StaticListModeViewModel.GetTypeClothesList;
            TypeShoesList = StaticListModeViewModel.GetTypeShoesList;
            TypeWatchJewelryList = StaticListModeViewModel.GetTypeWatchJewelryList;
            RubriqueList = StaticListModeViewModel.GetRubriqueModeList;
            BrandClothesList = StaticListModeViewModel.GetBrandClothesList;
            BrandShoesList = StaticListModeViewModel.GetBrandShoesList;
            UniversList = StaticListModeViewModel.GetUniversList;
            SizeClothesList = StaticListModeViewModel.GetSeizeClothesList;
            SizeShoesList = StaticListModeViewModel.GetSizeShoesList;
            ColorList = StaticListModeViewModel.GetColorModeList;
            StateList = StaticListModeViewModel.GetStateModeList;
            SearchOrSaskList = StaticListViewModel.OfferOSearchList;
        }


        public Command NextModeCommad { get; }


        async void OnNextApart()
        {
            ModeModel model = new ModeModel
            {
                Price = Price,
                SearchOrAskJob = SearchOrAskJob,
                RubriqueMode = Rubrique,
                BrandMode = Brand,
                TypeMode = Type,
                ColorMode = Color,
                SizeMode = Size,
                StateMode = State,
                UniversMode = Univers,
               
            };

            string json = JsonConvert.SerializeObject(model);
           string data = HttpUtility.UrlEncode(json);
            //  await Shell.Current.GoToAsync($"{nameof(ModeAddPage)}?{nameof(ModeEndViewModel.Price)}={Price}&{nameof(ModeEndViewModel.Rubrique)}={Rubrique}&{nameof(ModeEndViewModel.Type)}={Type}&{nameof(ModeEndViewModel.Size)}={Size}&{nameof(ModeEndViewModel.Color)}={Color}&{nameof(ModeEndViewModel.SearchOrAskJob)}={SearchOrAskJob}&{nameof(ModeEndViewModel.Univers)}={Univers}&{nameof(ModeEndViewModel.Brand)}={Brand}&{nameof(ModeEndViewModel.State)}={State}");
            await Shell.Current.GoToAsync($"{nameof(ModeAddPage)}?{nameof(ModeEndViewModel.Json)}={data}");

        }

    }
}
