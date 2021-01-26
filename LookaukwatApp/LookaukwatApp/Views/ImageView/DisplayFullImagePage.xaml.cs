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
	public partial class DisplayFullImagePage : ContentPage
	{
		public DisplayFullImagePage ()
		{
			InitializeComponent ();
			Shell.SetTabBarIsVisible(this, false);
		}
		public DisplayFullImagePage(ObservableCollection<string> Images)
		{
			InitializeComponent();
			Shell.SetTabBarIsVisible(this, false);
			CarouselImages.ItemsSource = Images;
		}
	}
}