using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.EventView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Event
{
    class EventViewModel : BaseViewModel
    {

        public IList<string> TypeSportList { get; }
        public IList<string> TypeSpectacleList { get; }
        public IList<string> RubriqueList { get; }

        ObservableCollection<string> types = new ObservableCollection<string>();
        public ObservableCollection<string> Types { get => types; set => SetProperty(ref types, value); }

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
            set
            {
                SetProperty(ref rubrique, value);

                CheckType(value);
            }
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

        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string MinDate { get => DateTime.Now.ToString("MM/dd/yyyy"); }
        private TimeSpan hour;
        public TimeSpan Hour
        {
            get => hour;
            set => SetProperty(ref hour, value);
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

        bool isSport = false;
        public bool IsSport
        {
            get { return isSport; }
            set { SetProperty(ref isSport, value); }
        }

        bool isSpectacle = false;
        public bool IsSpectacle
        {
            get { return isSpectacle; }
            set { SetProperty(ref isSpectacle, value); }
        }

        public EventViewModel()
        {
            NextEventCommad = new Command(OnNextEvent, Validate);
            this.PropertyChanged +=
              (_, __) => NextEventCommad.ChangeCanExecute();
            TitlePage = "Titre,description, ville, quartier...";
            TypeSportList = StaticListEventViewModel.GetTypeEventSportListt;
            TypeSpectacleList = StaticListEventViewModel.GetTypeEventSpectacleListt;
            RubriqueList = StaticListEventViewModel.GetRubriqueEventListt;
        }


        public Command NextEventCommad { get; }


        private void CheckType(string value)
        {
            Types.Clear();
           if (value == "Sport")
            {
                IsSport = true;
                IsSpectacle = false;
                foreach(var item in TypeSportList)
                {
                    Types.Add(item);
                }
            }
            else
            {
                IsSport = false;
                IsSpectacle = true;
                foreach (var item in TypeSpectacleList)
                {
                    Types.Add(item);
                }
            }
        }
        async void OnNextEvent()
        {

            await Shell.Current.GoToAsync($"{nameof(EventAddPage)}?{nameof(EventEndViewModel.Price)}={Price}&{nameof(EventEndViewModel.Rubrique)}={Rubrique}&{nameof(EventEndViewModel.ArtisteName)}={ArtisteName}&{nameof(EventEndViewModel.Type)}={Type}&{nameof(EventEndViewModel.Date)}={Date}&{nameof(EventEndViewModel.SearchOrAskJob)}={SearchOrAskJob}&{nameof(EventEndViewModel.Sport_Game)}={Sport_Game}&{nameof(EventEndViewModel.Hour)}={Hour}");

        }

    }
}
