using LookaukwatApp.Helpers;
using LookaukwatApp.Views.LoginView;
using LookaukwatApp.Views.RegisterView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PublishAnnouncePage : ContentPage
    {
        public PublishAnnouncePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var token = Settings.AccessToken;
            var username = Settings.Username;
            var password = Settings.Password;

            //if (!string.IsNullOrWhiteSpace(token))
            //{
            //    await Shell.Current.GoToAsync(nameof(PublishAnnouncePage));
            //}
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(token))
            {
                //await Navigation.PushAsync(new LoginPage());
                await PopupNavigation.Instance.PushAsync(new LoginPage());
            }
            else if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(token))
            {

                await PopupNavigation.Instance.PushAsync(new RegisterPage());
                // await Shell.Current.GoToAsync(nameof(RegisterPage));
                // await Navigation.PushModalAsync(new RegisterPage());
                // Navigation.RemovePage(this);
            }

        }
    }
}