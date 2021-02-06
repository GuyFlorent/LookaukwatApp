using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.Views.LoginView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Register
{
    public class RegisterViewModel : BaseViewModel
    {
        //for error message
        private string errorMessage1;
        private string errorMessage2;
        private string errorMessage3;
        private string errorMessage4;
        public string ErrorMessage4
        {
            get => errorMessage4;
            set => SetProperty(ref errorMessage4, value);
        }
        public string ErrorMessage3
        {
            get => errorMessage3;
            set => SetProperty(ref errorMessage3, value);
        }
        public string ErrorMessage2
        {
            get => errorMessage2;
            set => SetProperty(ref errorMessage2, value);
        }
        public string ErrorMessage1
        {
            get => errorMessage1;
            set => SetProperty(ref errorMessage1, value);
        }

        ApiServices _apiServices = new ApiServices();
        public string Message { get; set; }
        private string firstname;
        private string phone;
        private string email;
        public string password;
        public string confirmPassword;
        bool isTerms = false;
        public bool IsTerms
        {
            get => isTerms;
            set => SetProperty(ref isTerms, value);
        }
        public string FirstName
        {
            get => firstname;
            set => SetProperty(ref firstname, value);
        }
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }

        ObservableCollection<string> parrains = new ObservableCollection<string>();
        public ObservableCollection<string> Parrains { get => parrains; set => SetProperty(ref parrains, value); }

        private IDictionary<string, string> listParrian = new Dictionary<string, string>();
        public IDictionary<string,string> ListParrian { get => listParrian; set => SetProperty(ref listParrian, value); }

        bool isParrainCheck = false;
        public bool IsParrainCheck
        {
            get { return isParrainCheck; }
            set
            {
                SetProperty(ref isParrainCheck, value); 
                if(value == true)
                {
                    GetParrainList();
                }
                else
                {

                }
            }
        }

        private string parrainName;
        public string ParrainName
        {
            get => parrainName;
            set => SetProperty(ref parrainName, value);
        }

        public RegisterViewModel()
        {
            TitlePage = "Creation de compte";
            
            RegisterCommand = new Command(OnRegister, ValidateRegister);
            RegisterReturnUserAccountCommand = new Command(OnRegisterReturnUserAccount, ValidateRegister);
            LoginCommand = new Command(OnLoginClicked);
            LoginReturnForUserCommand = new Command(OnLoginReturnForUserClicked);
            this.PropertyChanged +=
                (_, __) => RegisterCommand.ChangeCanExecute();
            this.PropertyChanged +=
               (_, __) => RegisterReturnUserAccountCommand.ChangeCanExecute();
        }



        public Command RegisterCommand { get; set; }
        public Command RegisterReturnUserAccountCommand { get; set; }
        public Command LoginCommand { get; }
        public Command LoginReturnForUserCommand { get; }

        private bool ValidateRegister()
        {
            return !String.IsNullOrWhiteSpace(Email)
             && Email.Contains("@")
             && !String.IsNullOrWhiteSpace(FirstName)
             && !String.IsNullOrWhiteSpace(Password)
             && !String.IsNullOrWhiteSpace(ConfirmPassword)
               ;
        }

        private async void GetParrainList()
        {
            Parrains.Clear();
            ListParrian.Clear();
            ListParrian = await _apiServices.GetListParrainAsync();

            foreach(var key in ListParrian.Keys)
            {
                Parrains.Add(key);
            }
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        private async void OnLoginReturnForUserClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync(nameof(LoginRedirectUserAccountPage));
        }
        private async void OnRegister()
        {
            if (IsTerms)
            {


                IsRunning = true;
                ErrorMessage1 = "";
                ErrorMessage2 = "";
                ErrorMessage3 = "";
                ErrorMessage4 = "";

                if (!string.IsNullOrWhiteSpace(Email) || !string.IsNullOrWhiteSpace(FirstName) || !string.IsNullOrWhiteSpace(Phone)
                    || !string.IsNullOrWhiteSpace(Password) || !string.IsNullOrWhiteSpace(ConfirmPassword))
                {


                    bool checkPhoneExist = await _apiServices.CheckPhoneExistAsync(Phone);
                    bool checkEmailExist = await _apiServices.CheckEmailExistAsync(Email);

                    if (checkPhoneExist && checkEmailExist)
                    {
                        IsRunning = false;
                        ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                        ErrorMessage2 = "Cette adresse Email existe déja !";
                    }
                    else if (checkPhoneExist)
                    {
                        IsRunning = false;
                        ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                    }
                    else if (checkEmailExist)
                    {
                        IsRunning = false;
                        ErrorMessage2 = "Cette adresse Email existe déja !";
                    }

                    if ((Password.Length < 6 || confirmPassword.Length < 6) && Password != confirmPassword)
                    {
                        IsRunning = false;
                        ErrorMessage3 = "Le mot de passe doit être minimun 6 caractères.";
                        ErrorMessage4 = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.";
                    }
                    else if ((Password.Length >= 6 && confirmPassword.Length >= 6) && Password != confirmPassword)
                    {
                        IsRunning = false;
                        ErrorMessage4 = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.";
                    }
                    else if ((Password.Length < 6 || confirmPassword.Length < 6) && Password == confirmPassword)
                    {
                        IsRunning = false;
                        ErrorMessage3 = "Le mot de passe doit être minimun 6 caractères.";

                    }

                    if (checkPhoneExist == false && checkEmailExist == false && (Password.Length >= 6 && confirmPassword.Length >= 6) && Password == confirmPassword)
                    {
                        string parrainValue = null;
                        if (!string.IsNullOrWhiteSpace(ParrainName))
                        {
                            parrainValue = ListParrian[ParrainName];
                        }
                        var isSuccess = await _apiServices.RegisterAsync(Email, FirstName, Phone, Password, ConfirmPassword, parrainValue);


                        if (isSuccess)
                        {
                            IsRunning = false;
                            Settings.Username = Email;
                            Settings.Password = password;
                            Settings.Phone = Phone;
                            Message = "C'est bon c'est ajouté";
                            // await Shell.Current.GoToAsync(nameof(LoginPage));
                            await PopupNavigation.Instance.PopAllAsync();
                            await PopupNavigation.Instance.PushAsync(new LoginPage());
                        }
                        else
                        {
                            IsRunning = false;
                            Message = "C'est pas bon";
                        }

                    }
                }
                else
                {
                    IsRunning = false;
                    ErrorMessage1 = "Les champs: Prénom, Email, Téléphone, Mot de passe et Confirmation de mot de passe sont obligatoires !";
                }
                // This will pop the current page off the navigation stack
            }
        }

        private async void OnRegisterReturnUserAccount()
        {
            if (IsTerms)
            {


                IsRunning = false;
                ErrorMessage1 = "";
                ErrorMessage2 = "";
                ErrorMessage3 = "";
                ErrorMessage4 = "";

                if (!string.IsNullOrWhiteSpace(Email) || !string.IsNullOrWhiteSpace(FirstName) || !string.IsNullOrWhiteSpace(Phone)
                    || !string.IsNullOrWhiteSpace(Password) || !string.IsNullOrWhiteSpace(ConfirmPassword))
                {


                    bool checkPhoneExist = await _apiServices.CheckPhoneExistAsync(Phone);
                    bool checkEmailExist = await _apiServices.CheckEmailExistAsync(Email);

                    if (checkPhoneExist && checkEmailExist)
                    {
                        IsRunning = false;
                        ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                        ErrorMessage2 = "Cette adresse Email existe déja !";
                    }
                    else if (checkPhoneExist)
                    {
                        IsRunning = false;
                        ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                    }
                    else if (checkEmailExist)
                    {
                        IsRunning = false;
                        ErrorMessage2 = "Cette adresse Email existe déja !";
                    }

                    if ((Password.Length < 6 || confirmPassword.Length < 6) && Password != confirmPassword)
                    {
                        IsRunning = false;
                        ErrorMessage3 = "Le mot de passe doit être minimun 6 caractères.";
                        ErrorMessage4 = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.";
                    }
                    else if ((Password.Length >= 6 && confirmPassword.Length >= 6) && Password != confirmPassword)
                    {
                        IsRunning = false;
                        ErrorMessage4 = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.";
                    }
                    else if ((Password.Length < 6 || confirmPassword.Length < 6) && Password == confirmPassword)
                    {
                        IsRunning = false;
                        ErrorMessage3 = "Le mot de passe doit être minimun 6 caractères.";

                    }

                    if (checkPhoneExist == false && checkEmailExist == false && (Password.Length >= 6 && confirmPassword.Length >= 6) && Password == confirmPassword)
                    {
                        string parrainValue = null;
                        if (!string.IsNullOrWhiteSpace(ParrainName))
                        {
                            parrainValue = ListParrian[ParrainName];
                        }

                        var isSuccess = await _apiServices.RegisterAsync(Email, FirstName, Phone, Password, ConfirmPassword, parrainValue);


                        if (isSuccess)
                        {
                            IsRunning = false;
                            Settings.Username = Email;
                            Settings.Password = password;
                            Settings.Phone = Phone;
                            Message = "C'est bon c'est ajouté";
                            //await Shell.Current.GoToAsync(nameof(LoginRedirectUserAccountPage));
                            await PopupNavigation.Instance.PopAllAsync();
                            await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
                        }
                        else
                        {
                            IsRunning = false;
                            Message = "C'est pas bon";
                        }
                    }
                }
                else
                {
                    IsRunning = false;
                    ErrorMessage1 = "Les champs: Prénom, Email, Téléphone, Mot de passe et Confirmation de mot de passe sont obligatoires !";
                }
                // This will pop the current page off the navigation stack
            }
        }
    }
}
