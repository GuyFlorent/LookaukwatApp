using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.SellView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellPayementMethodPage : ContentPage
    {
        public SellPayementMethodPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}