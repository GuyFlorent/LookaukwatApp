using LookaukwatApp.ViewModels.Register;
using LookaukwatApp.Views.LoginView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LookaukwatApp.Views.RegisterView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage 
    {
        public RegisterPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterViewModel();
        }

        private async void LoginPagePopup_Buton(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            await PopupNavigation.Instance.PushAsync(new LoginPage(), true);
        }
        private async void ClosePoppup_Button(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync("//MainPage/ItemsPage");
        }
    }
}