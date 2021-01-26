using LookaukwatApp.Services;
using LookaukwatApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var fullScreenWidth = screenWidth / density;
            Resources.Add("FullScreenWidth", fullScreenWidth);
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
