using LookaukwatApp.ViewModels.Mode;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.ModeView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModeDetailPage : ContentPage
    {
        public ModeDetailPage()
        {
            InitializeComponent();
           // BindingContext = new ModeDetailViewModel();
            Shell.SetTabBarIsVisible(this, false);
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