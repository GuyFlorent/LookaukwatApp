using LookaukwatApp.ViewModels.Vehicule;
using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.Vehicule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehiculeDetailPage : ContentPage
    {
        public VehiculeDetailPage()
        {
            InitializeComponent();
           // BindingContext = new VehiculeDetailViewModel();
            Shell.SetTabBarIsVisible(this, false);
            
        }

        private async void CopyLink_Click(object sender, EventArgs e)
        {

            await DisplayAlert("Alerte", "Copier dans le papier-presse", "Ok");

        }

        private async void Map_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(Lat.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double lat))
                return;

            if (!double.TryParse(Lon.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double lon))
                return;

            var location = new Location(lat, lon);
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.None };

            await Map.OpenAsync(location, options);

        }
    }
}