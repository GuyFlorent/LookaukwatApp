using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.AppartmentView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Appartment
{
    public class ApartViewModel : BaseViewModel
    {

        public IList<string> TypeList { get; }
        public IList<string> FurnitureOrNotList { get; }
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
        private string furnitureOrNot;
        public string FurnitureOrNot
        {
            get => furnitureOrNot;
            set => SetProperty(ref furnitureOrNot, value);
        }

        private int roomNumber;
        public int RoomNumber
        {
            get => roomNumber;
            set => SetProperty(ref roomNumber, value);
        }

        private int apartSurface;
        public int ApartSurface
        {
            get => apartSurface;
            set => SetProperty(ref apartSurface, value);
        }

        private bool Validate()
        {
            return !String.IsNullOrWhiteSpace(SearchOrAskJob)
                && !String.IsNullOrWhiteSpace(Type);

        }
        public ApartViewModel()
        {
            NextApartCommad = new Command(OnNextApart, Validate);
            this.PropertyChanged +=
              (_, __) => NextApartCommad.ChangeCanExecute();
            TitlePage = "Titre,description, ville, quartier...";
            TypeList = StaticListViewModel.GetListApartType;
            FurnitureOrNotList = StaticListViewModel.GetListFurnitureOrNot;
            SearchOrSaskList = StaticListViewModel.OfferOSearchList;
        }


        public Command NextApartCommad { get; }

       
        async void OnNextApart()
        {

            await Shell.Current.GoToAsync($"{nameof(ApartAddPage)}?{nameof(ApartEndViewModel.Price)}={Price}&{nameof(ApartEndViewModel.RoomNumber)}={RoomNumber}&{nameof(ApartEndViewModel.FurnitureOrNot)}={FurnitureOrNot}&{nameof(ApartEndViewModel.Type)}={Type}&{nameof(ApartEndViewModel.ApartSurface)}={ApartSurface}&{nameof(ApartEndViewModel.SearchOrAskJob)}={SearchOrAskJob}");

        }


    }
}
