using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavorisPage : ContentPage
    {
        ObservableCollection<ProductForMobileViewModel> liste = new ObservableCollection<ProductForMobileViewModel>();
        public ObservableCollection<ProductForMobileViewModel> ListeFavo { get => liste; set => liste = value; }
        public FavorisPage()
        {
            InitializeComponent();
          
        }


        protected  override void OnAppearing()
        {
            base.OnAppearing();

            RefreshList();

        }

        private async void Remove_Button(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var item = args.Parameter as ProductForMobileViewModel;

           var response =  await Shell.Current.DisplayAlert("Retirer une annonce favorite", "Voulez vous vraiment retirer cette anonce ?", "Oui", "Non");
            if (response)
            {
                ListeFavo.Remove(item);
                Settings.JsonFavoriteList = JsonConvert.SerializeObject(ListeFavo);
            }
        }
        


            private void RefreshList()
        {
            var Liste = Settings.JsonFavoriteList;

            if (!string.IsNullOrWhiteSpace(Liste))
            {
                ListeFavo.Clear();

                List<ProductForMobileViewModel> ListFavorites = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(Liste);
                
                foreach(var item in ListFavorites)
                {
                    ListeFavo.Add(item);
                }
                if (ListFavorites.Count > 0)
                {
                    Stack_HasResult.IsVisible = true;
                    Stack_Has_NoResult.IsVisible = false;
                    mylist.ItemsSource = ListeFavo;
                    
                    
                }
                else
                {
                    Stack_Has_NoResult.IsVisible = true;
                    Stack_HasResult.IsVisible = false;
                    mylist.ItemsSource = "";
                }
            }
        }
    }
}