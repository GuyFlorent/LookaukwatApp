using LookaukwatApp.Helpers;
using LookaukwatApp.ViewModels.Appartment;
using LookaukwatApp.ViewModels.House;
using LookaukwatApp.ViewModels.Image;
using LookaukwatApp.ViewModels.Job;
using LookaukwatApp.Views.AppartmentView;
using LookaukwatApp.Views.EditView;
using LookaukwatApp.Views.HouseView;
using LookaukwatApp.Views.ImageView;
using LookaukwatApp.Views.JobView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Edit
{
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(Category), nameof(Category))]
    public class EditViewModel : BaseViewModel
    {
        private string id;
        public string Id
        {
            get => id;
            set 
            {
                SetProperty(ref id, value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Settings.ItemUpDateId = value;
                }
            }
        }

        private string category;
        public string Category
        {
            get => category;
            set
            { 
                SetProperty(ref category, value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Settings.CategoryUpDateId = value;
                }
            }
        }

        public Command EditImageCommad { get; }
        public Command EditCritereCommad { get; }
        public Command EditDescriptionTileTownCommad { get; }

        public EditViewModel()
        {
            TitlePage = "Modifier l'annonce";
            EditImageCommad = new Command(OnEditImage);
            EditCritereCommad = new Command(OnEditCritere);
            EditDescriptionTileTownCommad = new Command(OnEditDescriptionTileTown);
        }

        public async void OnEditImage()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
               Id =  Settings.ItemUpDateId ;
            }
            await Shell.Current.GoToAsync($"{nameof(EditImagePage)}?{nameof(EditImagesViewModel.ItemId)}={Id}");

        }

        private async void OnEditCritere()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = Settings.ItemUpDateId;
            }
            if (string.IsNullOrWhiteSpace(Category))
            {
                Category = Settings.CategoryUpDateId;
            }
            try
            {
                switch (Category)
                {
                    case "Emploi":
                        await Shell.Current.GoToAsync($"{nameof(EditJobCriterePage)}?{nameof(JobEditCritereViewModel.ItemId)}={Id}");
                        break;
                    case "Immobilier":

                        await Shell.Current.GoToAsync($"{nameof(ApartEditCriterePage)}?{nameof(ApartEditCritereViewModel.ItemId)}={Id}");

                        break;
                    case "Maison":
                         await Shell.Current.GoToAsync($"{nameof(HouseEditCriterePage)}?{nameof(HouseEditCritereViewModel.ItemId)}={Id}");

                        break;
                    case "Mode":
                        // await Shell.Current.GoToAsync($"{nameof(ApartEditCriterePage)}?{nameof(ApartEditCritereViewModel.ItemId)}={Id}");

                        break;
                    case "Multimedia":
                        // await Shell.Current.GoToAsync($"{nameof(ApartEditCriterePage)}?{nameof(ApartEditCritereViewModel.ItemId)}={Id}");

                        break;
                    case "Vehicule":
                        //  await Shell.Current.GoToAsync($"{nameof(ApartEditCriterePage)}?{nameof(ApartEditCritereViewModel.ItemId)}={Id}");

                        break;
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        private async void OnEditDescriptionTileTown()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = Settings.ItemUpDateId;
            }
            await Shell.Current.GoToAsync($"{nameof(EditDescrip_Title_Town_StreetPage)}?{nameof(EditDescrip_Title_Town_StreetViewModel.ItemId)}={Id}");
        }



    }
}
