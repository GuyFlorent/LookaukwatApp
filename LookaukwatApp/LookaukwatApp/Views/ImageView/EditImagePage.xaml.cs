using LookaukwatApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.ImageView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditImagePage : ContentPage
	{
		public EditImagePage ()
		{
			InitializeComponent ();
			Shell.SetTabBarIsVisible(this, false);
		}

        private async void Tapped_Image(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var image = args.Parameter as ImageProcductModel;
            ObservableCollection<string> images = new ObservableCollection<string> { image.ImageMobile };
            await App.Current.MainPage.Navigation.PushAsync(new DisplayFullImagePage(images));

        }

    }
}