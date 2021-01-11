using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Appartment;
using LookaukwatApp.ViewModels.House;
using LookaukwatApp.ViewModels.Job;
using LookaukwatApp.ViewModels.Mode;
using LookaukwatApp.ViewModels.Multimedia;
using LookaukwatApp.ViewModels.Vehicule;
using LookaukwatApp.Views.AppartmentView;
using LookaukwatApp.Views.HouseView;
using LookaukwatApp.Views.JobView;
using LookaukwatApp.Views.ModeView;
using LookaukwatApp.Views.MultimediaView;
using LookaukwatApp.Views.Vehicule;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.OtherServices
{
    public class ProductSearchHandler : SearchHandler
    {
        ApiServices _apiServices = new ApiServices();
        protected async override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = await _apiServices.GetProductsAsync(newValue);

            }
        }

        protected override async void OnItemSelected(object itemm)
        {
            base.OnItemSelected(itemm);

            var item = itemm as ProductForMobileViewModel;
            if (item == null)
                return;

            switch (item.Category)
            {
                case "Emploi":

                    // This will push the ItemDetailPage onto the navigation stack
                    await Shell.Current.GoToAsync($"{nameof(JobDetailPage)}?{nameof(JobDetailsViewModel.ItemId)}={item.id}");

                    break;

                case "Immobilier":

                    await Shell.Current.GoToAsync($"{nameof(ApartDetailPage)}?{nameof(ApartDetailViewModel.ItemId)}={item.id}");
                    break;

                case "Mode":

                    await Shell.Current.GoToAsync($"{nameof(ModeDetailPage)}?{nameof(ModeDetailViewModel.ItemId)}={item.id}");

                    break;

                case "Multimedia":
                    await Shell.Current.GoToAsync($"{nameof(MultimediaDetailPage)}?{nameof(MultimediaDetailViewModel.ItemId)}={item.id}");
                    break;

                case "Vehicule":
                    await Shell.Current.GoToAsync($"{nameof(VehiculeDetailPage)}?{nameof(VehiculeDetailViewModel.ItemId)}={item.id}");
                    break;

                case "Maison":
                    await Shell.Current.GoToAsync($"{nameof(HouseDetailPage)}?{nameof(HouseDetailViewModel.ItemId)}={item.id}");
                    break;
            }


            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes "Blue%20Monkey"). Therefore, decode at the receiver.
            // await (App.Current.MainPage as :::no - loc(Xamarin.Forms):::.Shell).GoToAsync($"monkeydetails?name={((Animal)item).Name}");
        }
    }
}
