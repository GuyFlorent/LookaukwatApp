using LookaukwatApp.ViewModels;
using LookaukwatApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LookaukwatApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
