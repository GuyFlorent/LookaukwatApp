using LookaukwatApp.Views.UserView;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.User
{
    public class UserTransactionsViewModel : BaseViewModel
    {
        bool isProvider = false;
        public bool IsProvider
        {
            get { return isProvider; }
            set { SetProperty(ref isProvider, value); }
        }
        public Command OrderCommand { get; }
        public Command AnnounceOnlineCommand { get; }
        public Command AnnounceSellCommand { get; }

        public UserTransactionsViewModel()
        {
            OrderCommand = new Command(OnOrder);
        }

        public async void OnOrder()
        {
            await Shell.Current.GoToAsync(nameof(UserCommandsPage));
        }
    }
}
