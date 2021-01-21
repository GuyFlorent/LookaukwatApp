using System;
using System.Collections.Generic;
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
	}
}