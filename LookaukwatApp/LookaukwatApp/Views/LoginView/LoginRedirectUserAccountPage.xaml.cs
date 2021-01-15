using LookaukwatApp.ViewModels.Login;
using LookaukwatApp.Views.RegisterView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.LoginView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginRedirectUserAccountPage 
    {
        public LoginRedirectUserAccountPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private async void RegisterViewForUser(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            await PopupNavigation.Instance.PushAsync(new RegisterRedirectLoginUserAccountPage());
        }

        private async void ForgotPassword_Button(object o, EventArgs e)
        {
           await Browser.OpenAsync("https://lookaukwat.com/Account/ForgotPassword");
        }

        private async void ContactUs_Button(object o, EventArgs e)
        {
           await Browser.OpenAsync("https://lookaukwat.com/Home/Contact");
        }

        private async void ClosePoppup_Button(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();

        }
    }
}