using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.EventView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Event
{
    class EventViewModel : BaseViewModel
    {

        public IList<string> TypeSportList { get; }
        public IList<string> TypeSpectacleList { get; }
        public IList<string> RubriqueList { get; }
       

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
                
            }
        }

        private string rubrique;
        public string Rubrique
        {
            get => rubrique;
            set => SetProperty(ref rubrique, value);
        }

        private string artisteName;
        public string ArtisteName
        {
            get => artisteName;
            set => SetProperty(ref artisteName, value);
        }

        private string sport_Game;
        public string Sport_Game
        {
            get => sport_Game;
            set => SetProperty(ref sport_Game, value);
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        private bool Validate()
        {
            return !String.IsNullOrWhiteSpace(SearchOrAskJob)
                && !String.IsNullOrWhiteSpace(Type);

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


        public EventViewModel()
        {
            NextEventCommad = new Command(OnNextApart, Validate);
            this.PropertyChanged +=
              (_, __) => NextEventCommad.ChangeCanExecute();
            TitlePage = "Titre,description, ville, quartier...";
            TypeSportList = StaticListEventViewModel.GetTypeEventSportListt;
            TypeSpectacleList = StaticListEventViewModel.GetTypeEventSpectacleListt;
            RubriqueList = StaticListEventViewModel.GetRubriqueEventListt;
        }


        public Command NextEventCommad { get; }


        async void OnNextApart()
        {

            await Shell.Current.GoToAsync($"{nameof(EventAddPage)}?{nameof(EventEndViewModel.Price)}={Price}&{nameof(EventEndViewModel.Rubrique)}={Rubrique}&{nameof(EventEndViewModel.ArtisteName)}={ArtisteName}&{nameof(EventEndViewModel.Type)}={Type}&{nameof(EventEndViewModel.Date)}={Date}&{nameof(EventEndViewModel.SearchOrAskJob)}={SearchOrAskJob}&{nameof(EventEndViewModel.Sport_Game)}={Sport_Game}");

        }


    }
}
