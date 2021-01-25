using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.MultimediaView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Multimedia
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class MultimediaEditCritereViewModel : BaseViewModel
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

        ObservableCollection<string> models = new ObservableCollection<string>();
        public ObservableCollection<string> Models { get => models; set => SetProperty(ref models, value); }

        ObservableCollection<string> brands = new ObservableCollection<string>();
        public ObservableCollection<string> Brands { get => brands; set => SetProperty(ref brands, value); }


        private void UpdateBrandlist(string rubrique)
        {
            Models.Clear();
            Brands.Clear();

            switch (rubrique)
            {
                case "Informatique":

                    foreach (var brand in BrandInformatiquePhotocopyMultimediaList)
                    {
                        Brands.Add(brand);
                    }
                    foreach (var model in ModelInformatiquePhotocopyMultimediaList)
                    {
                        Models.Add(model);
                    }

                    break;
                case "Consoles de jeux":

                    foreach (var brand in BrandConsoleGamequeMultimediaList)
                    {
                        Brands.Add(brand);
                    }
                    foreach (var model in ModelConsoleGameMultimediaList)
                    {
                        Models.Add(model);
                    }
                    break;
                case "Jeux video":

                    foreach (var brand in BrandConsoleGamequeMultimediaList)
                    {
                        Brands.Add(brand);
                    }
                    foreach (var model in ModelConsoleGameMultimediaList)
                    {
                        Models.Add(model);
                    }
                    break;
                case "Téléphonie":

                    foreach (var brand in BrandPhoneAccesorieMultimediaList)
                    {
                        Brands.Add(brand);
                    }
                    break;
                case "Accésoires téléphonie":

                    foreach (var brand in BrandPhoneAccesorieMultimediaList)
                    {
                        Brands.Add(brand);
                    }

                    break;
                case "Téléviseur":


                    foreach (var brand in BrandTVGameMultimediaList)
                    {
                        Brands.Add(brand);
                    }

                    foreach (var model in ModelTVGameMultimediaList)
                    {
                        Models.Add(model);
                    }
                    break;
                case "Son":

                    foreach (var brand in BrandSonMultimediaList)
                    {
                        Brands.Add(brand);
                    }

                    foreach (var model in ModelSonGamequeMultimediaList)
                    {
                        Models.Add(model);
                    }
                    break;
                case "Image & Camera vidéo":


                    foreach (var brand in BrandInformatiquePhotocopyMultimediaList)
                    {
                        Brands.Add(brand);
                    }
                    foreach (var model in ModelInformatiquePhotocopyMultimediaList)
                    {
                        Models.Add(model);
                    }
                    break;
                case "Photocopieuse":

                    foreach (var brand in BrandInformatiquePhotocopyMultimediaList)
                    {
                        Brands.Add(brand);
                    }
                    foreach (var model in ModelInformatiquePhotocopyMultimediaList)
                    {
                        Models.Add(model);
                    }
                    break;
            }
        }

        private void UpdateModellist(string brand, string rubrique)
        {
            if (rubrique == "Téléphonie")
            {
                Models.Clear();
                switch (brand)
                {
                    case "Apple":
                        foreach (var model in ModelApplePhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Huawei":
                        foreach (var model in ModelHuaweiPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Samsung":
                        foreach (var model in ModelSamsungPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Sony":
                        foreach (var model in ModelSonyPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Alcatel":
                        foreach (var model in ModelAlcatelPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Asus":
                        foreach (var model in ModelAzusPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Honor":
                        foreach (var model in ModelHonorPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "HTC":
                        foreach (var model in ModelHTCPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Lenovo":
                        foreach (var model in ModelLenovoPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "LG":
                        foreach (var model in ModelLGPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Microsoft":
                        foreach (var model in ModelMicrosoftPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Motorola":
                        foreach (var model in ModelMotorolaPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Nokia":
                        foreach (var model in ModelMicrosoftPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "One plus":
                        foreach (var model in ModelOnePlusPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Wiko":
                        foreach (var model in ModelWikoPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Xaomi":
                        foreach (var model in ModelXaomiPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "ZTE":
                        foreach (var model in ModelZTEPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    default:
                        Models.Add("Autre");
                        break;
                }
            }
            else if (rubrique == "Accésoires téléphonie")
            {
                Models.Clear();
                switch (brand)
                {
                    case "Apple":
                        foreach (var model in ModelApplePhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Huawei":
                        foreach (var model in ModelHuaweiPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Samsung":
                        foreach (var model in ModelSamsungPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Sony":
                        foreach (var model in ModelSonyPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Alcatel":
                        foreach (var model in ModelAlcatelPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Asus":
                        foreach (var model in ModelAzusPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Honor":
                        foreach (var model in ModelHonorPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "HTC":
                        foreach (var model in ModelHTCPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Lenovo":
                        foreach (var model in ModelLenovoPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "LG":
                        foreach (var model in ModelLGPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Microsoft":
                        foreach (var model in ModelMicrosoftPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Motorola":
                        foreach (var model in ModelMotorolaPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Nokia":
                        foreach (var model in ModelMicrosoftPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "One plus":
                        foreach (var model in ModelOnePlusPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Wiko":
                        foreach (var model in ModelWikoPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "Xaomi":
                        foreach (var model in ModelXaomiPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    case "ZTE":
                        foreach (var model in ModelZTEPhoneAccesorieMultimediaList)
                        {
                            Models.Add(model);
                        }
                        break;
                    default:
                        Models.Add("Autre");
                        break;
                }
            }


        }

        public IList<string> RubriqueList { get; }
        public IList<string> BrandInformatiquePhotocopyMultimediaList { get; }
        public IList<string> BrandConsoleGamequeMultimediaList { get; }
        public IList<string> BrandPhoneAccesorieMultimediaList { get; }
        public IList<string> BrandTVGameMultimediaList { get; }
        public IList<string> BrandSonMultimediaList { get; }
        public IList<string> ModelInformatiquePhotocopyMultimediaList { get; }
        public IList<string> ModelConsoleGameMultimediaList { get; }
        public IList<string> ModelApplePhoneAccesorieMultimediaList { get; }
        public IList<string> ModelHuaweiPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelSamsungPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelSonyPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelAlcatelPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelAzusPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelHonorPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelHTCPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelLenovoPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelMicrosoftPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelLGPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelMotorolaPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelOnePlusPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelWikoPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelXaomiPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelZTEPhoneAccesorieMultimediaList { get; }
        public IList<string> ModelTVGameMultimediaList { get; }
        public IList<string> ModelSonGamequeMultimediaList { get; }
        public IList<string> CapacityMultimediaList { get; }


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
                UpdateBrandlist(value);
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
                SetProperty(ref brand, value);
                UpdateModellist(value, Rubrique);
            }
        }


        private string model;
        public string Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }

        private string capacity;
        public string Capacity
        {
            get => capacity;
            set => SetProperty(ref capacity, value);
        }


        public MultimediaEditCritereViewModel()
        {
            EditCommand = new Command(OnEdit);
            TitlePage = "Modifier l'annonce";
            RubriqueList = StaticListMultimediaViewModel.GetTypeMultimediaList;
            BrandConsoleGamequeMultimediaList = StaticListMultimediaViewModel.GetBrandConsoleGamequeMultimediaList;
            BrandInformatiquePhotocopyMultimediaList = StaticListMultimediaViewModel.GetBrandInformatiquePhotocopyMultimediaList;
            BrandTVGameMultimediaList = StaticListMultimediaViewModel.GetBrandTVGameMultimediaList;
            BrandSonMultimediaList = StaticListMultimediaViewModel.GetBrandSonMultimediaList;
            BrandPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetBrandPhoneAccesorieMultimediaList;
            ModelInformatiquePhotocopyMultimediaList = StaticListMultimediaViewModel.GetModelInformatiquePhotocopyMultimediaList;
            ModelConsoleGameMultimediaList = StaticListMultimediaViewModel.GetModelConsoleGameMultimediaList;
            ModelApplePhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelApplePhoneAccesorieMultimediaList;
            ModelHuaweiPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelHuaweiPhoneAccesorieMultimediaList;
            ModelSamsungPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelSamsungPhoneAccesorieMultimediaList;
            ModelSonyPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelSonyPhoneAccesorieMultimediaList;
            ModelAlcatelPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelAlcatelPhoneAccesorieMultimediaList;
            ModelAzusPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelAzusPhoneAccesorieMultimediaList;
            ModelHonorPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelHonorPhoneAccesorieMultimediaList;
            ModelHTCPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelHTCPhoneAccesorieMultimediaList;
            ModelLenovoPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelLenovoPhoneAccesorieMultimediaList;
            ModelMicrosoftPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelMicrosoftPhoneAccesorieMultimediaList;
            ModelLGPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelLGPhoneAccesorieMultimediaList;
            ModelMotorolaPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelMotorolaPhoneAccesorieMultimediaList;
            ModelOnePlusPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelOnePlusPhoneAccesorieMultimediaList;
            ModelWikoPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelWikoPhoneAccesorieMultimediaList;
            ModelXaomiPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelXaomiPhoneAccesorieMultimediaList;
            ModelZTEPhoneAccesorieMultimediaList = StaticListMultimediaViewModel.GetModelZTEPhoneAccesorieMultimediaList;
            ModelTVGameMultimediaList = StaticListMultimediaViewModel.GetModelTVGameMultimediaList;
            ModelSonGamequeMultimediaList = StaticListMultimediaViewModel.GetModelSonGamequeMultimediaList;
            CapacityMultimediaList = StaticListMultimediaViewModel.GetCapacityMultimediaList;
            SearchOrSaskList = StaticListViewModel.OfferOSearchList;
        }

        public Command EditCommand { get; }

        public async void OnEdit()
        {
            IsBusy = true;
            var accessToken = Settings.AccessToken;
            await _apiServices.EditMultimediaCritereAsync(ItemId, Price, SearchOrAskJob, Rubrique, Brand, Model, Capacity, accessToken);
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
                var item = await _apiServices.GetUniqueMultimediaCritereAsync(id);
                
                SearchOrAskJob = item.SearchOrAsk;
                Price = item.Price;

                //for multimedia model
                Rubrique = item.Type;
                Brand = item.Brand;
                Model = item.Model;
                Capacity = item.Capacity;

                IsRunning = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

        }
    }
}
