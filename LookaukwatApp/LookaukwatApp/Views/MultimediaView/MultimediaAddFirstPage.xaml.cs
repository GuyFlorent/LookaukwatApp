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
    public partial class MultimediaAddFirstPage : ContentPage
    {
        public MultimediaAddFirstPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}