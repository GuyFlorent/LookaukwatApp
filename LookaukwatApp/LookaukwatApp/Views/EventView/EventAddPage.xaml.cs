﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.EventView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventAddPage : ContentPage
    {
        public EventAddPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}