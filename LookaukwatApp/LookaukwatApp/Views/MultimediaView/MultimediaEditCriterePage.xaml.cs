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
    public partial class MultimediaEditCriterePage : ContentPage
    {
        public MultimediaEditCriterePage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}