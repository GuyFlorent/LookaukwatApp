using LookaukwatApp.Models;
using LookaukwatApp.ViewModels.Image;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.ImageView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadImagePage : ContentPage
    {
        public UploadImagePage()
        {
            InitializeComponent();
            
            Shell.SetTabBarIsVisible(this, false);
           
        }
        //subscribe to tell to the mainPage to refresh the view

       
        // private void Valid_Image(object sender, EventArgs e)
        //{

        //    MessagingCenter.Send<Page>(this, "Update_listview");
        //}
        private async void Tapped_Image(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var image = args.Parameter as ImageProcductModel;
            ObservableCollection<string> images = new ObservableCollection<string> { image.ImageMobile };
            await App.Current.MainPage.Navigation.PushAsync(new DisplayFullImagePage(images));

        }
    }
}