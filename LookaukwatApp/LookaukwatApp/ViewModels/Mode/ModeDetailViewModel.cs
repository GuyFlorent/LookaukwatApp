using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.OtherServices;
using LookaukwatApp.Views.MessageView;
using LookaukwatApp.Views.ModeView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Mode
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ModeDetailViewModel : BaseViewModel
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
        //for mode model
        private string rubrique;
        private string type;
        private string brand;
        private string univers;
        private string size;
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
        public int Id { get; set; }

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

            await Shell.Current.GoToAsync($"{nameof(ModeDetailPage)}?{nameof(ModeDetailViewModel.ItemId)}={item.id}");

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

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        //for mode model
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
        public string Brand
        {
            get => brand;
            set => SetProperty(ref brand, value);
        }
        public string Univers
        {
            get => univers;
            set => SetProperty(ref univers, value);
        }
        public string Size
        {
            get => size;
            set => SetProperty(ref size, value);
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


        public ModeDetailViewModel()
        {
            TitlePage = Title;
            CallUserCommand = new Command(OncallUser);
            ItemTapped = new Command<SimilarProductViewModel>(OnItemSelected);
            ShareCommand = new Command(OnShareCommand);
            ClipBoardCommand = new Command(OnClipboard);
            SendMessageCommand = new Command(OnSendMessage);
        }

        public async void LoadItemId(string itemId)
        {
            IsRunning = true;
            try
            {
                var id = Convert.ToInt32(itemId);
                var item = await _apiServices.GetUniqueModeAsync(id);
                Id = item.id;
                Title = item.Title;
                Description = item.Description;
                Date = item.Date;
                Name = item.UserName;
                phone = item.UserPhone;
                Email = item.UserEmail;
                SearchOrAsk = item.SearchOrAsk;
                Price = item.Price;
                //for mode model
                Rubrique = item.Rubrique;
                Brand = item.Brand;
                Type = item.Type;
                Univers = item.Univers;
                Size = item.Size;
                Color = item.Color;
                State = item.State;

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
            }

        }

        private void OncallUser()
        {
            PhoneDialerViewModel.PlacePhoneCall(Phone);
        }

        private async void OnShareCommand()
        {
            var uri = "https://lookaukwat.com/Mode/ModeDetail/" + Id;
            await ShareViewModel.ShareUri(uri);
        }
        private async void OnClipboard()
        {
            var uri = "https://lookaukwat.com/Mode/ModeDetail/" + Id;
            await Clipboard.SetTextAsync(uri);

        }

        private async void OnSendMessage()
        {
            contactUserViewModel contact = new contactUserViewModel() 
            {
                NameSender = Settings.FirstName,
                EmailSender = Settings.Username,
                Category = "Mode",
                Linkshare = "https://lookaukwat.com/Mode/ModeDetail/" + Id,
                RecieverEmail = Email,
                RecieverName = Name,
                SubjectSender = "Votre article en vente sur lookaukwat me plaît"
        };
            
              await PopupNavigation.Instance.PushAsync(new ContactEmailUserPage(contact));
        }
    }
}
