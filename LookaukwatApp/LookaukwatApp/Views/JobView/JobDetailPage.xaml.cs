﻿using LookaukwatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.JobView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobDetailPage : ContentPage
    {
        public JobDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }

        private async void CopyLink_Click(object sender, EventArgs e)
        {

            await DisplayAlert("Alerte", "Copier dans le papier-presse", "Ok");

        }
    }
}