using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.ViewModels.Appartment;
using LookaukwatApp.ViewModels.House;
using LookaukwatApp.ViewModels.Job;
using LookaukwatApp.ViewModels.Mode;
using LookaukwatApp.ViewModels.Multimedia;
using LookaukwatApp.ViewModels.Vehicule;
using LookaukwatApp.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var fullScreenWidth = screenWidth / density;
            Resources.Add("FullScreenWidth", fullScreenWidth);
            MainPage = new AppShell();
        }


        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            if (uri.Host.EndsWith("lookaukwat.com", StringComparison.OrdinalIgnoreCase) ||
                uri.Host.EndsWith("lookaukwat.azurewebsites.net", StringComparison.OrdinalIgnoreCase))
            {

                if (uri.Segments != null && uri.Segments.Length == 4)
                {
                    var Controler = uri.Segments[1].Replace("/", "");
                    var msg = uri.Segments[2];
                    var id = uri.Segments[3].Replace("/", "");

                    switch (Controler)
                    {
                        
                        case "Job":
                            if (!string.IsNullOrEmpty(id))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Shell.Current.GoToAsync($"//MainPage/JobDetailPage?{nameof(JobDetailsViewModel.ItemId)}={id}");

                                });
                            }

                            break;

                        case "ApartmentRental":
                            if (!string.IsNullOrEmpty(id))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Shell.Current.GoToAsync($"//MainPage/ApartDetailPage?{nameof(ApartDetailViewModel.ItemId)}={id}");
                                });
                            }

                            break;

                        case "Multimedia":
                            if (!string.IsNullOrEmpty(id))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Shell.Current.GoToAsync($"//MainPage/MultimediaDetailPage?{nameof(MultimediaDetailViewModel.ItemId)}={id}");
                                });
                            }

                            break;

                        case "Vehicule":
                            if (!string.IsNullOrEmpty(id))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {

                                    await Shell.Current.GoToAsync($"//MainPage/VehiculeDetailPage?{nameof(VehiculeDetailViewModel.ItemId)}={id}");
                                });
                            }

                            break;

                        case "Mode":
                            if (!string.IsNullOrEmpty(id))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Shell.Current.GoToAsync($"//MainPage/ModeDetailPage?{nameof(ModeDetailViewModel.ItemId)}={id}");
                                });
                            }

                            break;

                        case "House":
                            if (!string.IsNullOrEmpty(id))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    
                                    await Shell.Current.GoToAsync($"//MainPage/HouseDetailPage?{nameof(HouseDetailViewModel.ItemId)}={id}");
                                });
                            }

                            break;

                        default:
                            Browser.OpenAsync(uri);
                            break;
                    }
                } else if (uri.Segments != null && (uri.Segments.Length == 1 || uri.Segments.Length == 2))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync($"//MainPage/ItemsPage");

                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {

                       await Launcher.OpenAsync(uri);
                    });
                } 
            }
        }


        ApiServices _apiServices = new ApiServices();
        protected override void OnStart()
        {
        }

        protected async override void OnSleep()
        {
            var items = await _apiServices.GetProductsAsync(pageIndex: 0, pageSize: 500, "");

            Settings.Products = JsonConvert.SerializeObject(items);
        }

        protected override void OnResume()
        {
        }
    }
}
