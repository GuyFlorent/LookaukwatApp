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
    public partial class LoginPage 
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private async void RegisterPage_Button(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            await PopupNavigation.Instance.PushAsync(new RegisterPage());
        }

        private async void ForgotPassword_Button(object o, EventArgs e)
        {
           await Browser.OpenAsync("https://lookaukwat.com/Account/ForgotPassword");
        }

        private async void ContactUs_Button(object o, EventArgs e)
        {
           await Browser.OpenAsync("https://lookaukwat.com/Home/Contact");
        }
    }
}