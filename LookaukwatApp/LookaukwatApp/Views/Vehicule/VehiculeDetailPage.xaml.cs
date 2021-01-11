using LookaukwatApp.ViewModels.Vehicule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            BindingContext = new VehiculeDetailViewModel();
        }

        private async void CopyLink_Click(object sender, EventArgs e)
        {

            await DisplayAlert("Alerte", "Copier dans le papier-presse", "Ok");

        }
    }
}