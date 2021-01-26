using LookaukwatApp.ViewModels;
using LookaukwatApp.ViewModels.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.JobView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobDetailPage : ContentPage
    {
        public JobDetailPage()
        {
            InitializeComponent();
            //BindingContext = new JobDetailsViewModel();
            Shell.SetTabBarIsVisible(this, false);
        }

        private async void CopyLink_Click(object sender, EventArgs e)
        {

            await DisplayAlert("Alerte", "Copier dans le papier-presse", "Ok");

        }

        private async void Map_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(Lat.Text, out double lat))
                return;

            if (!double.TryParse(Lon.Text, out double lon))
                return;

            var location = new Location(lat, lon);
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.None };

            await Map.OpenAsync(location, options);

        }
    }
}