using LookaukwatApp.ViewModels.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.HouseView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseDetailPage : ContentPage
    {
        public HouseDetailPage()
        {
            InitializeComponent();
            BindingContext = new HouseDetailViewModel();
            Shell.SetTabBarIsVisible(this, false);
        }

        private async void CopyLink_Click(object sender, EventArgs e)
        {

            await DisplayAlert("Alerte", "Copier dans le papier-presse", "Ok");

        }
    }
}