using LookaukwatApp.Helpers;
using LookaukwatApp.Services;
using LookaukwatApp.Views.LoginView;
using LookaukwatApp.Views.RegisterView;
using LookaukwatApp.Views.UserView;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.User
{
    public class UserProfileViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        private string olPassword;
        private string newPassword;
        private string confirmPassword;
        private string errorMessage1;
        private string errorMessage2;
        private string userName = Settings.FirstName;

        //for updating user informations
        private string newFirstName;
        private string newPhone;
        private string newEmail;

        public string OldFirstName { get; }
        public string OldPhone { get; }
        public string OldEmail { get; }
        public string NewFirstName
        {
            get => newFirstName;
            set => SetProperty(ref newFirstName, value);
        }
        public string NewPhone
        {
            get => newPhone;
            set => SetProperty(ref newPhone, value);
        }
        public string NewEmail
        {
            get => newEmail;
            set => SetProperty(ref newEmail, value);
        }
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
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
        public string OldPassword
        {
            get => olPassword;
            set => SetProperty(ref olPassword, value);
        }
        public string NewPassword
        {
            get => newPassword;
            set => SetProperty(ref newPassword, value);
        }
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }
        public UserProfileViewModel()
        {
            //TitlePage = "About";
            OldFirstName = Settings.FirstName;
            OldPhone = Settings.Phone;
            OldEmail = Settings.Username;
            AboutUsCommand = new Command(async () => await Browser.OpenAsync("https://lookaukwat.com/Home/About"));
            ContactUsCommand = new Command(async () => await Browser.OpenAsync("https://lookaukwat.com/Home/Contact"));
            UserAnnounceCommand = new Command(OnUserAnnouce);
            ProfileUserCommand = new Command(OnUserProfile);
            UpdateUserPasswordCommand = new Command(OnUserPassword);
            UpdateUserCommand = new Command(OnUpdateUserInfo, ValidateChangeUserInfo);
            UserPersonalInfoCommand = new Command(OnUserPersonalInfo);
            UpdateUserPageCommand = new Command(OnUpdateUserPage);
            UserTransactionsCommand = new Command(OnUserTransaction);
            ScanDeliveredItemCommand = new Command(OnScanDeliveredItem);
            ScanTrackingItemCommand = new Command(OnScanTrackingItem);
            UpdatePasswordCommand = new Command(OnChangePassword, ValidateChangePassword);
            this.PropertyChanged +=
               (_, __) => UpdatePasswordCommand.ChangeCanExecute();
            this.PropertyChanged +=
               (_, __) => UpdateUserCommand.ChangeCanExecute();
        }

        public ICommand AboutUsCommand { get; }
        public ICommand ContactUsCommand { get; }
        public Command UserAnnounceCommand { get; }
        public Command UpdateUserPasswordCommand { get; }
        public Command UpdatePasswordCommand { get; }
        public Command UpdateUserCommand { get; }
        public Command UpdateUserPageCommand { get; }
        public Command ProfileUserCommand { get; }
        public Command UserPersonalInfoCommand { get; }
        public Command UserTransactionsCommand { get; }
        public Command ScanDeliveredItemCommand { get; }
        public Command ScanTrackingItemCommand { get; }


       
        private bool ValidateChangePassword()
        {
            return !String.IsNullOrWhiteSpace(OldPassword)
                && !String.IsNullOrWhiteSpace(NewPassword)
                && !String.IsNullOrWhiteSpace(ConfirmPassword);
        }

        private bool ValidateChangeUserInfo()
        {
            return !String.IsNullOrWhiteSpace(NewPhone)
                || !String.IsNullOrWhiteSpace(NewEmail)
                || !String.IsNullOrWhiteSpace(NewFirstName);
        }

        private async void OnUserTransaction()
        {
            await Shell.Current.GoToAsync(nameof(UserTransactionsPage));
        }
        private async void OnUserAnnouce()
        {
            string accessToken = Settings.AccessToken;
            string username = Settings.Username;
            string password = Settings.Password;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                await Shell.Current.GoToAsync(nameof(UserAnnoucePage));
            }
            else if(!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
            }

            else
            {
                await PopupNavigation.Instance.PushAsync(new RegisterRedirectLoginUserAccountPage());

            }
        }

        private async void OnUserProfile()
        {
            string accessToken = Settings.AccessToken;
            string username = Settings.Username;
            string password = Settings.Password;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                await Shell.Current.GoToAsync(nameof(UserPage));
            }
            else if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
            }

            else 
            {
                await PopupNavigation.Instance.PushAsync(new RegisterRedirectLoginUserAccountPage());

            }
        }

        private async void OnUpdateUserPage()
        {
            string accessToken = Settings.AccessToken;
            string username = Settings.Username;
            string password = Settings.Password;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                await Shell.Current.GoToAsync(nameof(UserUpdateInfoPage));
            }
            else if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
            }

            else
            {
                await PopupNavigation.Instance.PushAsync(new RegisterRedirectLoginUserAccountPage());

            }
        }

        private async void OnUserPersonalInfo()
        {
            string accessToken = Settings.AccessToken;
            string username = Settings.Username;
            string password = Settings.Password;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                await Shell.Current.GoToAsync(nameof(UserPersonalInfoPage));
            }
             else if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
            }

            else
            {
                await PopupNavigation.Instance.PushAsync(new RegisterRedirectLoginUserAccountPage());

            }
        }

        private async void OnUserPassword()
        {
            string accessToken = Settings.AccessToken;
            string username = Settings.Username;
            string password = Settings.Password;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                await Shell.Current.GoToAsync(nameof(UpdateUserPasswordPage));
            }
            else if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
            }

            else
            {
                await PopupNavigation.Instance.PushAsync(new RegisterRedirectLoginUserAccountPage());

            }
        }

        private async void OnChangePassword()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                return;
            }


            var accessToken = Settings.AccessToken;
            string username = Settings.Username;
            string password = Settings.Password;
            if (accessToken != null)
            {
                if ((newPassword.Length < 6 || confirmPassword.Length < 6) && newPassword != confirmPassword)
                {
                    ErrorMessage1 = "Le mot de passe doit être minimun 6 caractères.";
                    ErrorMessage2 = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.";
                }
                else if ((newPassword.Length > 6 && confirmPassword.Length > 6) && newPassword != confirmPassword)
                {
                    ErrorMessage1 = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.";
                }
                else if ((newPassword.Length < 6 || confirmPassword.Length < 6) && newPassword == confirmPassword)
                {
                    ErrorMessage1 = "Le mot de passe doit être minimun 6 caractères.";

                }
                else
                {
                    var response = await _apiServices.ChangePasswordAsync(accessToken, OldPassword, NewPassword, ConfirmPassword);
                    if (response == null)
                    {
                        await Shell.Current.GoToAsync("///MainPage");
                    }
                    else
                    {
                        ErrorMessage1 = response;
                    }
                }



            }
            else if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                await PopupNavigation.Instance.PushAsync(new LoginRedirectUserAccountPage());
            }

            else
            {
                await PopupNavigation.Instance.PushAsync(new RegisterRedirectLoginUserAccountPage());

            }


        }
        private async void OnUpdateUserInfo()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                
                return;
            }

            if (!string.IsNullOrWhiteSpace(NewEmail) && !string.IsNullOrWhiteSpace(NewPhone) && !string.IsNullOrWhiteSpace(NewFirstName))
            {
                bool checkPhoneExist = await _apiServices.CheckPhoneExistAsync(NewPhone);
                bool checkEmailExist = await _apiServices.CheckEmailExistAsync(NewEmail);
                if (checkPhoneExist && checkEmailExist)
                {
                    ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                    ErrorMessage2 = "Cette adresse Email existe déja !";

                }
                else if (checkPhoneExist)
                {
                    ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                }
                else if (checkEmailExist)
                {
                    ErrorMessage2 = "Cette adresse Email existe déja !";
                }
                else
                {
                    await _apiServices.UpdateUserInfoAsync(NewFirstName, NewPhone, newEmail);
                    Settings.Username = NewEmail;
                    Settings.Phone = NewPhone;
                    Settings.FirstName = NewFirstName;

                    await App.Current.MainPage.DisplayAlert("Notification", "Votre profil a été modifié avec succès", "ok");

                    await Shell.Current.GoToAsync("///MainPage");
                }

            }
            else if (!string.IsNullOrWhiteSpace(NewEmail) && !string.IsNullOrWhiteSpace(NewPhone))
            {
                bool checkPhoneExist = await _apiServices.CheckPhoneExistAsync(NewPhone);
                bool checkEmailExist = await _apiServices.CheckEmailExistAsync(NewEmail);
                if (checkPhoneExist && checkEmailExist)
                {
                    ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                    ErrorMessage2 = "Cette adresse Email existe déja !";
                }
                else if (checkPhoneExist)
                {
                    ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                }
                else if (checkEmailExist)
                {
                    ErrorMessage2 = "Cette adresse Email existe déja !";
                }
                else
                {
                    await _apiServices.UpdateUserInfoAsync(OldFirstName, NewPhone, newEmail);

                    Settings.Username = NewEmail;
                    Settings.Phone = NewPhone;

                    await App.Current.MainPage.DisplayAlert("Notification", "Votre profil a été modifié avec succès", "ok");

                    await Shell.Current.GoToAsync("///MainPage");
                }
            }
            else if (!string.IsNullOrWhiteSpace(NewPhone) && !string.IsNullOrWhiteSpace(NewFirstName))
            {

                bool checkPhoneExist = await _apiServices.CheckPhoneExistAsync(NewPhone);
                if (checkPhoneExist)
                {
                    ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                }
                else
                {
                    await _apiServices.UpdateUserInfoAsync(NewFirstName, NewPhone, OldEmail);

                    Settings.Phone = NewPhone;
                    Settings.FirstName = NewFirstName;

                    await App.Current.MainPage.DisplayAlert("Notification", "Votre profil a été modifié avec succès", "ok");

                    await Shell.Current.GoToAsync("///MainPage");
                }
            }
            else if (!string.IsNullOrWhiteSpace(NewEmail) && !string.IsNullOrWhiteSpace(NewFirstName))
            {
                bool checkEmailExist = await _apiServices.CheckEmailExistAsync(NewEmail);
                if (checkEmailExist)
                {
                    ErrorMessage2 = "Cette adresse Email existe déja !";
                }
                else
                {
                    await _apiServices.UpdateUserInfoAsync(NewFirstName, OldPhone, NewEmail);

                    Settings.Username = NewEmail;
                    Settings.FirstName = NewFirstName;

                    await App.Current.MainPage.DisplayAlert("Notification", "Votre profil a été modifié avec succès", "ok");

                    await Shell.Current.GoToAsync("///MainPage");
                }
            }
            else if (!string.IsNullOrWhiteSpace(NewFirstName))
            {

                await _apiServices.UpdateUserInfoAsync(NewFirstName, OldPhone, OldEmail);

                Settings.FirstName = NewFirstName;

                await App.Current.MainPage.DisplayAlert("Notification", "Votre profil a été modifié avec succès", "ok");

                await Shell.Current.GoToAsync("///MainPage");

            }
            else if (!string.IsNullOrWhiteSpace(NewEmail))
            {
                bool checkEmailExist = await _apiServices.CheckEmailExistAsync(NewEmail);
                if (checkEmailExist)
                {
                    ErrorMessage2 = "Cette adresse Email existe déja !";
                }
                else
                {
                    await _apiServices.UpdateUserInfoAsync(OldFirstName, OldPhone, NewEmail);
                    Settings.Username = NewEmail;

                    await App.Current.MainPage.DisplayAlert("Notification", "Votre profil a été modifié avec succès", "ok");

                    await Shell.Current.GoToAsync("///MainPage");
                }
            }
            else if (!string.IsNullOrWhiteSpace(NewPhone))
            {
                bool checkPhoneExist = await _apiServices.CheckPhoneExistAsync(NewPhone);
                if (checkPhoneExist)
                {
                    ErrorMessage1 = "Ce numéro de téléphone existe déja !";
                }
                else
                {
                    await _apiServices.UpdateUserInfoAsync(OldFirstName, NewPhone, OldPhone);

                    Settings.Phone = NewPhone;

                    await App.Current.MainPage.DisplayAlert("Notification", "Votre profil a été modifié avec succès", "ok");

                    await Shell.Current.GoToAsync("///MainPage");
                }
            }


        }

        //For scanning item

        private async void OnScanDeliveredItem()
        {

            try
            {
                IQrScanningService scan = new QrScanningService();
                var result = await scan.ScanAsync();
                if (result != null)
                {
                    int CommandId = Convert.ToInt32(result);
                    string response = await _apiServices.GetDeliveredCommandAsync(CommandId);
                    if (!string.IsNullOrWhiteSpace(response))
                        await Shell.Current.DisplayAlert(response, "", "OK");
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
          
        }
        CancellationTokenSource cts;
        private async void OnScanTrackingItem()
        {
           

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Low, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                IQrScanningService scan = new QrScanningService();
                var result = await scan.ScanAsync();
                if (result != null)
                {
                    int Id = Convert.ToInt32(result);
                    var response = await _apiServices.PostTrackingCommandAsync(Id, location.Latitude.ToString(), location.Longitude.ToString());
                    if (response)
                        await Shell.Current.DisplayAlert("Scan validé !", "", "OK");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
