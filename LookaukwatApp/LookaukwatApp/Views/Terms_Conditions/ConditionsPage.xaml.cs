using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.Terms_Conditions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConditionsPage 
	{
		public ConditionsPage ()
		{
			InitializeComponent ();
		}

		private async void ClosePoppup_Button(object o, EventArgs e)
		{
			await PopupNavigation.Instance.PopAsync();
			
		}
	}
}