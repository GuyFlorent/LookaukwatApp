using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.OtherServices;
using LookaukwatApp.Views.AppartmentView;
using LookaukwatApp.Views.ImageView;
using LookaukwatApp.Views.MessageView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Appartment
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
   
    public class ApartDetailViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();
        ApartModelViewModel item;

        private int id;
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
        private string priceConvert;
        //for Apart model
        private string apartSurface;
        private string type;
        private string roomNumber;
        private string furnitureOrNot;



        ObservableCollection<string> images = new ObservableCollection<string>();
        public ObservableCollection<string> Images { get => images; set => SetProperty(ref images, value); }

        ObservableCollection<SimilarProductViewModel> similarProduct = new ObservableCollection<SimilarProductViewModel>();
        public ObservableCollection<SimilarProductViewModel> SimilarProduct { get => similarProduct; set => SetProperty(ref similarProduct, value); }
        public Command CallUserCommand { get; set; }
        public Command ShareCommand { get; set; }
        public Command ClipBoardCommand { get; set; }
        public Command SendMessageCommand { get; set; }
        public Command TappedImageCommand { get; set; }
        public Command SignalCommand { get; set; }
        public Command NotFavoriteCommand { get; set; }
        public Command FavoriteCommand { get; set; }
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
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
            await Shell.Current.GoToAsync($"{nameof(ApartDetailPage)}?{nameof(ApartDetailViewModel.ItemId)}={item.id}");
        }
       
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

        private string blackHeart = "heart_black";
        public string BlackHeart
        {
            get => blackHeart;
            set => SetProperty(ref blackHeart, value);
        }

        private string redHeart = "heart_red";
        public string RedHeart
        {
            get => redHeart;
            set => SetProperty(ref redHeart, value);
        }
        public string PriceConvert
        {
            get => priceConvert;
            set => SetProperty(ref priceConvert, value);
        }

        bool isLookaukwat = false;
        public bool IsLookaukwat
        {
            get { return isLookaukwat; }
            set { SetProperty(ref isLookaukwat, value); }
        }
        private int stock;
        public int Stock
        {
            get { return stock; }
            set { SetProperty(ref stock, value); }
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
        //for Apart model
        public string ApartSurface
        {
            get => apartSurface;
            set => SetProperty(ref apartSurface, value);
        }
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
        public string RoomNumber
        {
            get => roomNumber;
            set => SetProperty(ref roomNumber, value);
        }
        public string FurnitureOrNot
        {
            get => furnitureOrNot;
            set => SetProperty(ref furnitureOrNot, value);
        }



        public ApartDetailViewModel()
        {
            TitlePage = Title;
            item = new ApartModelViewModel();
            CallUserCommand = new Command(OncallUser);
            ItemTapped = new Command<SimilarProductViewModel>(OnItemSelected);
            ShareCommand = new Command(OnShareCommand);
            ClipBoardCommand = new Command(OnClipboard);
            SendMessageCommand = new Command(OnSendMessage);
            TappedImageCommand = new Command<string>(OnTappedImage);
            SignalCommand = new Command(OnSignal);
            NotFavoriteCommand = new Command<SimilarProductViewModel>(OnFavorite);
            FavoriteCommand = new Command(OnFavorite);
        }


        public async void OnTappedImage(string image)
        {
            var index = Images.IndexOf(image);
            Images.Move(index, 0);
            await App.Current.MainPage.Navigation.PushAsync(new DisplayFullImagePage(Images));
        }
        public async void LoadItemId(string itemId)
        {
            IsBusy = false;
            IsRunning = true;

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");
                IsBusy = false;
                IsRunning = false;
                return;
            }

            try
            {
                var id = Convert.ToInt32(itemId);
                item = await _apiServices.GetUniqueApartAsync(id);
                Id = item.id;
                Title = item.Title;
                Description = item.Description;
                Date = item.Date;
                Name = item.UserName;
                phone = item.UserPhone;
                Email = item.UserEmail;
                SearchOrAsk = item.SearchOrAsk;
                Price = item.Price;
                PriceConvert = item.PriceConvert;
                Town = item.Town;
                Street = item.Street;
                Lat = item.Lat;
                Lon = item.Lon;
                IsLookaukwat = item.IsLookaukwat;
                Stock = item.Stock;
                //for Apart model
                FurnitureOrNot = item.FurnitureOrNot;
                ApartSurface = item.ApartSurface.ToString();
                Type = item.Type;
                RoomNumber = item.RoomNumber.ToString();

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
                IsBusy = true;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
                IsRunning = false;
            }

        }


        private async void OnFavorite(SimilarProductViewModel item)
        {
            ProductForMobileViewModel Itemproduct = new ProductForMobileViewModel()
            {
                id = item.id,
                Category = item.Category,
                Price = item.Price,
                Town = item.Town,
                Title = item.Title,
                NumberImages = item.NumberImages,
                Image = item.Image,
                DateLetter = item.Date,

            };
            bool response = CheckFavorite.IsFabvorite(Itemproduct);
            if (response)
            {
                item.BlackHeart = "heart_red";
                item.RedHeart = "heart_red";
                await Shell.Current.DisplayAlert("Ajoutée aux favoris !", "Vous pouvez contacter l'annonceur à tout moment dans vos favoris pour lui montrer votre intérêt", "ok");
            }
            else
            {
                item.BlackHeart = "heart_black";
                item.RedHeart = "heart_black";
            }
        }



        private async void OnFavorite()
        {
            ProductForMobileViewModel Itemproduct = new ProductForMobileViewModel()
            {
                id = item.id,
                Category = "Maison",
                Price = item.Price,
                Town = item.Town,
                Title = item.Title,
                NumberImages = item.NumberImages,
                Image = item.Images.First(),
                DateLetter = item.Date,

            };
            bool response = CheckFavorite.IsFabvorite(Itemproduct);
            if (response)
            {
                BlackHeart = "heart_red";
                RedHeart = "heart_red";
                await Shell.Current.DisplayAlert("Ajoutée aux favoris !", "Vous pouvez contacter l'annonceur à tout moment dans vos favoris pour lui montrer votre intérêt", "ok");
            }
            else
            {
                BlackHeart = "heart_black";
                RedHeart = "heart_black";
            }
        }

        private async void OncallUser()
        {
            string text = "- Ne jamais envoyer de l'argent sans avoir vu et juger le(a) " + Type + ", " + Environment.NewLine + Environment.NewLine +
                "- Toujours exiger une facture avec la pièce d'identité jointe." + Environment.NewLine + Environment.NewLine +
                "Merci pour la confiance.";
            await Shell.Current.DisplayAlert("Attention à l'arnaque !", text, "ok");
            PhoneDialerViewModel.PlacePhoneCall(Phone);
        }

        private async void OnShareCommand()
        {
            var uri = "https://lookaukwat.com/ApartmentRental/ApartDetail/" + Id;
            await ShareViewModel.ShareUri(uri);
        }
        private async void OnClipboard()
        {
            await Shell.Current.DisplayAlert("Alerte", "Copier dans le papier-presse", "Ok");
            var uri = "https://lookaukwat.com/ApartmentRental/ApartDetail/" + Id;
            await Clipboard.SetTextAsync(uri);

        }

        private async void OnSendMessage()
        {
            contactUserViewModel contact = new contactUserViewModel()
            {
                NameSender = Settings.FirstName,
                EmailSender = Settings.Username,
                Category = "Immobilier",
                Linkshare = "https://lookaukwat.com/ApartmentRental/ApartDetail/" + Id,
                RecieverEmail = Email,
                RecieverName = Name,
                SubjectSender = "Votre article en vente sur lookaukwat me plaît"
            };

            await PopupNavigation.Instance.PushAsync(new ContactEmailUserPage(contact));
        }

        private async void OnSignal()
        {
            contactUserViewModel contact = new contactUserViewModel()
            {
                NameSender = Settings.FirstName,
                EmailSender = Settings.Username,
                Category = "Immobilier",
                Linkshare = "https://lookaukwat.com/ApartmentRental/ApartDetail/" + Id,
                RecieverEmail = "contact@lookaukwat.com",
                RecieverName = "Staff lookaukwat",
                SubjectSender = "Votre article en vente sur lookaukwat me plaît"
            };

            await App.Current.MainPage.Navigation.PushAsync(new SignalAnnoucePage(contact));
        }
    }
}
