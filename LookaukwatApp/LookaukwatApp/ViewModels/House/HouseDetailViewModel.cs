using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.OtherServices;
using LookaukwatApp.Views.HouseView;
using LookaukwatApp.Views.ImageView;
using LookaukwatApp.Views.MessageView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.House
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    
    public class HouseDetailViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        private string itemId;

        private int price;
        private string title;
        private string description;
        private string date;
        private string name;
        private string phone;
        private string email;
        private string searchOrAsk;
        private string street;
        private string town;
        private string lat;
        private string lon;
        //for House model
        private string rubrique;
        private string type;
        private string fabricMateriale;
        private string color;
        private string state;



        ObservableCollection<string> images = new ObservableCollection<string>();
        public ObservableCollection<string> Images { get => images; set => SetProperty(ref images, value); }

        ObservableCollection<SimilarProductViewModel> similarProduct = new ObservableCollection<SimilarProductViewModel>();
        public ObservableCollection<SimilarProductViewModel> SimilarProduct { get => similarProduct; set => SetProperty(ref similarProduct, value); }
        public Command CallUserCommand { get; set; }
        public Command ShareCommand { get; set; }
        public Command ClipBoardCommand { get; set; }
        public Command SendMessageCommand { get; set; }
        public Command TappedImageCommand { get; set; }
        //Similar item selected
        public Command<SimilarProductViewModel> ItemTapped { get; }

        private SimilarProductViewModel _selectedItem;
        public SimilarProductViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        async void OnItemSelected(SimilarProductViewModel item)
        {
            // var item = itemm as ProductForMobileViewModel;
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(HouseDetailPage)}?{nameof(HouseDetailViewModel.ItemId)}={item.id}");
        }

        public int Id { get; set; }


        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }


        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }


        public string SearchOrAsk
        {
            get => searchOrAsk;
            set => SetProperty(ref searchOrAsk, value);
        }
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public int Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Street
        {
            get => street;
            set => SetProperty(ref street, value);
        }
        public string Town
        {
            get => town;
            set => SetProperty(ref town, value);
        }
        public string Lat
        {
            get => lat;
            set => SetProperty(ref lat, value);
        }
        public string Lon
        {
            get => lon;
            set => SetProperty(ref lon, value);
        }
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                if (value != null)
                {
                    LoadItemId(value);
                }
            }
        }
        //for House model
        public string Rubrique
        {
            get => rubrique;
            set => SetProperty(ref rubrique, value);
        }
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
        public string FabricMateriale
        {
            get => fabricMateriale;
            set => SetProperty(ref fabricMateriale, value);
        }
        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }

        public string State
        {
            get => state;
            set => SetProperty(ref state, value);
        }


        public HouseDetailViewModel()
        {
            TitlePage = Title;
            CallUserCommand = new Command(OncallUser);
            ItemTapped = new Command<SimilarProductViewModel>(OnItemSelected);
            ShareCommand = new Command(OnShareCommand);
            ClipBoardCommand = new Command(OnClipboard);
            SendMessageCommand = new Command(OnSendMessage);
            TappedImageCommand = new Command<string>(OnTappedImage);
        }

        public async void OnTappedImage(string image)
        {
            var index = Images.IndexOf(image);
            Images.Move(index, 0);
            await App.Current.MainPage.Navigation.PushAsync(new DisplayFullImagePage(Images));
        }
        public async void LoadItemId(string itemId)
        {
            IsRunning = true;
            try
            {
                var id = Convert.ToInt32(itemId);
                var item = await _apiServices.GetUniqueHouseAsync(id);
                Id = item.id;
                Title = item.Title;
                Description = item.Description;
                Date = item.Date;
                Name = item.UserName;
                phone = item.UserPhone;
                Email = item.UserEmail;
                SearchOrAsk = item.SearchOrAsk;
                Price = item.Price;
                Price = item.Price;
                Town = item.Town;
                Street = item.Street;
                Lat = item.Lat;
                Lon = item.Lon;
                //for House model
                Rubrique = item.RubriqueHouse;
                FabricMateriale = item.FabricMaterialeHouse;
                Type = item.TypeHouse;
                Color = item.ColorHouse;
                State = item.StateHouse;

                var img = item.Images;
                foreach (var im in img)
                {
                    images.Add(im);
                }


                //For similar product
                foreach (var similar in item.SimilarProduct)
                {
                    SimilarProduct.Add(similar);
                }
                IsRunning = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
                IsRunning = false;
            }

        }

        private void OncallUser()
        {
            PhoneDialerViewModel.PlacePhoneCall(Phone);
        }

        private async void OnShareCommand()
        {
            var uri = "https://lookaukwat.com/House/HouseDetail/" + Id;
            await ShareViewModel.ShareUri(uri);
        }
        private async void OnClipboard()
        {
            var uri = "https://lookaukwat.com/House/HouseDetail/" + Id;
            await Clipboard.SetTextAsync(uri);

        }

        private async void OnSendMessage()
        {
            contactUserViewModel contact = new contactUserViewModel()
            {
                NameSender = Settings.FirstName,
                EmailSender = Settings.Username,
                Category = "Mode",
                Linkshare = "https://lookaukwat.com/House/HouseDetail/" + Id,
                RecieverEmail = Email,
                RecieverName = Name,
                SubjectSender = "Votre article en vente sur lookaukwat me plaît"
            };

            await PopupNavigation.Instance.PushAsync(new ContactEmailUserPage(contact));
        }
    }
}
