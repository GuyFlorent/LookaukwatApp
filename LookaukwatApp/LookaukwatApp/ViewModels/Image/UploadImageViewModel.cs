using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.ViewModels.Image
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class UploadImageViewModel : BaseViewModel
    {
        private string itemId;
        public string ItemId
        {
            get => itemId;
            set => SetProperty(ref itemId, value);
        }
    }
}
