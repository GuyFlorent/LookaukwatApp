using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.ModeView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModeEditCriterePage : ContentPage
	{
		public ModeEditCriterePage ()
		{
			InitializeComponent ();
			Shell.SetTabBarIsVisible(this, false);
		}
	}
}