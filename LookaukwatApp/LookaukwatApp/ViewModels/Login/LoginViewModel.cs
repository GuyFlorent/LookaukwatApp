using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.Views.RegisterView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; set; }
        public Command LoginCommandToRenewToken { get; set; }
        public Command LoginByPhoneCommand { get; set; }
        public Command LoginUserAccountCommand { get; set; }
        public Command LoginUserAccountByPhoneCommand { get; set; }
        public Command RegisterViewCommand { get; }
        // public INavigation Navigation { get; set; }
        ApiServices _apiServices = new ApiServices();

        private string usermame;
        private string phone;
        private string password;
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }
        public LoginViewModel()
        {
            usermame = Settings.Username;
            password = Settings.Password;
            phone = Settings.Phone;
            TitlePage = "Connexion par email";
            LoginCommand = new Command(OnLoginClicked);
            LoginByPhoneCommand = new Command(OnLoginByPhoneClicked);
            LoginCommandToRenewToken = new Command(OnLoginToRenewToken);
            LoginUserAccountCommand = new Command(OnLoginReturnUserClicked);
            LoginUserAccountByPhoneCommand = new Command(OnLoginByPhoneReturnUserClicked);
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
        }


        private bool ValidateLoging()
        {
            return !String.IsNullOrWhiteSpace(Username)
                && Username.Contains("@")
                && !String.IsNullOrWhiteSpace(Password);
        }

        private bool ValidateLoginByPhone()
        {
            return !String.IsNullOrWhiteSpace(Phone)
                && !String.IsNullOrWhiteSpace(Password);
        }
        public string Username
        {
            get => usermame;
            set => SetProperty(ref usermame, value);
        }

        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private bool isEmail = true;
        public bool IsEmail
        {
            get { return isEmail; }
            set
            {
                SetProperty(ref isEmail, value);

                if (value == true)
                {
                    IsPhone = false;
                }
            }
        }
        private bool isPhone = false;

        public bool IsPhone
        {
            get { return isPhone; }
            set
            {
                SetProperty(ref isPhone, value);

                if (value == true)
                {
                    IsEmail = false;
                }
            }
        }
        private async void OnLoginToRenewToken(object obj)
        {
           
                var accesstonken = await _apiServices.LoginASync(Settings.Username, Settings.Password);
            if (accesstonken != null)
                Settings.AccessToken = accesstonken;
        }
        private async void OnLoginClicked()
        {
            IsRunning = true;

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsRunning = false;
                return;
            }

            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    var accesstonken = await _apiServices.LoginASync(Username, Password);

                    Settings.AccessToken = accesstonken;
                    // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                    if (accesstonken != null)
                    {
                        Settings.Password = password;
                        await PopupNavigation.Instance.PopAllAsync();
                        // await Shell.Current.GoToAsync("//MainPage/PublishAnnouncePage");
                        IsRunning = false;
                    }
                    else
                    {
                        IsRunning = false;
                        ErrorMessage = "Email et/ou mot de passe incorrect !";
                    }
                }catch(Exception e) { Console.WriteLine(e.Message); }
                
            }
            else
            {
                IsRunning = false;
                ErrorMessage = "Email et mot de passe sont obligatoires !";
            }

        }

        private async void OnLoginByPhoneClicked()
        {
            IsRunning = true;

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsRunning = false;
                return;
            }

            if (!string.IsNullOrEmpty(Phone) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    string userEmail = await _apiServices.GetUserNameWithPhoneAsync(Phone);
                    if (userEmail == null)
                    {
                        IsRunning = false;
                        ErrorMessage = "Téléphone et/ou mot de passe incorrecte !";
                    }
                    else
                    {

                        var accesstonken = await _apiServices.LoginASync(userEmail, Password);

                        Settings.AccessToken = accesstonken;
                        // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                        if (accesstonken != null)
                        {
                            Settings.Password = password;
                            await PopupNavigation.Instance.PopAllAsync();
                            //await Shell.Current.GoToAsync("//MainPage/PublishAnnouncePage");
                            IsRunning = false;
                        }
                        else
                        {
                            IsRunning = false;
                            ErrorMessage = "Téléphone et/ou mot de passe incorrect !";
                        }
                    }
                }catch(Exception e) { Console.WriteLine(e.Message); }
               
            }
            else
            {
                IsRunning = false;
                ErrorMessage = "Téléphone et mot de passe sont obligatoires !";
            }
        }

        private async void OnLoginReturnUserClicked()
        {
            IsRunning = true;
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsRunning = false;
                return;
            }

            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    var accesstonken = await _apiServices.LoginASync(Username, Password);

                    Settings.AccessToken = accesstonken;
                    // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                    if (accesstonken != null)
                    {
                        Settings.Password = password;
                        await PopupNavigation.Instance.PopAllAsync();
                        //await Shell.Current.GoToAsync("//MainPage/UserProfilePage");
                        IsRunning = false;
                    }
                    else
                    {
                        IsRunning = false;
                        ErrorMessage = "Email et/ou mot de passe incorrect !";
                    }
                }catch(Exception e) { Console.WriteLine(e.Message); }
                
            }
            else
            {
                IsRunning = false;
                ErrorMessage = "Email et mot de passe sont obligatoires !";
            }
        }
        //for login with phone
        private async void OnLoginByPhoneReturnUserClicked()
        {
            IsRunning = true;

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsRunning = false;
                return;
            }

            if (!string.IsNullOrEmpty(Phone) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    string userEmail = await _apiServices.GetUserNameWithPhoneAsync(Phone);
                    if (userEmail == null)
                    {
                        IsRunning = false;
                        ErrorMessage = "Téléphone et/ou mot de passe incorrecte !";
                    }
                    else
                    {
                        var accesstonken = await _apiServices.LoginASync(userEmail, Password);

                        Settings.AccessToken = accesstonken;
                        // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                        if (accesstonken != null)
                        {
                            Settings.Password = password;
                            await PopupNavigation.Instance.PopAllAsync();
                            //await Shell.Current.GoToAsync("//MainPage/UserProfilePage");
                            IsRunning = false;
                        }
                        else
                        {
                            IsRunning = false;
                            ErrorMessage = "Téléphone et/ou mot de passe incorrect !";
                        }
                    }
                }catch(Exception e) { Console.WriteLine(e.Message); }
               
            }
            else
            {
                IsRunning = false;
                ErrorMessage = "Téléphone et mot de passe sont obligatoires!";
            }

        }
    }
}
