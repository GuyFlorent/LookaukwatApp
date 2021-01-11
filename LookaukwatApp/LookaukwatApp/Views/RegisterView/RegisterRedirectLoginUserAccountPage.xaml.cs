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
    public partial class RegisterRedirectLoginUserAccountPage
    {
        public RegisterRedirectLoginUserAccountPage()
        {
            InitializeComponent();
        }

        private async void OnLoginReturnForUserClicked(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
        }
    }
}