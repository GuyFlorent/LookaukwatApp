﻿using LookaukwatApp.Helpers;
using LookaukwatApp.Models;
using LookaukwatApp.Services;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        string Uri = "https://192.168.1.66:45455/";
        private MediaFile _mediaFile;
        public ObservableCollection<ImageProcductModel> Items { get; set; }
        public Command AddImageGaleryCommad { get; }
        public Command AddImageTakeCommad { get; }
        public Command DeleteImageCommand { get; }
        public Command PublishCommand { get; }


        public Command BackCommand { get; }
        public UploadImageViewModel()
        {
            BackCommand = new Command(OnBack);
            AddImageGaleryCommad = new Command(OnAddImageGalery);
            AddImageTakeCommad = new Command(OnImageTake);
            PublishCommand = new Command(OnPublish);
            Items = new ObservableCollection<ImageProcductModel>();
            DeleteImageCommand = new Command(async (e) =>
            {
                var itm = e as ImageProcductModel;
                var response = await Shell.Current.DisplayAlert("Alerte !!!", "Voulez vous vraiment suprimer cette image ?", "Oui", "Non");

                if (response)
                {
                    IsBusy = true;
                    var AccessToken = Settings.AccessToken;

                    bool resp = await _apiServices.DeleteImageAsync(AccessToken, itm.id);
                    if (resp)
                        IsBusy = false;
                    Items.Remove(itm);
                }
            });
        }
        private async void OnBack()
        {
            var acessToken = Settings.AccessToken;
            int id = Convert.ToInt32(ItemId);
            var response = await Shell.Current.DisplayAlert("Alerte !!!", "Si vous retournez, votre annonce sera supprimée. Voulez vous vraiment continuer ?", "Oui", "Non");
           
            if (response)
            await _apiServices.DeleteProduct(acessToken,id);
            await Shell.Current.GoToAsync("///MainPage");
        }

        private async void OnPublish()
        {
            IsRunning = true;
            var content = new MultipartFormDataContent();

           
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            string id = ItemId;

            await client.PostAsync(Uri + "api/Product/UpdateProductImage/?id=" + id, content);
            IsRunning = false;
            await Shell.Current.DisplayAlert("Alerte", "Votre annonce a été publié avec succès. Merci de votre confiance", "Ok");

            await Shell.Current.GoToAsync("///MainPage");
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
            string productId = ItemId;

            var uploadServiceBaseAdress = Uri + "api/Product/UploadImages/?id=" + productId;
            var httpResponseMessage = await client.PostAsync(uploadServiceBaseAdress, content);
            var jwt = await httpResponseMessage.Content.ReadAsStringAsync();
            var Image = JsonConvert.DeserializeObject<ImageProcductModel>(jwt);


            return Image;
        }

    }
}
