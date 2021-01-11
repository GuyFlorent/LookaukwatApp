using LookaukwatApp.ViewModels.Mode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            BindingContext = new ModeDetailViewModel();
        }

        private async void CopyLink_Click(object sender, EventArgs e)
        {

            await DisplayAlert("Alerte", "Copier dans le papier-presse", "Ok");

        }
    }
}