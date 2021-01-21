using LookaukwatApp.ViewModels.Image;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
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


    }
}