using LookaukwatApp.Helpers;
using LookaukwatApp.Models;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Image;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.ImageView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Mode
{
    //[QueryProperty(nameof(Price), nameof(Price))]
    //[QueryProperty(nameof(Type), nameof(Type))]
    //[QueryProperty(nameof(Rubrique), nameof(Rubrique))]
    //[QueryProperty(nameof(Brand), nameof(Brand))]
    //[QueryProperty(nameof(SearchOrAskJob), nameof(SearchOrAskJob))]
    //[QueryProperty(nameof(Univers), nameof(Univers))]
    //[QueryProperty(nameof(Size), nameof(Size))]
    //[QueryProperty(nameof(State), nameof(State))]
    //[QueryProperty(nameof(Color), nameof(Color))]
    [QueryProperty(nameof(Json), nameof(Json))]
    public class ModeEndViewModel : BaseViewModel
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

        private string json;
        public string Json
        {
            get => json;
            set
            { 
                SetProperty(ref json, value);

                UpdateParam();
            }
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

        private string brand;
        public string Brand
        {
            get => brand;
            set => SetProperty(ref brand, Uri.UnescapeDataString(value));
        }

        private string univers;
        public string Univers
        {
            get => univers;
            set => SetProperty(ref univers, Uri.UnescapeDataString(value));
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

        public ModeEndViewModel()
        {
            PostModeCommad = new Command(OnPostMode, ValidateLoging);
            TitlePage = "Titre,description, ville, quartier...";
            TownList = StaticListViewModel.GetTownCameroonList;
            this.PropertyChanged +=
               (_, __) => PostModeCommad.ChangeCanExecute();
        }

        public Command PostModeCommad { get; }


        async void OnPostMode()
        {
            IsRunning = true;
            int price = Convert.ToInt32(Price);

            var accessToken = Settings.AccessToken;
            var ProductId = await _apiServices.ModePostAsync(accessToken, Title, Description, Town, Street, price, SearchOrAskJob, Rubrique, Brand, Color, Type, Size, State, Univers);

            if (ProductId != 0)
            {
                IsRunning = false;
                Id = ProductId;
                await Shell.Current.GoToAsync($"{nameof(UploadImagePage)}?{nameof(UploadImageViewModel.ItemId)}={ProductId}");

            }
        }


        private  void UpdateParam()
        {
            try
            {
               var data = HttpUtility.UrlDecode(Json);

                var model = JsonConvert.DeserializeObject<ModeModel>(data);
            Price = model.Price.ToString();
            SearchOrAskJob = model.SearchOrAskJob;
            Rubrique = model.RubriqueMode;
            Brand = model.BrandMode;
            Type = model.TypeMode;
            Color = model.ColorMode;
            Size = model.SizeMode;
            State = State;
            Univers = Univers;
            } catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
