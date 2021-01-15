using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.AppartmentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApartAddFirstPage : ContentPage
    {
        public ApartAddFirstPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}