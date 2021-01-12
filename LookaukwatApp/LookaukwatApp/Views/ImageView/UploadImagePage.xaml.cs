using LookaukwatApp.ViewModels.Image;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.ImageView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadImagePage : ContentPage
    {
        private MediaFile _mediaFile;
        string Uri = "https://192.168.1.66:45455/";
        public UploadImagePage()
        {
            InitializeComponent();
            BindingContext = new UploadImageViewModel();
        }



        private async void PickPhoto_Click(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No pick photo", ":(No Pick photo available.", "ok");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 40
            });
            if (_mediaFile == null)
                return;
            StackFirstActivity.IsVisible = true;
            FirstActivity.IsRunning = true;
            //LocalPathLabel.Text = _mediaFile.Path;
            FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            var isSuccess = await UploadPhoto_Click();
            if (isSuccess)
            {
                StackFirstActivity.IsVisible = false;
                FirstActivity.IsRunning = false;
                FirstImage.IsVisible = false;
                response.Text = "Ajoutée avec success";
            }
            else
            {
                StackFirstActivity.IsVisible = false;
                FirstActivity.IsRunning = false;
                response.Text = "Désolé une erreur s'est produite !";
            }
        }

        private async void PickPhoto_Click1(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No pick photo", ":(No Pick photo available.", "ok");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 40
            });
            if (_mediaFile == null)
                return;

            StackSecondActivity.IsVisible = true;
            SecondActivity.IsRunning = true;
            //LocalPathLabel.Text = _mediaFile.Path;
            FileImage1.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            var isSuccess = await UploadPhoto_Click();
            if (isSuccess)
            {
                StackSecondActivity.IsVisible = false;
                SecondActivity.IsRunning = false;
                SecondImage.IsVisible = false;
                response1.Text = "Ajoutée avec success";
            }
            else
            {
                StackSecondActivity.IsVisible = false;
                SecondActivity.IsRunning = false;
                response1.Text = "Désolé une erreur s'est produite !";
            }
        }

        private async void PickPhoto_Click2(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No pick photo", ":(No Pick photo available.", "ok");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 40
            });
            if (_mediaFile == null)
                return;
            StackThirdActivity.IsVisible = true;
            ThirdActivity.IsRunning = true;
            //LocalPathLabel.Text = _mediaFile.Path;
            FileImage2.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            var isSuccess = await UploadPhoto_Click();
            if (isSuccess)
            {
                StackThirdActivity.IsVisible = false;
                ThirdActivity.IsRunning = false;
                ThirdImage.IsVisible = false;
                response2.Text = "Ajoutée avec success";
            }
            else
            {
                StackThirdActivity.IsVisible = false;
                ThirdActivity.IsRunning = false;
                response2.Text = "Désolé une erreur s'est produite !";
            }
        }

        private async void TakePhoto_Click(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No pick photo", ":(No Pick photo available.", "ok");
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
            StackFirstActivity.IsVisible = true;
            FirstActivity.IsRunning = true;
            //LocalPathLabel.Text = _mediaFile.Path;
            FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            var isSuccess = await UploadPhoto_Click();
            if (isSuccess)
            {
                StackFirstActivity.IsVisible = false;
                FirstActivity.IsRunning = false;
                FirstImage.IsVisible = false;
                response.Text = "Ajoutée avec success";
            }
            else
            {
                StackFirstActivity.IsVisible = false;
                FirstActivity.IsRunning = false;
                response.Text = "Désolé une erreur s'est produite !";
            }
        }

        private async void TakePhoto_Click1(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No pick photo", ":(No Pick photo available.", "ok");
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
            StackSecondActivity.IsVisible = true;
            SecondActivity.IsRunning = true;
            //LocalPathLabel.Text = _mediaFile.Path;
            FileImage1.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });
            var isSuccess = await UploadPhoto_Click();
            if (isSuccess)
            {
                StackSecondActivity.IsVisible = false;
                SecondActivity.IsRunning = false;
                SecondImage.IsVisible = false;
                response1.Text = "Ajoutée avec success";
            }
            else
            {
                StackSecondActivity.IsVisible = false;
                SecondActivity.IsRunning = false;
                response1.Text = "Désolé une erreur s'est produite !";
            }
        }

        private async void TakePhoto_Click2(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No pick photo", ":(No Pick photo available.", "ok");
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
            StackThirdActivity.IsVisible = true;
            ThirdActivity.IsRunning = true;
            //LocalPathLabel.Text = _mediaFile.Path;
            FileImage2.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            var isSuccess = await UploadPhoto_Click();
            if (isSuccess)
            {
                StackThirdActivity.IsVisible = false;
                ThirdActivity.IsRunning = false;
                ThirdImage.IsVisible = false;
                response2.Text = "Ajoutée avec success";
            }
            else
            {
                StackThirdActivity.IsVisible = false;
                ThirdActivity.IsRunning = false;
                response2.Text = "Désolé une erreur s'est produite !";
            }
        }

        private async Task<bool> UploadPhoto_Click()
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
            string productId = ProductId.Text;

            var uploadServiceBaseAdress = Uri + "api/Product/UploadImages/?id=" + productId;
            // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var httpResponseMessage = await client.PostAsync(uploadServiceBaseAdress, content);
            // RemotePathLabel.Text = await httpResponseMessage.Content.ReadAsStringAsync();
            return httpResponseMessage.IsSuccessStatusCode;
        }

        private async void Validate_Click(object o, EventArgs e)
        {
            StackValidate.IsVisible = true;
            ActivityValidate.IsRunning = true;
            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(_mediaFile.GetStream()),
                "\"file\"",
                $"\"{_mediaFile.Path}\"");
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            string id = ProductId.Text;
            // string Uri = "https://192.168.1.66:45460/";
            await client.PostAsync(Uri + "api/Product/UpdateProductImage/?id=" + id, content);
            StackValidate.IsVisible = false;
            ActivityValidate.IsRunning = false;
            await DisplayAlert("Alerte", "Votre annonce a été publié avec succèss. Merci de votre confiance", "Ok");

            await Shell.Current.GoToAsync("///MainPage");
        }

    }
}