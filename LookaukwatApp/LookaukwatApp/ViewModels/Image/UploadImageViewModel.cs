using LookaukwatApp.Helpers;
using LookaukwatApp.Models;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Home;
using LookaukwatApp.Views.ImageView;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            set
            { 
                SetProperty(ref itemId, value);
                if (value != null)
                    Settings.IdItem_For_Image = value;
            }
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

       // string Uri = "https://lookaukwatapi.azurewebsites.net/";
        string Uri = "https://lookaukwatapi-st5.conveyor.cloud/";
        private MediaFile _mediaFile;
        public ObservableCollection<ImageProcductModel> Items { get; set; }
        public Command AddImageGaleryCommad { get; }
        public Command AddImageTakeCommad { get; }
        public Command DeleteImageCommand { get; }
        public Command PublishCommand { get; }
        public Command TappedCommand { get; set; }

        public Command BackCommand { get; }
        public UploadImageViewModel()
        {
            BackCommand = new Command(OnBack);
            AddImageGaleryCommad = new Command(OnAddImageGalery);
            AddImageTakeCommad = new Command(OnImageTake);
            PublishCommand = new Command(OnPublish);
            TappedCommand = new Command<string>(OnTappedImage);
            Items = new ObservableCollection<ImageProcductModel>();
            DeleteImageCommand = new Command(async (e) =>
            {
                var itm = e as ImageProcductModel;
                var response = await Shell.Current.DisplayAlert("Alerte !!!", "Voulez vous vraiment suprimer cette image ?", "Oui", "Non");

                if (response)
                {
                    IsBusy = true;
                    var AccessToken = Settings.AccessToken;
                  
                    Items.Remove(itm);
                    IsBusy = false;
                    bool resp = await _apiServices.DeleteImageAsync(AccessToken, itm.id);
                   // if (resp)
                        
                }
            });
        }


        public async void OnTappedImage(string image)
        {
            ObservableCollection<string> images = new ObservableCollection<string> {image };
            await App.Current.MainPage.Navigation.PushAsync(new DisplayFullImagePage(images));
        }


        private async void OnBack()
        {
            var acessToken = Settings.AccessToken;
            int id;
            if (ItemId == null)
            {
               
                id = Convert.ToInt32(Settings.IdItem_For_Image);
            }
            else
            {
                id = Convert.ToInt32(ItemId);
            }
             
            var response = await Shell.Current.DisplayAlert("Alerte !!!", "Si vous retournez, votre annonce sera supprimée. Voulez vous vraiment continuer ?", "Oui", "Non");
           
            if (response)
            await _apiServices.DeleteProduct(acessToken,id);
            await Shell.Current.GoToAsync("///MainPage");
        }

        private bool validateImage()
        {
          
                return IsBusy == false;
            
           
        }
        private async void OnPublish()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsBusy = false;
                return;
            }


            if (IsBusy)
            {
                await Shell.Current.DisplayAlert("Alerte", "veuillez patienter la fin du téléchargement de l'image avant de valider", "ok");
            }
            else
            {
                IsRunning = true;
                var content = new MultipartFormDataContent();


                HttpClient client;

                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };

                client = new HttpClient(httpClientHandler);
               
                string id;
                if (ItemId == null)
                {
                    id = Settings.IdItem_For_Image;
                }
                else
                {
                    id = ItemId;
                }

                await client.PostAsync(Uri + "api/Product/UpdateProductImage/?id=" + id, content);
                IsRunning = false;


                await Shell.Current.DisplayAlert("Alerte", "Votre annonce a été publiée avec succès. Lookaukwat vous remercie de votre confiance", "Ok");
                await Shell.Current.GoToAsync("///MainPage");
                if (string.IsNullOrWhiteSpace(Settings.Reviews_GooglePlay))
                {
                    var respon = await Shell.Current.DisplayAlert("Votre avis nous intéresse !", "Donnez votre avis en 1 minute", "Oui", "Non");
                    if (respon)
                    {
                       Settings.Reviews_GooglePlay = "Ok";
                        string uri = "https://play.google.com/store/apps/details?id=com.lookaukwat.lookaukwatapp";
                        await Browser.OpenAsync(uri);
                    }
                   
                }
               
               

                
            }
        }

        private async void OnAddImageGalery()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("No pick photo", ":(No Pick photo available.", "ok");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 40
            });
            if (_mediaFile == null)
                return;
            IsBusy = true;

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsBusy = false;
                return;
            }


            var Image = await UploadPhoto_Click();
            if (Image != null)
            {
                IsBusy = false;
                Items.Add(Image);
            }
            else
            {
                IsBusy = false;
                Message = "Désolé une erreur s'est produite !";
            }

                
        }


        private async void OnImageTake()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("No pick photo", ":(No Pick photo available.", "ok");
                return;
            }

            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 40,
                Directory = "Sample",
                Name = "MyImage.jpg"
            }
                );
            if (_mediaFile == null)
                return;
            IsBusy = true;

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsBusy = false;
                return;
            }


            var Image = await UploadPhoto_Click();
            if (Image != null)
            {
                IsBusy = false;
                Items.Add(Image);
            }
            else
            {
                IsBusy = false;
                Message = "Désolé une erreur s'est produite !";
            }
        }

        private async Task<ImageProcductModel> UploadPhoto_Click()
        {
            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(_mediaFile.GetStream()),
                "\"file\"",
                $"\"{_mediaFile.Path}\"");

            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            string productId;
            if (ItemId == null)
            {
                 productId = Settings.IdItem_For_Image;
            }
            else
            {
                productId = ItemId;
            }
            

            var uploadServiceBaseAdress = Uri + "api/Product/UploadImages/?id=" + productId;
            var httpResponseMessage = await client.PostAsync(uploadServiceBaseAdress, content);
            var jwt = await httpResponseMessage.Content.ReadAsStringAsync();
            var Image = JsonConvert.DeserializeObject<ImageProcductModel>(jwt);


            return Image;
        }

    }
}
