using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace LookaukwatApp.Droid
{
    [Activity(Label = "Lookaukwat", Icon = "@mipmap/ico", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]

    //Invite App Link
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "https",
                  DataHost = "lookaukwat.com",
                  DataPaths = new[] { "/", "/Home" },
                  DataPathPrefixes = new[] { "/Job", "/ApartmentRental", "/Multimedia", "/Vehicule", "/Mode", "/House" },
                  AutoVerify = true,
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "http",
                  DataHost = "lookaukwat.com",
                  AutoVerify = true,
                  DataPaths = new[] { "/", "/Home" },
                  DataPathPrefixes = new[] { "/Job", "/ApartmentRental", "/Multimedia", "/Vehicule", "/Mode", "/House" },
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "https",
                  DataHost = "lookaukwat.azurewebsites.net",
                  DataPaths = new[] {"/","/Home"},
                  DataPathPrefixes = new[] {  "/Job", "/ApartmentRental", "/Multimedia", "/Vehicule","/Mode", "/House" },
                  AutoVerify = true,
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "http",
                  DataHost = "lookaukwat.azurewebsites.net",
                  AutoVerify = true,
                  DataPaths = new[] { "/", "/Home" },
                  DataPathPrefixes = new[] { "/Job", "/ApartmentRental", "/Multimedia", "/Vehicule", "/Mode", "/House" },
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]

    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "https",
                  DataHost = "www.lookaukwat.com",
                  DataPaths = new[] { "/", "/Home" },
                  DataPathPrefixes = new[] { "/Job", "/ApartmentRental", "/Multimedia", "/Vehicule", "/Mode", "/House" },
                  AutoVerify = true,
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "http",
                  DataHost = "www.lookaukwat.com",
                  AutoVerify = true,
                 DataPathPrefixes = new[] {"/Home/", "/Job", "/ApartmentRental", "/Multimedia", "/Vehicule", "/Mode", "/House" },
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]

    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "https",
                  DataHost = "www.lookaukwat.azurewebsites.net",
                  DataPaths = new[] { "/", "/Home" },
                  DataPathPrefixes = new[] { "/Job", "/ApartmentRental", "/Multimedia", "/Vehicule", "/Mode", "/House" },
                  AutoVerify = true,
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "http",
                  DataHost = "www.lookaukwat.azurewebsites.net",
                  AutoVerify = true,
                  DataPaths = new[] { "/", "/Home" },
                  DataPathPrefixes = new[] { "/Job", "/ApartmentRental", "/Multimedia", "/Vehicule", "/Mode", "/House" },
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
        }

        public override async void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
                await PopupNavigation.Instance.PopAsync();
                await Shell.Current.GoToAsync("//MainPage/ItemsPage");
            }
            
        }

    }
}