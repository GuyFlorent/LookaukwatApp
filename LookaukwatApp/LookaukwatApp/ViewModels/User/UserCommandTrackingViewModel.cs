using LookaukwatApp.Models;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.User
{
    [QueryProperty(nameof(CommandId), nameof(CommandId))]
    public class UserCommandTrackingViewModel : BaseViewModel
    {
        ApiServices _apiServices = new ApiServices();

        ObservableCollection<TrackingCommandModelViewModel> trackings = new ObservableCollection<TrackingCommandModelViewModel>();
        public ObservableCollection<TrackingCommandModelViewModel> Trackings { get => trackings; set => SetProperty(ref trackings, value); }



        private string commandId;
        public string CommandId
        {
            get
            {
                return commandId;
            }
            set
            {
                commandId = value;
                if (value != null)
                {
                   
                    LoadItemId(value);
                }
            }
        }

      
        public UserCommandTrackingViewModel()
        {
            TitlePage = "Suivie de ma commande";
        }



        private async void LoadItemId(string value)
        {
            IsRunning = true;
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Pas de connexion internet !", "Vérifiez votre connexion", "OK");

                IsRunning = false;
                return;
            }
            Trackings.Clear();
            try
            {
                int commandId = Convert.ToInt32(value);
                List<TrackingCommandModelViewModel> result = await _apiServices.GetListTrackingCommand(commandId);
                foreach (var track in result)
                {
                    Trackings.Add(track);
                }
            }catch(Exception e)
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
