﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.UserView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPersonalInfoPage : ContentPage
    {
        public UserPersonalInfoPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}