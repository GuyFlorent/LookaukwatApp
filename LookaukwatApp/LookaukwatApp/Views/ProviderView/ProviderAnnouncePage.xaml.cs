using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.ProviderView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProviderAnnouncePage : ContentPage
	{
		public ProviderAnnouncePage ()
		{
			InitializeComponent ();
			Shell.SetTabBarIsVisible(this, false);
		}
	}
}