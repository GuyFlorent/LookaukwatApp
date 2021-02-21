using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using LookaukwatApp.Views.UserView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.User
{
    public class UserCommandsViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        ObservableCollection<CommandForMobile> commands = new ObservableCollection<CommandForMobile>();
        public ObservableCollection<CommandForMobile> Commands { get => commands; set => SetProperty(ref commands, value); }

        public Command TrackingOrderCommand { get; }
        public Command BillCommand { get; }
        public UserCommandsViewModel()
        {
            TitlePage = "Mes commandes";
            BillCommand = new Command(async (e) =>
            {
                var item = e as CommandForMobile;

                await Shell.Current.GoToAsync($"{nameof(UserBillPage)}?{nameof(UserBillViewModel.CommandId)}={item.CommandId}");


            });
            Populate();
        }



      

        private async void Populate()
        {
            IsRunning = true;
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");
               
                IsRunning = false;
                return;
            }
            try
            {
                Commands.Clear();
                string accessToken = Settings.AccessToken;
                var results = await _apiServices.GetUserCommandsAsync(accessToken);
                foreach (var item in results)
                {
                    Commands.Add(item);
                }
            } catch( Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                IsRunning = false;
            }
           
        }
    }
}
