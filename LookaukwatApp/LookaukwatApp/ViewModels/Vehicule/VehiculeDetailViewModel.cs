using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.OtherServices;
using LookaukwatApp.Views.ImageView;
using LookaukwatApp.Views.MessageView;
using LookaukwatApp.Views.Vehicule;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Vehicule
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
   
    public class VehiculeDetailViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();
        VehiculeModelViewModel item;
        private string itemId;
        private int id;
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
        //for House model
        private string rubriqueVehicule;
        private string brandVehicule;
        private string modelVehicule;
        private string typeVehicule;
        private string petrolVehicule;
        private string yearVehicule;
        private string mileageVehicule;
        private string numberOfDoorVehicule;
        private string colorVehicule;
        private string stateVehicule;
        private string gearBoxVehicule;



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


            await Shell.Current.GoToAsync($"{nameof(VehiculeDetailPage)}?{nameof(VehiculeDetailViewModel.ItemId)}={item.id}");


        }
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
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
        public string PriceConvert
        {
            get => priceConvert;
            set => SetProperty(ref priceConvert, value);
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

        public string Model
        {
            get => modelVehicule;
            set => SetProperty(ref modelVehicule, value);
        }

        public string Rubrique
        {
            get => rubriqueVehicule;
            set => SetProperty(ref rubriqueVehicule, value);
        }

        public string Brand
        {
            get => brandVehicule;
            set => SetProperty(ref brandVehicule, value);
        }
        public string Type
        {
            get => typeVehicule;
            set => SetProperty(ref typeVehicule, value);
        }
        public string Petrol
        {
            get => petrolVehicule;
            set => SetProperty(ref petrolVehicule, value);
        }
        public string Year
        {
            get => yearVehicule;
            set => SetProperty(ref yearVehicule, value);
        }

        public string Mileage
        {
            get => mileageVehicule;
            set => SetProperty(ref mileageVehicule, value);
        }
        public string NumberOfDoor
        {
            get => numberOfDoorVehicule;
            set => SetProperty(ref numberOfDoorVehicule, value);
        }
        public string Color
        {
            get => colorVehicule;
            set => SetProperty(ref colorVehicule, value);
        }
        public string State
        {
            get => stateVehicule;
            set => SetProperty(ref stateVehicule, value);
        }

        public string GearBox
        {
            get => gearBoxVehicule;
            set => SetProperty(ref gearBoxVehicule, value);
        }


        public VehiculeDetailViewModel()
        {
            TitlePage = Title;
            item = new VehiculeModelViewModel();
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
            
            try
            {
                var id = Convert.ToInt32(itemId);
                 item = await _apiServices.GetUniqueVehiculeAsync(id);
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
                //for House model
                Rubrique = item.RubriqueVehicule;
                Brand = item.BrandVehicule;
                Type = item.TypeVehicule;
                Color = item.ColorVehicule;
                Petrol = item.PetrolVehicule;
                Year = item.YearVehicule;
                Mileage = item.MileageVehicule;
                NumberOfDoor = item.NumberOfDoorVehicule;
                GearBox = item.GearBoxVehicule;
                Model = item.ModelVehicule;
                State = item.StateVehicule;


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
                Category = "Vehicule",
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

        private void OncallUser()
        {
            PhoneDialerViewModel.PlacePhoneCall(Phone);
        }
        private async void OnShareCommand()
        {
            var uri = "https://lookaukwat.com/Vehicule/VehiculeDetail/" + Id;
            await ShareViewModel.ShareUri(uri);
        }
        private async void OnClipboard()
        {
            await Shell.Current.DisplayAlert("Alerte", "Copier dans le papier-presse", "Ok");
            var uri = "https://lookaukwat.com/Vehicule/VehiculeDetail/" + Id;
            await Clipboard.SetTextAsync(uri);

        }

        private async void OnSendMessage()
        {
            contactUserViewModel contact = new contactUserViewModel()
            {
                NameSender = Settings.FirstName,
                EmailSender = Settings.Username,
                Category = "Vehicule",
                Linkshare = "https://lookaukwat.com/Vehicule/VehiculeDetail/" + Id,
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
                Category = "Vehicule",
                Linkshare = "https://lookaukwat.com/Vehicule/VehiculeDetail/" + Id,
                RecieverEmail = "contact@lookaukwat.com",
                RecieverName = "Staff lookaukwat",
                SubjectSender = "Votre article en vente sur lookaukwat me plaît"
            };

            await App.Current.MainPage.Navigation.PushAsync(new SignalAnnoucePage(contact));
        }
    }
}
