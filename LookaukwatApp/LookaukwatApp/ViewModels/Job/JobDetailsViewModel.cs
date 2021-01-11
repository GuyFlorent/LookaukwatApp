using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.OtherServices;
using LookaukwatApp.Views.JobView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Job
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class JobDetailsViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        private string itemId;
        private int price;
        private string title;
        private string description;
        private string date;
        private string contratType;
        private string activitySector;
        private string searchOrAsk;
        private string name;
        private string phone;
        private string email;
        ObservableCollection<string> images = new ObservableCollection<string>();
        public ObservableCollection<string> Images { get => images; set => SetProperty(ref images, value); }

        ObservableCollection<SimilarProductViewModel> similarProduct = new ObservableCollection<SimilarProductViewModel>();
        public ObservableCollection<SimilarProductViewModel> SimilarProduct { get => similarProduct; set => SetProperty(ref similarProduct, value); }
        public Command CallUserCommand { get; set; }
        public Command ShareCommand { get; set; }
        public Command ClipBoardCommand { get; set; }

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

            await Shell.Current.GoToAsync($"{nameof(JobDetailPage)}?{nameof(JobDetailsViewModel.ItemId)}={item.id}");

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

        public string ContratType
        {
            get => contratType;
            set => SetProperty(ref contratType, value);
        }
        public string ActivitySector
        {
            get => activitySector;
            set => SetProperty(ref activitySector, value);
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

        public JobDetailsViewModel()
        {
            TitlePage = Title;
            CallUserCommand = new Command(OncallUser);
            ItemTapped = new Command<SimilarProductViewModel>(OnItemSelected);
            ShareCommand = new Command(OnShareCommand);
            ClipBoardCommand = new Command(OnClipboard);
        }

        public async void LoadItemId(string itemId)
        {
            IsRunning = true;
            try
            {
                var id = Convert.ToInt32(itemId);
                var item = await _apiServices.GetUniqueJobAsync(id);
                Id = item.id;
                Title = item.Title;
                Description = item.Description;
                Date = item.Date;
                ActivitySector = item.ActivitySector;
                ContratType = item.TypeContract;
                SearchOrAsk = item.SearchOrAskJob;
                Name = item.User.FirstName;
                phone = item.User.PhoneNumber;
                Email = item.User.Email;
                var img = item.Images.Select(s => s.ImageMobile);
                foreach (var im in img)
                {
                    images.Add(im);
                }
                Price = item.Price;

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
            var uri = "https://lookaukwat.azurewebsites.net/Job/JobDetail/" + Id;
            await ShareViewModel.ShareUri(uri);
        }

        private async void OnClipboard()
        {
            var uri = "https://lookaukwat.azurewebsites.net/Job/JobDetail/" + Id;
            await Clipboard.SetTextAsync(uri);

        }
    }
}
