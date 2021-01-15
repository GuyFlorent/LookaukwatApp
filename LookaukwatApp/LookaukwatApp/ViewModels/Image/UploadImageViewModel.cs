using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Image
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class UploadImageViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();
        private string itemId;
        public string ItemId
        {
            get => itemId;
            set => SetProperty(ref itemId, value);
        }
        public Command BackCommand { get; }
        public UploadImageViewModel()
        {
            BackCommand = new Command(OnBack);
        }
        private async void OnBack()
        {
            var acessToken = Settings.AccessToken;
            int id = Convert.ToInt32(ItemId);
            var response = await App.Current.MainPage.DisplayAlert("Alerte !!!", "Si vous retournez, votre annonce sera supprimée. Voulez vous vraiment continuer ?", "Oui", "Non");
           
            if (response)
            await _apiServices.DeleteProduct(acessToken,id);
            await Shell.Current.GoToAsync("///MainPage");
        }


    }
}
