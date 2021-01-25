using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.StaticList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Edit
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditDescrip_Title_Town_StreetViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        public IList<string> TownList { get; }
        private string itemId;
        private string title;
        private string description;
        private string street;
        private string town;

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

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
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

        public Command EditCommand { get; set; }
        public EditDescrip_Title_Town_StreetViewModel()
        {
            TitlePage = "Modifier";
            TownList = StaticListViewModel.GetTownCameroonList;
            EditCommand = new Command(OnEdit, Validate);
            this.PropertyChanged +=
               (_, __) => EditCommand.ChangeCanExecute();
        }

        private bool Validate()
        {
            return !String.IsNullOrWhiteSpace(Title)
                && !String.IsNullOrWhiteSpace(Description)
                && !String.IsNullOrWhiteSpace(Town)
                && !String.IsNullOrWhiteSpace(Street);

        }
        public async void OnEdit()
        {
            IsBusy = true;
            var accessToken = Settings.AccessToken;
            await _apiServices.EditProductWithSamePropertieAsync(accessToken , ItemId, Title, Description, Town, Street);
            IsBusy = false;
            await Shell.Current.DisplayAlert("Information", "Modifier avec succès", "Ok");
            await Shell.Current.GoToAsync("..");
        }


        public async void LoadItemId(string itemId)
        {
            IsRunning = true;
            try
            {
                var id = Convert.ToInt32(itemId);
                var item = await _apiServices.GetProductSameParamAsync(id);
                Title = item.Title;
                Description = item.Description;
                Town = item.Town;
                Street = item.Street;
           

                IsRunning = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

        }
    }
}
