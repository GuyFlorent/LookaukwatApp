using LookaukwatApp.Views.LoginView;
using LookaukwatApp.Views.Terms_Conditions;
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

        private async void ClosePoppup_Button(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            await Shell.Current.GoToAsync("//MainPage/ItemsPage");
        }

        private async void Term_Button(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new ConditionsPage(), true);
        }
        private void Register_Button(object o, EventArgs e)
        {

            if (!CheckTerms.IsChecked)
            {
                Label_Terme.TextColor = Color.Red;

            }
            else
            {
                Label_Terme.TextColor = Color.Black;
            }
        }
    }
}