using LookaukwatApp.ViewModels.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.MultimediaView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultimediaDetailPage : ContentPage
    {
        public MultimediaDetailPage()
        {
            InitializeComponent();
            BindingContext = new MultimediaDetailViewModel();
        }

        private async void CopyLink_Click(object sender, EventArgs e)
        {

            await DisplayAlert("Alerte", "Copier dans le papier-presse", "Ok");

        }
    }
}