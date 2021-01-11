using LookaukwatApp.Helpers;
using LookaukwatApp.ViewModels.StaticList;
using LookaukwatApp.Views.AppartmentView;
using LookaukwatApp.Views.HouseView;
using LookaukwatApp.Views.JobView;
using LookaukwatApp.Views.ModeView;
using LookaukwatApp.Views.MultimediaView;
using LookaukwatApp.Views.Vehicule;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.OtherServices
{
    public class PublishAnnounceViewModel : BaseViewModel
    {
        public Command GetCategoryCommand { get; set; }
        private string category;
        public IList<string> liste { get; }
        public PublishAnnounceViewModel()
        {
            GetCategoryCommand = new Command(OnSelectCategory);
            liste = StaticListViewModel.GetCategoryList;
            TitlePage = "Déposer une annonce en 2 minutes";

        }
        public string Categori
        {
            get { return category; }
            set
            {

                SetProperty(ref category, value);

            }
        }

        async void OnSelectCategory()
        {

            var token = Settings.AccessToken;

            if (!string.IsNullOrWhiteSpace(token))
            {
                switch (Categori)
                {
                    case "Emploi":
                        await Shell.Current.GoToAsync(nameof(JobAddPage));
                        break;
                    case "Immobilier":
                        await Shell.Current.GoToAsync(nameof(ApartAddFirstPage));
                        break;
                    case "Multimédia":
                        await Shell.Current.GoToAsync(nameof(MultimediaAddFirstPage));
                        break;
                    case "Véhicule":
                        await Shell.Current.GoToAsync(nameof(VehiculeAddFirstPage));
                        break;
                    case "Mode":
                        await Shell.Current.GoToAsync(nameof(ModeAddFirstPage));
                        break;
                    case "Maison":
                        await Shell.Current.GoToAsync(nameof(HouseAddFirstPage));
                        break;

                }
            }
            else
            {
                await Shell.Current.GoToAsync("///MainPage");
                // await Shell.Current.GoToAsync(nameof(RegisterPage));
                // await Navigation.PushModalAsync(new RegisterPage());
                // Navigation.RemovePage(this);
            }


        }
    }
}
