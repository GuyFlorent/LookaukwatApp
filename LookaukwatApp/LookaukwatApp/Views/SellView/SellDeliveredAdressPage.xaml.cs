﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.SellView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellDeliveredAdressPage : ContentPage
    {
        public SellDeliveredAdressPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}