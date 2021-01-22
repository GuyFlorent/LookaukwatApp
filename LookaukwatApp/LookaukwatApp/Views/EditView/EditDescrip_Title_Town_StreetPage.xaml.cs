using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.EditView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditDescrip_Title_Town_StreetPage : ContentPage
	{
		public EditDescrip_Title_Town_StreetPage ()
		{
			InitializeComponent ();
			Shell.SetTabBarIsVisible(this, false);
		}
	}
}