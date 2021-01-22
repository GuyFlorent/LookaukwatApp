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
    public partial class UserProfilePage : ContentPage
    {
        public UserProfilePage()
        {
            InitializeComponent();
        }

        async void LogOut_Click(object sender, EventArgs e)
        {
            string accessToken = Settings.AccessToken;
            var response = true;
            if (response)
            {
                Settings.AccessToken = "";
                var username = Settings.Username;
                var password = Settings.Password;
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
                }

                else if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
                {
                    await PopupNavigation.Instance.PushAsync(new RegisterRedirectLoginUserAccountPage());

                }
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            UserName.Text = Settings.FirstName;
            var token = Settings.AccessToken;
            var username = Settings.Username;
            var password = Settings.Password;
            //if (!string.IsNullOrWhiteSpace(token))
            //{
            //    await Shell.Current.GoToAsync(nameof(UserProfilePage));
            //}
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(token))
            {
                await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
            }

            else if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(token))
            {
                await PopupNavigation.Instance.PushAsync(new RegisterRedirectLoginUserAccountPage());

            }
        }
    }
}