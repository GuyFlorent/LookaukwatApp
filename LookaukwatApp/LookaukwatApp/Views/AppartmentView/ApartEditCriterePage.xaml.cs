﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.AppartmentView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ApartEditCriterePage : ContentPage
	{
		public ApartEditCriterePage ()
		{
			InitializeComponent ();
			Shell.SetTabBarIsVisible(this, false);
		}
	}
}