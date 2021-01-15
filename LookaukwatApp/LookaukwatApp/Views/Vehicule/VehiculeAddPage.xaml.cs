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
    public partial class VehiculeAddPage : ContentPage
    {
        public VehiculeAddPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}