using LookaukwatApp.ViewModels.Image;
using LookaukwatApp.Views.ImageView;
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
            set => SetProperty(ref id, value);
        }

        private string category;
        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public Command EditImageCommad { get; }
        public Command EditCritereCommad { get; }
        public Command EditDescriptionTileTownCommad { get; }

        public EditViewModel()
        {
            TitlePage = "Modifier l'annonce";
            EditImageCommad = new Command(OnEditImage);
        }

        public async void OnEditImage()
        {
            await Shell.Current.GoToAsync($"{nameof(EditImagePage)}?{nameof(EditImagesViewModel.ItemId)}={Id}");

        }
    }
}
